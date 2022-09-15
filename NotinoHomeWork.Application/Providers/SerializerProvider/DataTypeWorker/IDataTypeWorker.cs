namespace NotinoHomeWork.Application.Providers.SerializerProvider.DataTypeWorker;

public interface IDataTypeWorker
{
    public T Deserialize<T>(string dataToDeserialize);

    public string Serialize<T>(T dataToSerialize);


}
