namespace ConsoleApp1;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class DeviceManager
{
    public int limit = 15;
    public string filePath;
    public List<Device> devices;

    public DeviceManager(string FilePath)
    {
        filePath = FilePath;
        devices = new List<Device>();
        readDevices();
    }

    private void readDevices()
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("no device file found.");
            return;
        }

        foreach (var line in File.ReadLines(filePath))
        {
            try
            {
                var device = readDeviceLine(line);
                if (device != null)
                    devices.Add(device);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"invalid line: {line}");
            }
        }
    }

    private Device? readDeviceLine(string line)
    {
        string[] parts = line.Split(',');
        if (parts.Length < 3)
        {
            Console.WriteLine("invalid line: {line}");
            return null;
        }

        string[] typeAndId = parts[0].Split('-');
        if (typeAndId.Length != 2 || !int.TryParse(typeAndId[1], out int id))
        {
            Console.WriteLine("invalid line: {line}");
            return null;
        }

        string name = parts[1];

        return typeAndId[0] switch
        {
            "SW" when parts.Length > 3 && int.TryParse(parts[3].Trim('%'), out int battery) 
                => new Smartwatch(id, name, battery),

            "P" => new PersonalComputer(id, name, parts.Length > 3 ? parts[3] : null),

            "ED" when parts.Length > 4 
                => new EmbeddedDevice(id, name, parts[2], parts[3]),

            _ => throw new FormatException("wrong device line")
        };
    }

    public void add(Device device)
    {
        if (devices.Count >= limit)
        {
            Console.WriteLine("limit exceeded.");
            return;
        }

        devices.Add(device);
    }

    public void RemoveDevice(int id)
    {
        var device = devices.FirstOrDefault(d => d.Id == id);
        if (device != null)
        {
            devices.Remove(device);
            Console.WriteLine("removed succesfully");
        }
        else
        {
            Console.WriteLine("device was not found");
        }
    }

    public void edit(int id, object newValue)
    {
        var device = devices.FirstOrDefault(d => d.Id == id);
        if (device == null)
        {
            Console.WriteLine("device was not found");
            return;
        }

        switch (device)
        {
            case Smartwatch sw when newValue is int battery:
                sw.setBattery(battery);
                Console.WriteLine($"battery was updated to {battery}%");
                break;

            case PersonalComputer p when newValue is string os:
                p.installOperatingSystem(os);
                Console.WriteLine($"installed OS '{os}'");
                break;

            case EmbeddedDevice ed when newValue is string network:
                ed.network = network;
                Console.WriteLine($"updated network to '{network}'");
                break;

            default:
                Console.WriteLine("can't edit device.");
                break;
        }
    }

    public void turnOn(int id)
    {
        var device = devices.FirstOrDefault(d => d.Id == id);
        if (device == null)
        {
            Console.WriteLine($"no device was found");
            return;
        }

        try
        {
            device.TurnOn();
            Console.WriteLine($"device turned on.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"error turning on device");
        }
    }

    public void turnOff(int id)
    {
        var device = devices.FirstOrDefault(d => d.Id == id);
        if (device == null)
        {
            Console.WriteLine("no device was found");
            return;
        }

        try
        {
            device.TurnOff();
            Console.WriteLine($"device turned off.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"error turning off device {id}: {ex.Message}");
        }
    }

    public void showAll()
    {
        if (devices.Count == 0)
        {
            Console.WriteLine("No devices found.");
            return;
        }

        foreach (var device in devices)
        {
            Console.WriteLine(device);
        }
    }

    public void saveToFile()
    {
        var lines = devices.Select(device => device switch
        {
            Smartwatch sw => $"SW-{sw.Id},{sw.Name},true,{sw.battery}%",
            PersonalComputer pc => $"P-{pc.Id},{pc.Name},false,{pc.operatingSystem ?? "Not Installed"}",
            EmbeddedDevice ed => $"ED-{ed.Id},{ed.Name},{ed.ip},{ed.network}",
            _ => null
        }).Where(line => line != null).ToList();

        File.WriteAllLines(filePath, lines);
        Console.WriteLine("Devices saved successfully!.");
    }
}
