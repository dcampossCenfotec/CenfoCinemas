namespace DTOs
{
    public class User : BaseDTO
    {
        public string UserCode { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birth { get; set; }
        public string Status { get; set; }
    }
}
