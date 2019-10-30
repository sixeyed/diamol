const restify = require("restify");
const prom = require("prom-client");
const log = require("./log");
const os = require("os");

const accessCounter = new prom.Counter({
  name: "access_log_total",
  help: "Access Log - total log requests"
});

const clientIpGauge = new prom.Gauge({
  name: "access_client_ip_current",
  help: "Access Log - current unique IP addresses"
});

//setup Prometheus with hostname label:
const defaultLabels = { hostname: os.hostname() };
prom.register.setDefaultLabels(defaultLabels);
prom.collectDefaultMetrics();

function stats(req, res, next) {
  log.Logger.debug("** GET /stats called");
  var data = {
    logs: logCount
  };
  res.send(data);
  next();
}

function respond(req, res, next) {
  log.Logger.debug("** POST /access-log called");
  log.Logger.info("Access log, client IP: %s", req.body.clientIp);
  logCount++;

  //metrics:
  accessCounter.inc();
  ipAddresses.push(req.body.clientIp);
  let uniqueIps = Array.from(new Set(ipAddresses));
  clientIpGauge.set(uniqueIps.length);

  res.send(201, "Created");
  next();
}

var logCount = 0;
var ipAddresses = new Array();
var server = restify.createServer();
server.use(restify.plugins.bodyParser());
server.get("/stats", stats);
server.post("/access-log", respond);

server.get("/metrics", function(req, res, next) {
  res.end(prom.register.metrics());
});

server.headersTimeout = 20;
server.keepAliveTimeout = 10;
server.listen(80, function() {
  log.Logger.info("%s listening at %s", server.name, server.url);
});
