namespace TestProject1;
using ConsoleApp1;

public class DeviceManagerTests
{
    private const string testFile = "/Users/aniasmuga/RiderProjects/TaskAPBD/TestProject1/test.txt";
        
    [Fact]
    public void removeDevice()
    {
        File.WriteAllLines(testFile, new[] { "SW-1,Apple Watch,true,50%" });
        var deviceManager = new DeviceManager(testFile);

        deviceManager.RemoveDevice(1);

        Assert.Empty(deviceManager.devices);
    }
        
    [Fact]
    public void readDevices()
    {
        File.WriteAllLines(testFile, new[]
        {
            "SW-1,watch,true,100%",
            "P-2,myPC,false,Linux"
        });

        var deviceManager = new DeviceManager(testFile);

        Assert.Equal(2, deviceManager.devices.Count);
    }

    [Fact]
    public void editDevice()
    {
        File.WriteAllLines(testFile, new[] { "SW-1,Apple Watch2,true,90%" });
        var deviceManager = new DeviceManager(testFile);

        deviceManager.edit(1, 60);

        var device = deviceManager.devices.First() as Smartwatch;
        Assert.NotNull(device);
        Assert.Equal(60, device.battery);
    }

    [Fact]
    public void turnOnDevice()
    {
        File.WriteAllLines(testFile, new[] { "P-2,Acer,false,Minecrosoft" });
        var deviceManager = new DeviceManager(testFile);

        deviceManager.turnOn(2);

        var device = deviceManager.devices.First();
        Assert.True(device.TurnedOn);
    }
}