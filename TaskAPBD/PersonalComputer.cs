namespace ConsoleApp1;

public class PersonalComputer : Device
{
    public string? operatingSystem { get; set; }

    public PersonalComputer(int id, string name, string? system = null) : base(id, name)
    {
        operatingSystem = system;
    }
    public override void TurnOn()
    {
        if (string.IsNullOrEmpty(operatingSystem))
            throw new EmptySystemException();

        base.TurnOn();
    }


    public void installOperatingSystem(string system)
    {
        operatingSystem = system;
    }
    
    public override string ToString()
    {
        return base.ToString() + $"; OS: {(operatingSystem ?? "Not Installed")}";
    }
}