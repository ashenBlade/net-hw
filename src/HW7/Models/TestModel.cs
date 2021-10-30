using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HW7.Models
{
    public class TestModel
    {
        [DisplayName("Имя")]
        [Required]
        public string Name { get; set; }

        [Range(1, 4)]
        public string LastName { get; set; }

        [Phone]
        public string Phone { get; set; }

        [DataType(DataType.Password)]
        public int Age { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Кто по жизни?")]
        public Gender Gender { get; set; }

        [RegularExpression(@"\d\d\d")]
        public string Regex { get; set; }
    }
}