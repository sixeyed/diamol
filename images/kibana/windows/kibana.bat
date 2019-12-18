@echo off

SETLOCAL

set SCRIPT_DIR=%~dp0
for %%I in ("%SCRIPT_DIR%..") do set DIR=%%~dpfI

REM use the local node - the bundled version in the Kibana download has been removed
set NODE="node"

set NODE_ENV="production"

If Not Exist "%NODE%" (
  IF Exist "%SYS_NODE%" (
    set "NODE=%SYS_NODE%"
  ) else (
    Echo unable to find usable node.js executable.
    Exit /B 1
  )
)

"%NODE%" --no-warnings --max-http-header-size=65536 %NODE_OPTIONS% "%DIR%\src\cli" %*

:finally

ENDLOCAL
