using System.ComponentModel.DataAnnotations;

namespace HW10.Models
{
    public class CalculatorCache
    {
        [Key]
        public string Expression { get; set; }

        public decimal Value { get; set; }
    }
}