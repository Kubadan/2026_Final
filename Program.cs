Console.WriteLine("=============================================");
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("      Vítejte v aplikaci PATRIOTERM    ");
Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("=============================================");
Console.ResetColor();
while (true)
{
    Console.Write("VLASTENEC >");
    var input = CleanInput(Console.ReadLine());
    if (input.Count == 0)
        continue;
}

List<string> CleanInput(string? input)
{
    if (input == null)
    {
        return [];
    }

    return input.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
}