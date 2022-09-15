using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace NotinoHomeWork.Application.Providers.SerializerProvider.DataTypeWorker;

public class XmlWorker : IDataTypeWorker
{
    public T Deserialize<T>(string dataToDeserialize)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (var stringReader = new StringReader(dataToDeserialize))
        {
            var deserializedData = serializer.Deserialize(stringReader);

            if (deserializedData == default)
            {
                throw new InvalidOperationException($"Cannot deserialize data from XML to object {nameof(T)}.");
            }

            return (T)deserializedData;
        }
    }

    public string Serialize<T>(T dataToSerialize)
    {
        var settings = new XmlWriterSettings
        {
            Indent = true,
            OmitXmlDeclaration = true
        };

        var xmlNamespace = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

        using (var stream = new StringWriter()) 
        using (var xmlWriter = XmlWriter.Create(stream, settings))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(xmlWriter, dataToSerialize, xmlNamespace);

            return stream.ToString();
        }
    }
}
