using NESControllerLibrary;
namespace ConsoleTester;
internal class Program
{
    static void Main(string[] args)
    {
        NESController controller = new ();
        controller.ButtonStateChanged += (object? sender, NESControllerEventArgs e) => Console.WriteLine(e);
        Console.ReadLine();
    }
}