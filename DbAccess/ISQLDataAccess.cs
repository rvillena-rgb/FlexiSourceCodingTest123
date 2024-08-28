namespace FlexiSourceCodingTest.DbAccess
{
    public interface ISQLDataAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "FlexiSourceCodingTestDB");
        Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "FlexiSourceCodingTestDB");
    }
}
