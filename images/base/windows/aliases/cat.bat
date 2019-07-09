@echo off
set "args=%1"
set "args=%args:/=\%"
type "%args%"