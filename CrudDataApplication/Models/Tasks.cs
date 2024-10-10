using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CrudDataApplication.Models
{
    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }
        public string? TaskName { get; set; }
        public string? Deadline { get; set; }
        public bool? Completed { get; set; }

        [JsonIgnore]
        public virtual ICollection<Projects>? Projects { get; set; }
    }
}
