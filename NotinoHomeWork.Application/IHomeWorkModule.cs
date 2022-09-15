using NotinoHomeWork.Application.NotinoHomeWork.Models;
using NotinoHomeWork.Application.Providers.SerializerProvider;

namespace NotinoHomeWork.Application;

public interface IHomeWorkModule
{
    public Document DeserializeDocument(DataTypeEnum fromDataType, string data);

    public string SerializeDocument(DataTypeEnum toDataTypeEnum, Document document);

    public Task<byte[]> ReadFileFromPath(string filePath);

    public Task WriteFileToPath(string filePath, byte[] file);

    public Task<byte[]> GetDataFromUrl(Uri uri);

    public void EmailFile(string toEmail, string filePath);
}
