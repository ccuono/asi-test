namespace AsiTest.Business.Data.Testing;

/// <summary>
/// Generates data for initial use within the database
/// </summary>
public interface IApplicationDataSeeder
{
    void InitializeData(int contactGenerationAmount = 25, int emailMax = 5);
}