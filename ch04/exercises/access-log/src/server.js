var restify = require('restify');
var log = require('./log');

function respond(req, res, next) {
  log.Logger.debug('** POST /access-log called');  
  log.Logger.info('Access log, client IP: %s', req.body.clientIp);
  res.send(201, 'Created');
  next();
}

var server = restify.createServer();
server.use(restify.plugins.bodyParser());
server.post('/access-log', respond);

server.listen(80, function() {
  log.Logger.info('%s listening at %s', server.name, server.url);
});