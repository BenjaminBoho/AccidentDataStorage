using System.ComponentModel.DataAnnotations;

namespace AccidentDataStorage.Models
{
    public class ItemList
    {
        [Required]
        public required string ItemGenre { get; set; }
        [Required]
        public required string ItemValue { get; set; }
        [Required]
        public required string ItemName { get; set; }
    }
}
