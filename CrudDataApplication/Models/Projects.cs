using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CrudDataApplication.Models
{
    public class Projects
    {
        [Key]
        public int? ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public string? Status { get; set; }
        public int? TasksId { get; set; }
        public bool? IsActive { get; set; }

        [JsonIgnore]
        public virtual Tasks? Tasks { get; set; }
        [JsonIgnore]
        public virtual ICollection<Departments>? Departments { get; set; }
    }
}
