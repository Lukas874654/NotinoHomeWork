using NotinoHomeWork.Application.Providers.SerializerProvider;
using NotinoHomeWork.Application.Providers.SerializerProvider.DataTypeWorker;

namespace NotinoHomworkApplication;

public class DataTypeProviderTest
{
    [Fact]
    public void TEST_GetDataTypeWorkerCorrectWorker()
    {
        var dataTypeProvider = new DataTypeProvider();

        var worker = dataTypeProvider.GetDataTypeWorker(DataTypeEnum.Xml);

        Assert.True(worker is XmlWorker);

    }

    [Fact]
    public void TEST_GetDataTypeWorkerIncorrectWorker()
    {
        var dataTypeProvider = new DataTypeProvider();

        var worker = dataTypeProvider.GetDataTypeWorker(DataTypeEnum.Json);

        Assert.False(worker is XmlWorker);
    }
}

