namespace Projekt2;

/// <summary>
/// Repræsenterer de mulige afdelinger. Mere robust end strings,
/// da det er umuligt at kunne arbejde i "Isbod"-afdelingen på
/// denne måde.
/// </summary>
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
    /// <summary>
    /// Tager en string og oversætter den til en afdeling.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Tager en int og oversætter den til en afdeling.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static Department From(int input)
    {
        if (input >= 6 || input < 1) return Department.Unknown;
        return (Department) (input - 1);
    }
}


/*
 *   Bil
 */