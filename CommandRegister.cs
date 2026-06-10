public static class CommandRegister
{
    public static void Exit()
    {
        Console.WriteLine("Ukončuju program...");
        Environment.Exit(0);
    }

    public static void Help()
    {
        Console.WriteLine("...Právě dorazila.");
        foreach (var command in GetCommands())
        {
            Console.WriteLine(command.Name + " : " + command.Description);
        }
    }

    public static List<Command> GetCommands()
    {
        return
        [
            new Command("konec", "Ukončí smyčku", Exit),
            new Command("pomoc", "ukáže možné příkazy", Help)
        ];
    }
    
}