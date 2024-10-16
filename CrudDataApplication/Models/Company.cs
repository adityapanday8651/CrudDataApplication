using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CrudDataApplication.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public int? DepartmentId { get; set; }
        public bool? IsActive { get; set; }

        [JsonIgnore]
        public virtual Departments? Departments { get; set; }
    }
}
