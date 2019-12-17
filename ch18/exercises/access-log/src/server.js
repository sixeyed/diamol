const restify = require("restify");
const prom = require("prom-client");
const log = require("./log");
const os = require("os");

// load config from multiple directories:
process.env["NODE_CONFIG_DIR"] =
  __dirname +
  "/config/" +
  require("path").delimiter +
  __dirname +
  "/config-override/";
const conf = require("config");

const accessCounter = new prom.Counter({
  name: "access_log_total",
  help: "Access Log - total log requests"
});

const clientIpGauge = new prom.Gauge({
  name: "access_client_ip_current",
  help: "Access Log - current unique IP addresses"
});

const metricsConf = conf.get("metrics");
if (metricsConf.enabled) {
  const defaultLabels = { hostname: os.hostname() };
  prom.register.setDefaultLabels(defaultLabels);
  prom.collectDefaultMetrics();
}

function stats(req, res, next) {
  log.Logger.debug("** GET /stats called");
  var data = {
    logs: logCount
  };
  res.send(data);
  next();
}

function config(req, res, next) {
  log.Logger.debug("** GET /config called");
  var data = {
    release: conf.get("release"),
    environment: conf.get("environment"),
    metricsEnabled: metricsConf.enabled
  };
  res.send(data);
  next();
}

function respond(req, res, next) {
  log.Logger.debug("** POST /access-log called");
  log.Logger.info("Access log, client IP: %s", req.body.clientIp);
  logCount++;

  if (metricsConf.enabled) {
    accessCounter.inc();
    ipAddresses.push(req.body.clientIp);
    let uniqueIps = Array.from(new Set(ipAddresses));
    clientIpGauge.set(uniqueIps.length);
  }

  res.send(201, "Created");
  next();
}

var logCount = 0;
var ipAddresses = new Array();
var server = restify.createServer();
server.use(restify.plugins.bodyParser());
server.get("/stats", stats);
server.get("/config", config);
server.post("/access-log", respond);

if (metricsConf.enabled) {
  server.get("/metrics", function(req, res, next) {
    res.end(prom.register.metrics());
  });
}

server.headersTimeout = 20;
server.keepAliveTimeout = 10;
server.listen(80, function() {
  log.Logger.info(
    "%s listening at %s, metrics enabled: %s",
    server.name,
    server.url,
    metricsConf.enabled
  );
});
