namespace ConsoleApp1;

public class Smartwatch:Device,IPowerNotifier
{
    public int battery;
    public Smartwatch(int id, string name, int batteryPercentage) : base(id, name)
    {
        setBattery(batteryPercentage);
    }
    public void setBattery(int percentage)
    {
        if (percentage < 0 || percentage > 100)
            throw new ArgumentException("invalid input");

        if (percentage < 20)
            lowBattery();

        battery = percentage;
    }
    public override void TurnOn()
    {
        if (battery < 11)
        {
            throw new EmptyBatteryException();
        }

        base.TurnOn();
        battery -= 10;
        
    }
    public void lowBattery()
    {
        Console.WriteLine("battery <20%");
    }

    public override string ToString()
    {
        return base.ToString() + $" battery: {battery}%";
    }


}