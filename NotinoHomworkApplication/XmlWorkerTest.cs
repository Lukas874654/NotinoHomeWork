using NotinoHomeWork.Application.NotinoHomeWork.Models;
using NotinoHomeWork.Application.Providers.SerializerProvider.DataTypeWorker;

namespace NotinoHomworkApplication;

public class XmlWorkerTest
{
    [Fact]
    public void TEST_DeserializeObjectEqual()
    {
        string dataString = "<Document><Title>title</Title><Text>text</Text></Document>";
        Document data = new Document { Text = "text", Title = "title" };

        var xmlWorker = new XmlWorker();

        var result = xmlWorker.Deserialize<Document>(dataString);

        Assert.Equal(data.Title, result.Title);
        Assert.Equal(data.Text, result.Text);
    }

    [Fact]
    public void TEST_SerializeObjectEqual()
    {
        string dataString = "<Document>\r\n  <Title>title</Title>\r\n  <Text>text</Text>\r\n</Document>";
        Document data = new Document { Text = "text", Title = "title" };

        var xmlWorker = new XmlWorker();

        var result = xmlWorker.Serialize(data);

        Assert.Equal(dataString, result);
    }
}

