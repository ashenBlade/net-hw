namespace HW8.Services
{
    public class SimpleCalculatorService : ICalculatorService

    {
        public int Calculate(int left, string operation, int right)
        {
            return operation switch
                   {
                       "+" => left + right,
                       "-" => left - right,
                       "*" => left * right,
                       "/" => right == 0
                                  ? 0
                                  : left / right,
                       _ => 0
                   };
        }
    }
}