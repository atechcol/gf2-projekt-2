using System.Runtime.CompilerServices;
using Projekt2;

internal class Program
{
    private static DatabaseManager manager = new DatabaseManager();

    private static string Select()
    {
        Console.Write(">> ");
        return Console.ReadLine() ?? string.Empty;
    }

    private static void AddWorker()
    {
        Console.WriteLine("Hvad hedder medarbejderen? Skriv det fulde navn.");
        // Split navnet op i fornavn og efternavn ved at splitte ved et mellemrum.
        string[] fullName = Select().Split(" ");
        var (firstName, lastName) = (fullName[0], string.Join(" ", fullName[1..]));

        Console.WriteLine("""
                          Hvilken afdeling arbejder medarbejderen i? 
                          (1: Development, 2: HR, 3: Management, 4: Production, 5: IT)
                          """
        );

        string input = Select();
        int intOfInput;
        bool parsed = int.TryParse(input, out intOfInput);

        Department department = parsed switch
        {
            true => DepartmentFunctions.From(intOfInput),
            false => DepartmentFunctions.From(input),
        };
        
        if (department == Department.Unknown)
        {
            Console.WriteLine("Ugyldig afdeling, stopper!");
            return;
        }

        Worker worker = new Worker(firstName, lastName, department);
        manager.AddWorker(worker);
    }

    private static void RemoveWorker()
    {
        Console.WriteLine(
            $"""
            Vælg id'en af en af følgende medarbejdere. Skriv 'n' for at gå tilbage til menuen.
            {manager.WorkerDatabaseString()}
            """
            );
        
        string input = Select();
        if (input == "n") return;

        uint id;
        bool parsed = uint.TryParse(input, out id);
        if (parsed == false)
        {
            Console.WriteLine("Ugyldig id, stopper!");
            return;
        }
        manager.RemoveWorker(id);    
    }

    private static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Velkommen til EmployeeManagerToolkit version 2.3829.JensOlesen.BananKage");

            Ask:
            Console.WriteLine(
                """
                1: Tilføj Medarbejder
                2: Fjern Medarbejder
                3: Udskriv firmadata
                4: Afslut programmet
                """
            );
            string input = Select();
            switch (input)
            {
                case "1":
                    AddWorker();
                    break;
                case "2":
                    RemoveWorker();
                    break;
                case "3":
                    break;
                case "4":
                    Console.WriteLine("Lukker ned.");
                    return;
                default:
                    Console.WriteLine("Ugyldigt input, prøv igen!");
                    break;
            }
            Console.WriteLine(manager);
            goto Ask;
        }
    }
}