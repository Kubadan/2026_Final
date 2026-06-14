using System.Drawing;
using System.Net.Http.Json;
using System.Text.Json;
using OpenMeteo;
using Jokes;

static class CommandRegister
{
    private static readonly HttpClient Http = new();

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public static async Task Exit()
    {
        Console.WriteLine("Ukončuju program...");
        Environment.Exit(0);
    }

    public static async Task Help()
    {
        Console.WriteLine("Dostupné příkazy (CASE SENSITIVE):");
        foreach (var command in GetCommands())
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{command.Name} : {command.Description}");
            Console.ResetColor();
        }

        await Task.CompletedTask;
    }

    public static async Task Joke()
    {
        string url = "https://official-joke-api.appspot.com/random_joke";
        try
        {
            var response = await Http.GetFromJsonAsync<JokeResponse>(url, JsonOpts);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(response.Setup);
            Thread.Sleep(2000);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(response.Punchline);
            Console.ResetColor();
        }
        // OŠETŘENÍ CATCHEM
        catch (UriFormatException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Chyba adresy]: URL adresa pro API nebyla správně sestavena. ({ex.Message})");
            Console.ResetColor();
        }
        catch (HttpRequestException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(
                $"[Chyba sítě]: Nepodařilo se spojit se serverem Official-Joke-API. Zkontrolujte internet. ({ex.Message})");
            Console.ResetColor();
            Console.WriteLine(url);
        }
        catch (JsonException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(
                $"[Chyba dat]: Odpověď ze serveru není validní JSON nebo neodpovídá struktuře. ({ex.Message})");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            // ZACHYTÍ JAKOUKOLI JINOU CHYBU
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Neočekávaná chyba]: {ex.Message}");
            Console.ResetColor();
        }
    }

    //PARAMETRY DEFINUJÍCÍ MĚSTA
    private static async Task Prague() => await FetchWeather("Praha", "50.07", "14.43");
    private static async Task Brno() => await FetchWeather("Brno", "49.19", "16.60");
    private static async Task Ostrava() => await FetchWeather("Ostrava", "49.83", "18.29");

    public static List<Command> GetCommands()
    {
        return
        [
            new Command("konec", "Ukončí smyčku", Exit),
            new Command("pomoc", "Ukáže možné příkazy", Help),
            new Command("vtip", "vypíše vtip (anglicky, ale o tom ani muk. :3)", Joke),
            new Command("praha", "Ukáže teplotu v Praze", Prague),
            new Command("brno", "Ukáže teplotu v Brně", Brno),
            new Command("ostrava", "ukáže teplotu v Ostravě", Ostrava)
        ];
    }

    public static async Task FetchWeather(string cityName, string latitude, string longitude)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Načítám počasí pro město {cityName}...");
        Console.ResetColor();
        string url =
            $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,apparent_temperature";

        try
        {
            var response = await Http.GetFromJsonAsync<OpenMeteoResponse>(url, JsonOpts);
            Console.WriteLine($"--- Počasí: {cityName} ---");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Aktuální teplota: {response.Current.Temperature_2m}°C");
            Console.WriteLine($"Pocitová teplota: {response.Current.ApparentTemperature}°C");
            Console.ResetColor();
            Console.WriteLine("-------------------------");
        }
        // OŠETŘENÍ CATCHEM
        catch (UriFormatException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Chyba adresy]: URL adresa pro API nebyla správně sestavena. ({ex.Message})");
            Console.ResetColor();
        }
        catch (HttpRequestException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(
                $"[Chyba sítě]: Nepodařilo se spojit se serverem Open-Meteo. Zkontrolujte internet. ({ex.Message})");
            Console.ResetColor();
            Console.WriteLine(url);
        }
        catch (JsonException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(
                $"[Chyba dat]: Odpověď ze serveru není validní JSON nebo neodpovídá struktuře. ({ex.Message})");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            // Zachytí jakoukoliv jinou neočekávanou systémovou chybu
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Neočekávaná chyba]: {ex.Message}");
            Console.ResetColor();
        }
    }
}