namespace ConsoleApp1;

class Program
{
    static void Main()
    {
        string filePath = "/Users/aniasmuga/RiderProjects/TaskAPBD/TaskAPBD/input.txt";

        DeviceManager manager = new DeviceManager(filePath);

        Console.WriteLine("\ndevices:");
        manager.showAll();

        Smartwatch newWatch = new Smartwatch(8, "my smartwatch", 50);
        manager.add(newWatch);

        manager.turnOn(1);

        manager.edit(1, 90);

        manager.edit(2, "Windows 10");
        
        manager.turnOn(2);

        manager.RemoveDevice(3);

        manager.saveToFile();

        Console.WriteLine("\ndevices after changes:");
        manager.showAll();
    }
}