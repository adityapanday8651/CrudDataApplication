using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CrudDataApplication.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }

        [JsonIgnore]
        public virtual ICollection<Employees>? Employees { get; set; }
    }
}
