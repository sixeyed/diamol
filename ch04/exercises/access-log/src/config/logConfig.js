const { format, transports } = require('winston');
var logConfig = module.exports = {};

logConfig.options = {
    format: format.combine(
        format.splat(),
        format.simple()
    ),
    transports: [
        new transports.Console({
            level: 'info'
        })
    ]
};
