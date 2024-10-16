using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CrudDataApplication.Models
{
    public class Manager
    {
        [Key]
        public int ManagerId { get; set; }
        public string? ManagerName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool? IsActive { get; set; }

        [JsonIgnore]
        public virtual ICollection<Departments>? Departments { get; set; }
    }
}
