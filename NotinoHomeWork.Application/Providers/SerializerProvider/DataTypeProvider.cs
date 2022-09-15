using NotinoHomeWork.Application.Providers.SerializerProvider.DataTypeWorker;

namespace NotinoHomeWork.Application.Providers.SerializerProvider;

public class DataTypeProvider : IDataTypeProvider
{
    public IDataTypeWorker GetDataTypeWorker(DataTypeEnum format)
    {
        switch (format)
        {
            case DataTypeEnum.Json:
                return new JsonWorker();
            case DataTypeEnum.Xml:
                return new XmlWorker();
            default:
                throw new NotImplementedException($"Parser for type {format} is not implemented.");
        }
    }
}
