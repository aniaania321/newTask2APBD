namespace ConsoleApp1;

public class EmptySystemException:Exception
{
    public EmptySystemException() : base("Install operating system before turning on") { }

}