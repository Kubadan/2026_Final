while (true)
{
    Console.Write("Meteo >");
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