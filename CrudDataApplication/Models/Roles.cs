using System.Text.Json.Serialization;

namespace CrudDataApplication.Models
{
    public class Roles
    {
        public int Id { get; set; }
        public string? RoleName { get; set; }
        public virtual ICollection<Register> Register { get; set; }
    }
}
