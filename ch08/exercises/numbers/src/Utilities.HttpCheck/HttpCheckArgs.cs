using PowerArgs;

namespace Utilities.HttpCheck
{
    public class HttpCheckArgs
    {
        [ArgShortcut("u"), ArgDefaultValue("http://localhost")]
        public string Url { get; set; }

        [ArgShortcut("t"), ArgDefaultValue(300)]
        public int TimeoutMilliseconds { get; set; }

        [ArgShortcut("c")]
        public string UrlFromConfigSetting { get; set; }

        [ArgShortcut("ls"), ArgDefaultValue(true)]
        public bool LogSuccess { get; set; }

        [ArgShortcut("lf"), ArgDefaultValue(true)]
        public bool LogFailure { get; set; }
    }
}