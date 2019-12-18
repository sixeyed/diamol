using System;
using System.Linq;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;

namespace Diamol.Chapter19.Tail
{
    class Program
    {
        private static string _FullPath;
        private static PhysicalFileProvider _FileProvider;
        private static ManualResetEvent _ResetEvent = new ManualResetEvent(false);        

        public static void Main(params string[] args)
        {
            var directory = args[0];
            var filename = args[1];
            _FullPath = Path.Combine(directory, filename);
            WriteLastLine();

            /*
            var watcher = new FileSystemWatcher()
            {
                Path = directory,
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size,
                Filter = filename
            };
            watcher.Changed += (s, e) => WriteLastLine(e.FullPath);
            watcher.EnableRaisingEvents = true;
            */

            _FileProvider = new PhysicalFileProvider(directory);
            WatchForFileChanges();
            _ResetEvent.WaitOne();
        }

        private static void WatchForFileChanges()
        {
            var fileChangeToken = _FileProvider.Watch("*.*");
            fileChangeToken.RegisterChangeCallback(Notify, default);
        }

        private static void Notify(object state)
        {
            WriteLastLine();
            WatchForFileChanges();
        }


        private static void WriteLastLine()
        {
            //horribly inefficient, just for demo code:
            using (var stream = new FileStream(_FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = new StreamReader(stream))
                {
                    var lines = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine().Trim();
                        if (!string.IsNullOrEmpty(line))
                        {
                            lines.Add(line);
                        }
                    }
                    Console.WriteLine(lines.Last());
                }
            }
        }
    }
}
