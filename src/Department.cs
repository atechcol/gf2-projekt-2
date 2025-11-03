namespace Projekt2;

public enum Department
{
    Development,
    HR,
    Management,
    Production,
    IT,
    Unknown,
}

public class DepartmentFunctions
{
    public static Department From(string input)
    {
        return input.ToLower() switch {
            "development" => Department.Development,
            "hr" => Department.HR,
            "management" => Department.Management,
            "production" => Department.Production,
            "it" => Department.IT,
            _ => Department.Unknown,
        };
    }

    public static Department From(int input)
    {
        if (input >= 6 || input < 1) return Department.Unknown;
        return (Department) (input - 1);
    }
}


/*
 *   Bil
 */