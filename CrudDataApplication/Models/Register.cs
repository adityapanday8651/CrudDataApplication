namespace CrudDataApplication.Models
{
    public class Register
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public virtual Roles Roles { get; set; }
    }
}
