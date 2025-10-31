namespace Projekt2;

public class Worker
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public Department Department { get; init; }
    
    public uint Id { get; set; }
    
    public string FullName => $"{FirstName} {LastName}";

    public Worker(string firstName, string lastName, Department department)
    {
        FirstName = firstName;
        LastName = lastName;
        Department = department;
    }
    
    public override string ToString()
    {
        return $"{FullName}: {Department}";
    }
}