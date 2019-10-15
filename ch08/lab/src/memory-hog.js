const fs = require("fs");

function allocate() {
  const toAllocate = 1024 * 1024 * process.env.LOOP_ALLOCATION_MB;

  /* this allocates a large chunk of memory, but in this app we're just going to pretend
  var buffers = new Array();
  for (i = 0; i <= loop; i++) {
    buffers.push(Buffer.alloc(toAllocate));
  }
  */

  var allocatedMb = process.env.LOOP_ALLOCATION_MB * loop;
  console.log(`Allocated: ${allocatedMb}MB`);
  fs.writeFileSync("ALLOCATED_MB", allocatedMb, "utf-8");

  loop++;
  setTimeout(allocate, process.env.LOOP_INTERVAL_MS);
}

var loop = 1;
allocate();
