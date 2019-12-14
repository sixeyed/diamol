using System.Net;
using System.Runtime.InteropServices;
using ToDoList.Model;

namespace ToDoList.Services
{
    public class DiagnosticsService
    {
        private static readonly Diagnostics _Diagnostics;

        static DiagnosticsService()
        {
            _Diagnostics = new Diagnostics
            {
                OSArchitecture = RuntimeInformation.OSArchitecture.ToString(),
                OSDescription = RuntimeInformation.OSDescription,
                FrameworkDescription = RuntimeInformation.FrameworkDescription,
                HostName = Dns.GetHostName()
            };
        }

        public Diagnostics GetDiagnostics()
        {
            return _Diagnostics;
        }
    }
}
