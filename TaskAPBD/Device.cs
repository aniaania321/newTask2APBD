namespace ConsoleApp1;

public abstract class Device
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool TurnedOn { get; set; }

    public Device(int id, string name)
    {
        Id = id;
        Name = name;
        TurnedOn = false;
    }

    public virtual void TurnOn(){
        TurnedOn = true;
    }

    public virtual void TurnOff(){
        TurnedOn = false;
    }
    
    public override string ToString(){
        return $"Device name: {Name} ;Device ID: {Id}; Is it turned on? {TurnedOn}";
    }
}
