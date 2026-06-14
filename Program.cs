var commands = CommandRegister.GetCommands();

// Intro
Console.WriteLine("=============================================");
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("      Vítejte v aplikaci PATRIOTERM    ");
Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("=============================================");
Console.ResetColor();
Console.WriteLine("Pro zobrazení nápovědy napište : 'pomoc'");

// Read loop
while (true)
{
    Console.Write("VLASTENEC >");
    var input = CleanInput(Console.ReadLine());
    if (input.Count == 0)
        continue;

    var command = commands.Find(c => c.Name == input[0]);
    if (command == null)
    {
        Console.WriteLine("Špatný příkaz");
        continue;
    }

    await command.Action();
}

// Rozdělení inputu na slova
List<string> CleanInput(string? input)
{
    if (input == null)
    {
        return [];
    }

    return input.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
}