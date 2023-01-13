using Microsoft.Extensions.Options;

namespace TicTacToe.Web.Options;

public class PostgresOptions
{
    public string Host { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public int Port { get; set; }
    public string Database { get; set; }

    public string ToConnectionString() =>
        $"User Id={Username};Password={Password};Host={Host};Database={Database};Port={Port}";
}