using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HW7.Models
{
    public class TestModel
    {
        [DisplayName("Имя")]
        public string Name { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.Password)]
        public int Age { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Кто по жизни?")]
        public Gender Gender { get; set; }
    }
}