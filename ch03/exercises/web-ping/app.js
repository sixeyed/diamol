const https = require("https");

const options = {
  hostname: process.env.TARGET,
  method: process.env.METHOD
};

console.log(
  "** web-ping ** Pinging: %s; method: %s; %dms intervals",
  options.hostname,
  options.method,
  process.env.INTERVAL
);

process.on("SIGINT", function() {
  process.exit();
});

let i = 1;
let start = new Date().getTime();
setInterval(() => {
  start = new Date().getTime();
  console.log("Making request number: %d; at %d", i++, start);
  var req = https.request(options, res => {
    var end = new Date().getTime();
    var duration = end - start;
    console.log(
      "Got response status: %s at %d; duration: %dms",
      res.statusCode,
      end,
      duration
    );
  });
  req.on("error", e => {
    console.error(e);
  });
  req.end();
}, process.env.INTERVAL);
