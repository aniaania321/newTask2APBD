using System.Text.RegularExpressions;

namespace ConsoleApp1;

public class EmbeddedDevice : Device
{
    public string network { get; set; }
    public string ip { get; set; }

    public EmbeddedDevice(int id, string name, string IP, string Network) : base(id, name)
    {
        string pattern = @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$";
        Regex rg = new Regex(pattern);  
        if (!rg.IsMatch(IP))
            throw new ArgumentException("Please provide a valid IP.");

        ip = IP;
        network = Network;
    }

    public void Connect()
    {
        if (network.Contains("MD Ltd."))
            Console.WriteLine($"connected to {network}");
        else throw new ConnectionException();
    }

    public override void TurnOn()
    {
        Connect();
        base.TurnOn();
    }

    public override string ToString()
    {
        return base.ToString() + $"; IP: {ip}; Network: {network}";
    }
}