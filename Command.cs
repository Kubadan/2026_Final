using OpenMeteo;
public delegate Task CommandFunc();

public class Command
{
    public string Name { get; set; }
    public string Description { get; set; }
    public CommandFunc Action { get; set; }

    public Command(string name, string description, CommandFunc action)
    {
        Name = name;
        Description = description;
        Action = action;
    }
}