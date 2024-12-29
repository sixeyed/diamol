## Lab Solution

You can find my solution in the `solution` folder:

- [Dockerfile](/ch13/lab/solution/Dockerfile)
- [docker-compose.yml](/ch13/lab/solution/docker-compose.yml)

Main points to note:

- the LogMonitor tool is from GitHub: https://github.com/microsoft/windows-container-tools/tree/master/LogMonitor

- the configuration file for LogMonitor is expected at the path `C:\LogMonitor\LogMonitorConfig.json`

- the entrypoint for the container is `LogMonitor.exe`, which needs to call `ServiceMonitor.exe` with the parameter `w3svc` - to start the chain of monitoring the IIS Windows Service

When you get it running you'll see this page at http://localhost:8088/signup:

![](/ch13/lab/signup-web.png)

If you follow the _Sign Up_ link and enter some details, you'll see a _Thank You_ page and you can check your data is saved by running a PowerShell command in the SQL Server container:

```
docker exec solution-SIGNUP-DB-DEV01-1 powershell "Invoke-SqlCmd -Query 'SELECT * FROM Prospects' -Database SignUp -TrustServerCertificate"
```