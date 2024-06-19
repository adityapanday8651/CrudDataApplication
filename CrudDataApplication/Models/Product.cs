using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CrudDataApplication.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId {  get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }

        [JsonIgnore]
        public virtual Category Category { get; set; }
    }
}
