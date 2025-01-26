namespace Numbers.Api
{
    public static class Status
    {
        public static bool Healthy { get; set; }

        static Status()
        {
            Healthy = true;
        }
    }
}