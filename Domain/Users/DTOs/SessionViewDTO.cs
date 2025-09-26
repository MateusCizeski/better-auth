namespace Domain
{
    public class SessionViewDTO
    {
        public string DeviceId { get; set; }
        public string UserAgent { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public DateTime Expires { get; set; }
        public string IpAddress { get; set; }
    }
}
