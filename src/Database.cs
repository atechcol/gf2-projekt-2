using System.Text;

namespace Projekt2;

using IdType = uint;

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

internal class WorkerDatabase : Dictionary<IdType, Worker>
{
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        foreach (var (id, worker) in this)
        {
            builder.Append($"ID {id}: {worker}\n");
        }

        return builder.ToString();
    }
}

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

public class DatabaseManager
{
    private WorkerDatabase _workerDb;
    private DepartmentDatabase _departmentDb;
    private uint _counter;

    public DatabaseManager()
    {
        _workerDb = new WorkerDatabase();
        _departmentDb = new DepartmentDatabase();
        _counter = 0;
    }

    public void AddWorker(Worker worker)
    {
        // Insert in worker database
        _workerDb[_counter] = worker;
        worker.Id = _counter;
        // Update department database
        Department department = worker.Department;
        if (!_departmentDb.ContainsKey(department))
        {
            _departmentDb[department] = new WorkerList();
        }
        _departmentDb[department].Add(worker);
        // Update counter
        _counter += 1;
    }
    
    // TODO: Finish this!
    public void RemoveWorker(uint id)
    {
        // Remove from worker database
        _workerDb.Remove(id);
    }
    
    // TODO: Implement!
    public void RemoveWorker(string fullName, Department department)
    {
            
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
