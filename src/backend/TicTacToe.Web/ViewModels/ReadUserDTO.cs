namespace TicTacToe.Web.ViewModels;

public class ReadUserDTO
{
    public ReadUserDTO(string name)
    {
        Name = name;
        Rank = 0;
    }
    
    public string Name { get; set; }
    public int Rank { get; set; }
}