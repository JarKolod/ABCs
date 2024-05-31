public interface IDataService
{
    bool SaveData<T>(string RelativePath, T Data, bool Encrypted);
    bool LoadData<T>(string RelativePath, out T data, bool Encrypted) where T : class, new();
}