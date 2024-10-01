using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccidentDataStorage.Models.Accidents
{
    public class Accidents
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccidentId { get; set; }
        [Required(ErrorMessage = "*必須項目")]
        public required string ConstructionField { get; set; }

        [Required(ErrorMessage = "*必須項目")]
        public required string ConstructionType { get; set; }

        [Required(ErrorMessage = "*必須項目")]
        public required string WorkType { get; set; }

        [Required(ErrorMessage = "*必須項目")]
        public required string ConstructionMethod { get; set; }

        [Required(ErrorMessage = "*必須項目")]
        public required string DisasterCategory { get; set; }

        [Required(ErrorMessage = "*必須項目")]
        public required string AccidentCategory { get; set; }

        public string? Weather { get; set; }

        [Required(ErrorMessage = "*必須項目")]
        public required int AccidentYear { get; set; }

        [Required(ErrorMessage = "*必須項目")]
        public required int AccidentMonth { get; set; }

        [Required(ErrorMessage = "*必須項目")]
        public required int AccidentDateTime { get; set; }

        [Required(ErrorMessage = "*必須項目")]
        public required string AccidentLocationPref { get; set; }

        public string? AccidentBackground { get; set; }

        public string? AccidentCause { get; set; }

        public string? AccidentCountermeasure { get; set; }
    }
}
