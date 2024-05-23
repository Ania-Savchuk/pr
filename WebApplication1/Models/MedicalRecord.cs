namespace WebApplication1.Models;

public class MedicalRecord
{
    public string FullName { get; set; }
    public Data DateOfBirth { get; set; }
    public List<KeyValuePair<string, Data>> Appointments { get; set; } = new();

    public string GetBirthdate()
    {
        return DateOfBirth.FormatDate();
    }
}