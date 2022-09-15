using NotinoHomeWork.Application.Exceptions;
using NotinoHomeWork.Application.NotinoHomeWork.Models;
using NotinoHomeWork.Application.Providers.EmailProvider;
using NotinoHomeWork.Application.Providers.SerializerProvider;

namespace NotinoHomeWork.Application;

public class HomeWorkModule : IHomeWorkModule
{
    private IDataTypeProvider dataTypeProvider;
    private IEmailProvider emailProvider;

    public HomeWorkModule(IDataTypeProvider dataTypeProvider, IEmailProvider emailProvider)
    {
        this.dataTypeProvider = dataTypeProvider;
        this.emailProvider = emailProvider;
    }

    public Document DeserializeDocument(DataTypeEnum fromDataType, string data)
    {
        var dataTypeWorker = dataTypeProvider.GetDataTypeWorker(fromDataType);
        return dataTypeWorker.Deserialize<Document>(data);
    }

    public string SerializeDocument(DataTypeEnum toDataTypeEnum, Document document)
    {
        var dataTypeWorker = dataTypeProvider.GetDataTypeWorker(toDataTypeEnum);
        return dataTypeWorker.Serialize(document);
    }

    public async Task<byte[]> ReadFileFromPath(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new ItemNotFoundException();
        }

        return await File.ReadAllBytesAsync(filePath).ConfigureAwait(false);
    }


    public async Task WriteFileToPath(string filePath, byte[] file)
    {
        var fileDirectory = Path.GetDirectoryName(filePath);
        if (string.IsNullOrEmpty(fileDirectory))
        {
            throw new BadRequestException("Invalid path.");
        }

        Directory.CreateDirectory(fileDirectory);

        if (file.Length > 0)
        {
            await File.WriteAllBytesAsync(filePath, file).ConfigureAwait(false);
        }
    }

    public async Task<byte[]> GetDataFromUrl(Uri uri)
    {
        var httpClient = new HttpClient();
        return await httpClient.GetByteArrayAsync(uri);
    }

    public void EmailFile(string toEmail, string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new ItemNotFoundException();
        }

        emailProvider.SendFile(toEmail, filePath);
    }
}