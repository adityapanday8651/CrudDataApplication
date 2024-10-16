using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace CrudDataApplication.Models
{
    public class Departments
    {
        [Key]
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int? ManagerId { get; set; }
        public int? EmployeesId { get; set; }
        public int? ProjectsId { get; set; }
        public bool? IsActive { get; set; }

        [JsonIgnore]
        public virtual Manager? Manager { get; set; }
        [JsonIgnore]
        public virtual Employees? Employees { get; set; }
        [JsonIgnore]
        public virtual Projects? Projects { get; set; }

        [JsonIgnore]
        public virtual ICollection<Company>? Company { get; set; }
    }
}
