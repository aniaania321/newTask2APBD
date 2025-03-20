namespace ConsoleApp1;

public class EmptyBatteryException:Exception
{
    public EmptyBatteryException() : base("Battery percentage is less than 11%. Cannot turn on") { }
}