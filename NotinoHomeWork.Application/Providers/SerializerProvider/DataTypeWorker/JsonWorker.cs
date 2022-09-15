using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NotinoHomeWork.Application.Providers.SerializerProvider.DataTypeWorker;

public class JsonWorker : IDataTypeWorker
{
    private JsonSerializerSettings jsonSerializerSettings;
    public JsonWorker()
    {
        jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
    }

    public T Deserialize<T>(string dataToDeserialize)
    {
        var deserializedData = JsonConvert.DeserializeObject<T>(dataToDeserialize, jsonSerializerSettings);

        if (deserializedData == null)
        {
            throw new InvalidOperationException($"Cannot deserialize data from JSON to object {nameof(T)}.");
        }

        return deserializedData;
    }

    public string Serialize<T>(T dataToSerialize)
    {
        return JsonConvert.SerializeObject(dataToSerialize, jsonSerializerSettings);
    }
}
