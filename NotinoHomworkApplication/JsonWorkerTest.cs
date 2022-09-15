using NotinoHomeWork.Application.NotinoHomeWork.Models;
using NotinoHomeWork.Application.Providers.SerializerProvider.DataTypeWorker;

namespace NotinoHomworkApplication;

public class JsonWorkerTest
{
    [Fact]
    public void TEST_DeserializeObjectEqual()
    {
        string dataString = "{\"title\":\"title\",\"text\":\"text\"}";
        Document data = new Document { Text = "text", Title = "title" };

        var jsonWorker = new JsonWorker();

        var result = jsonWorker.Deserialize<Document>(dataString);

        Assert.Equal(data.Title, result.Title);
        Assert.Equal(data.Text, result.Text);
    }

    [Fact]
    public void TEST_SerializeObjectEqual()
    {
        string dataString = "{\"title\":\"title\",\"text\":\"text\"}";
        Document data = new Document { Text = "text", Title = "title" };

        var jsonWorker = new JsonWorker();

        var result = jsonWorker.Serialize(data);

        Assert.Equal(dataString, result);
    }
}

