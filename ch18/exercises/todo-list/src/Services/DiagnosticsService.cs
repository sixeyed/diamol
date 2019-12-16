using System.Net;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Configuration;
using ToDoList.Model;

namespace ToDoList.Services
{
    public class DiagnosticsService
    {
        private static Diagnostics _Diagnostics;
        private readonly IConfiguration _configuration;

        public DiagnosticsService(IConfiguration configuration)
        {
            _configuration = configuration;
            EnsureDiagnostics();
        }

        private void EnsureDiagnostics()
        {
            if (_Diagnostics == null)
            {
                _Diagnostics = new Diagnostics
                {
                    OSArchitecture = RuntimeInformation.OSArchitecture.ToString(),
                    OSDescription = RuntimeInformation.OSDescription,
                    FrameworkDescription = RuntimeInformation.FrameworkDescription,
                    HostName = Dns.GetHostName(),
                    Release = _configuration["Release"],
                    Environment = _configuration["Environment"]
                };
            }
        }

        public Diagnostics GetDiagnostics()
        {
            return _Diagnostics;
        }
    }
}
