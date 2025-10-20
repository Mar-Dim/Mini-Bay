namespace MiniBay.Shared.Features.Contact
{
    public class ContactInfoDto
    {
        public string? Address { get; set; }
        public string? SupportEmail { get; set; }
        public string? CustomerServicePhone { get; set; }
        public List<string>? Hours { get; set; }
    }
}