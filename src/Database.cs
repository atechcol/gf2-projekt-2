using System.Text;

namespace Projekt2;


// Vi bruger unsigned integers til at give arbejderne en id. For at lave "semantiske" typer,
// altså typer der fortæller hvad formålet med dem er, bruges et using-statement. Nok mere
// normalt i andre sprog.
using IdType = uint;

/// <summary>
/// Vi vil gerne have en liste af arbejdere. Vi laver en klasse til dette
/// der nedarver fra `List<Worker>` for at kunne definere `ToString` så det
/// printes fint i konsollen. `internal` betyder bare, at klassen kun kan
/// tilgås indenfor denne fil.
/// </summary>
internal class WorkerList : List<Worker>
{
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append('[');
        for (var i = 0; i < this.Count; i++)
        {
            var worker = this[i];
            if (i == this.Count - 1)
            {
                builder.Append($"{worker.Id}: {worker.FullName}");
                break;
            }

            builder.Append($"{worker.Id}: {worker.FullName}, ");
        }

        builder.Append(']');
        return builder.ToString();
    }
}

/// <summary>
/// Dette repræsenterer den ene del af vores database.
/// Her associerer vi en id med en `worker`, dvs. at
/// `WorkerDatabase` kan blive spurgt "giv mig arbejderen
/// med id `xyz`", og kan svare ved at give dig den arbejder.
/// `DatabaseManager` håndterer denne klasse.
/// </summary>
/// <example>
/// <code>
/// {
///   1: Worker("Anna", ...),
///   2: Worker("John", ...)
/// }
/// </code>
/// </example>
internal class WorkerDatabase : Dictionary<IdType, Worker>
{
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        foreach (var (id, worker) in this)
        {
            builder.Append($"ID {id} - {worker}\n");
        }

        return builder.ToString();
    }
}

/// <summary>
/// Dette repræsenterer den anden del af vores database.
/// Her associerer vi en afdeling med en liste af arbejdere.
/// Vi har tilføjet dette som en optimering, så vi hurtigt
/// printe data. `DatabaseManager` håndterer denne klasse.
/// </summary>
/// <example>
/// <code>
/// {
///   Department.IT: [Worker("Anna", ...), Worker("John", ...)],
///   Department.HR: [Worker("Maria", ...), Worker("Carl", ...)],
/// }
/// </code>
/// </example>
internal class DepartmentDatabase : Dictionary<Department, WorkerList>
{
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        foreach (var (department, worker) in this)
        {
            builder.Append($"{department}: {worker}\n");
        }

        return builder.ToString();
    }
}

/// <summary>
/// Udgør den centrale del af databasen. Den håndterer
/// det at tilføje og fjerne arbejdere.
/// </summary>
public class DatabaseManager
{
    private WorkerDatabase _workerDb;
    private DepartmentDatabase _departmentDb;
    // Id der tildeles den næste medarbejder der skal tilføjes, starter ved 1
    private uint _counter;
    
    // Constructor, sætter det hele op
    public DatabaseManager()
    {
        _workerDb = new WorkerDatabase();
        _departmentDb = new DepartmentDatabase();
        _counter = 1;
    }
    
    // Returnerer arbejderen med det efterspurgte id
    public Worker GetWorker(uint id)
    {
        return _workerDb[id];
    }
    
    // Tilføj en medarbejder
    public void AddWorker(Worker worker)
    {
        // |- TILFØJ TIL MEDARBEJDERDATABASEN
        // Sæt 
        _workerDb[_counter] = worker;
        worker.Id = _counter;
        // TILFØJ TIL MEDARBEJDERDATABASEN -|
        
        // |- TILFØJ TIL AFDELINGSDATABASEN
        // Find afdelingen medarbejderen arbejder i
        Department department = worker.Department;
        // Hvis afdelingen i afdelingsdatabasen ikke har en liste,
        // så tilføj en tom liste til den afdeling
        if (!_departmentDb.ContainsKey(department))
        {
            _departmentDb[department] = new WorkerList();
        }
        // Tilføj medarbejderen til listen
        _departmentDb[department].Add(worker);
        // TILFØJ TIL AFDELINGSDATABASEN -|
        
        // Opdater id-tælleren.
        _counter++;
    }
    
    // Fjern en medarbejder med id
    public Worker RemoveWorker(uint id)
    {
        // Find afdelingen medarbejderen med id'en 'id' arbejder i
        Department department = _workerDb[id].Department;
        
        // |- FJERN FRA MEDARBEJDERDATABASEN
        _workerDb.Remove(id);
        // FJERN FRA MEDARBEJDERDATABASEN -|
        
        // |- FJERN FRA AFDELINGSDATABASEN
        // Find listen over medarbejdere i afdelingen 'department'
        WorkerList departmentList = _departmentDb[department];
        // Find det index i departmentList der har id 'id'
        int index = departmentList.FindIndex(worker => worker.Id == id);
        // Fjern medarbejderen på index 'index'
        Worker worker = departmentList[index];
        departmentList.RemoveAt(index);
        // FJERN FRA AFDELINGSDATABASEN -|

        return worker;
    }
    
    public string WorkerDatabaseString()
    {
        return _workerDb.ToString();
    }
    
    public string DepartmentDatabaseString()
    {
        return _departmentDb.ToString();
    }
    
    public override string ToString()
    {
        return $"""
                Worker database:
                {_workerDb}
                Department database:
                {_departmentDb}
                """;
    }
}