namespace Pi.Web.Models
{
    public class PiViewModel
    {
        public string Value { get; set; }

        public int DecimalPlaces { get; set; }

        public long ComputeMilliseconds { get; set; }

        public string ComputeHost { get; set; }
    }
}