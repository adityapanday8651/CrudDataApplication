using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace CrudDataApplication.Models
{
    public class Employees
    {
        [Key]
        public int EmployeeId { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public decimal? Salary { get; set; }
        public string? HireDate { get; set; }
        public int? AddressId { get; set; }

        [JsonIgnore]
        public virtual Address? Address { get; set; }

        [JsonIgnore]
        public virtual ICollection<Departments>? Departments { get; set; }
    }
}
