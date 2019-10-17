var restify = require('restify');
var log = require('./log');

function stats(req, res, next) {
  log.Logger.debug('** GET /stats called');
  var data = {
    logs: logCount
  };
  res.send(data);
  next();
}

function respond(req, res, next) {
  log.Logger.debug('** POST /access-log called');  
  log.Logger.info('Access log, client IP: %s', req.body.clientIp);
  logCount++;
  res.send(201, 'Created');
  next();
}

var logCount = 0;
var server = restify.createServer();
server.use(restify.plugins.bodyParser());
server.get('/stats', stats);
server.post('/access-log', respond);

server.listen(80, function() {
  log.Logger.info('%s listening at %s', server.name, server.url);
});