using System.Net.Http.Json;
using System.Text.Json;
using OpenMeteo;

static class CommandRegister
{
    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    static async Task Exit()
    {
        Console.WriteLine("Ukončuju program...");
        Environment.Exit(0);
    }

    static async Task Help()
    {
        Console.WriteLine("...Právě dorazila. (CASE SENSITIVE");
        foreach (var command in GetCommands())
        {
            Console.WriteLine(command.Name + " : " + command.Description);
        }
    }

    static async Task Prague()
    {
        Console.WriteLine("Počasí v Praze:");
        var url =
            "https://api.open-meteo.com/v1/forecast?latitude=50.07&longitude=14.41&hourly=temperature_2m,apparent_temperature";
        using var client = new HttpClient();
        var response = await client.GetAsync(url);
        var content = response.Content.ReadAsStringAsync().Result;
        Console.WriteLine(content);
    }

    static async Task Brno()
    {
        Console.WriteLine("Počasí v Brně:");
        //TODO: OPENMETEO NA SOUŘADNICE BRNA
    }

    public static List<Command> GetCommands()
    {
        return
        [
            new Command("konec", "Ukončí smyčku", Exit),
            new Command("pomoc", "Ukáže možné příkazy", Help),
            new Command("praha", "Ukáže počasí v Praze", Prague),
            new Command("brno", "ukáže počasí v Brně", Brno)
        ];
    }
}