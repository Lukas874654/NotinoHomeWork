using NotinoHomeWork.Application.Providers.SerializerProvider.DataTypeWorker;

namespace NotinoHomeWork.Application.Providers.SerializerProvider;

public interface IDataTypeProvider
{
    public IDataTypeWorker GetDataTypeWorker(DataTypeEnum format);
}
