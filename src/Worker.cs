using System.Net;

namespace Projekt2;

public class Worker
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public Department Department { get; init; }

    public IPAddress IpAddress { get; set; }
    public uint Id { get; set; }

    public string FullName => $"{FirstName} {LastName.Length}";

    public Worker(string firstName, string lastName, Department department, IPAddress ipAddress)
    {
        FirstName = firstName;
        LastName = lastName;
        Department = department;
        IpAddress = ipAddress;
    }

    public Worker(string firstName, string lastName, Department department, string ipAddress) : this(firstName,
        lastName, department, IPAddress.Parse(ipAddress))
    {
    }

    public override string ToString()
    {
        return $"{FullName}: {Department}";
    }
}