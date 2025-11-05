using System.Net;
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
        
        // |- BEREGN NAVN 
        // Split navnet op i fornavn og efternavn ved at splitte ved et mellemrum
        string[] fullName = Select().Split(" ");
        // Tag den første string som fornavn
        string firstName = fullName[0];
        // Saml de resterende strings med mellemrum mellem dem som efternavn
        string lastName = string.Join(" ", fullName[1..]);
        // BEREGN NAVN -| 

        Console.WriteLine("""
                          Hvilken afdeling arbejder medarbejderen i? Skriv enten afdelingens id eller navn. 
                          (1: Development, 2: HR, 3: Management, 4: Production, 5: IT)
                          """
        );
         
        // |- BEREGN AFDELING
        // Tag input fra brugeren
        string input = Select();
        // Lav int som TryParse skriver til
        int intOfInput;
        // Forsøg at parse input som int
        bool parsed = int.TryParse(input, out intOfInput);
        
        // Hvis parsed er falsk, kunne den ikke parse som int, og prøver i stedet at parse som string.
        // Tjek DepartmentFunctions i Department.cs for at se hvordan From virker.
        Department department = parsed switch
        {
            true => DepartmentFunctions.From(intOfInput),
            false => DepartmentFunctions.From(input),
        };
        
        // Hvis afdelingen er ukendt, stopper vi
        if (department == Department.Unknown)
        {
            Console.WriteLine("Ugyldig afdeling, stopper!");
            return;
        }
        // BEREGN AFDELING -|
        
        // Lav medarbejderen og tilføj den til databasen
        Worker worker = new Worker(firstName, lastName, department, IPAddress.None);
        manager.AddWorker(worker);
        
        Console.WriteLine($"Tilføjede {worker.FullName} til {worker.Department}.");
    }

    private static void RemoveWorker()
    {
        Console.WriteLine(
            $"""
            Vælg id'en af en af følgende medarbejdere. Skriv 'n' for at gå tilbage til menuen.
            {manager.WorkerDatabaseString()}
            """
            );
        
        // Tag input fra brugeren
        string input = Select();
        // Hvis brugeren skriver n, så stop
        if (input == "n") return;
        
        // |- BEREGN ID
        // Lav id som TryParse skriver til
        uint id;
        // Prøv at parse input som uint
        bool parsed = uint.TryParse(input, out id);
        // Hvis parsed er falsk, 
        if (!parsed)
        {
            // Hvis vi ikke kunne parse
            Console.WriteLine("Ugyldig id, stopper!");
            return;
        }
        // BEREGN ID -|
        
        // Fjern medarbejderen med id 'id' fra databasen.
        Worker worker = manager.RemoveWorker(id);
        
        Console.WriteLine($"Fjernede {worker.FullName} fra {worker.Department}.");
        
    }

    private static void Main(string[] args)
    {
        while (true)
        {
            // Startup-besked
            Console.WriteLine("== EmployeeManagerToolkit version 2.3829.JensOlesen.BananKage == Jensen Electronics (TM) ==");
            
            // Label. Lader en bruge goto. Lad være med at bruge de her, jeg synes bare det er sjovt
            Ask:
            Console.WriteLine("---------------------\n");
            Console.WriteLine(
                """
                1: Tilføj Medarbejder
                2: Fjern Medarbejder
                3: Udskriv firmadata
                4: Afslut programmet
                """
            );
            
            // Få input fra brugeren.
            string input = Select();
            // Bestem hvad der skal gøres 
            switch (input)
            {
                case "1":
                    AddWorker();
                    break;
                case "2":
                    RemoveWorker();
                    break;
                case "3":
                    if (manager.DepartmentDatabaseIsEmpty())
                    {
                        Console.WriteLine("Ingen indkodninger!");
                        break;
                    }
                    Console.WriteLine("\n");
                    Console.WriteLine(manager.DepartmentDatabaseString());
                    break;
                case "4":
                    Console.WriteLine("Lukker ned.");
                    return;
                default:
                    Console.WriteLine("Ugyldigt input, prøv igen!");
                    break;
            }
            // Gå op til der hvor Ask er og kør videre derfra
            goto Ask;
        }
    }
}