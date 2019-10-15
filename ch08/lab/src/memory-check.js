const fs = require("fs");

if (!fs.existsSync("ALLOCATED_MB")) {
  console.log("Memory check: OK, none allocated.");
  process.exit(0);
}

const max = process.env.MAX_ALLOCATION_MB;
const allocated = parseInt(fs.readFileSync("ALLOCATED_MB", "utf-8"));

if (max >= allocated) {
  console.log(`Memory check: OK, allocated: ${allocated}MB, max: ${max}MB`);
  process.exit(0);
} else {
  console.log(`Memory check: FAIL, allocated: ${allocated}MB, max: ${max}MB`);
  process.exit(1);
}
