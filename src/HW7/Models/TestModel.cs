using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HW7.Models
{
    public class TestModel
    {
        [Required]
        public string Required { get; set; }

        public string CamelCase { get; set; }

        [DisplayName("Number between 4 and 8")]
        [Range(4, 8)]
        public int SomeNumber { get; set; }

        [MinLength(4)]
        [MaxLength(6)]
        public int NumberMinimum4DigitsAndMaximum6Digits { get; set; }

        [MinLength(2)]
        [MaxLength(5)]
        public string StringMinimum2LettersAndMaximum5Letters { get; set; }

        [Phone]
        public string Phone { get; set; }

        [DataType(DataType.Password)]
        public int Password { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Gender (enum)")]
        public Gender Gender { get; set; }

        [RegularExpression(@"\d\d\d")]
        [Display(Name = "Here must be 3 digits")]
        public string Regex { get; set; }
    }
}