namespace CrudDataApplication.Models
{
    public class RefreshToken
    {
        public long Id { get; set; }
        public string? Token { get; set; }
        public string? JwtId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public string? UserName { get; set; }
    }
}
