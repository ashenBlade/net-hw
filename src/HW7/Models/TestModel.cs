using System.ComponentModel;

namespace HW7.Models
{
    public class TestModel
    {
        [DisplayName("Имя")]
        public string Name { get; set; }

        public string LastName { get; set; }
        public int Age { get; set; }

        [DisplayName("Кто по жизни?")]
        public Gender Gender { get; set; }
    }
}