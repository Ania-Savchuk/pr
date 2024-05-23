namespace WebApplication1.Models;

public static class DataGenerator
{
    private static readonly Random Rand = Random.Shared;
    
    public static string GenerateRandomId()
    {
        string result = string.Empty;
        for (int i = 0; i < 8; ++i)
        {
            result += Rand.Next(10).ToString();
        }
        return result;
    }
}