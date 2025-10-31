

using Projekt2;

internal class Program
{
    private static void Main(string[] args)
    {
        DatabaseManager manager = new DatabaseManager();
        Worker john = new Worker("John", "Walker", Department.IT);
        Worker anna = new Worker("Anna", "Wilson", Department.IT);
        manager.AddWorker(john);
        manager.AddWorker(anna);
        Console.WriteLine(manager);
    }
}
