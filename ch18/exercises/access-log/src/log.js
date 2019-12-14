const winston = require('winston');
var logConfig = require('./config/logConfig');

const logger = winston.createLogger(logConfig.options);
exports.Logger = logger;