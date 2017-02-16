using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using YupiPlay;
using System.Text;
using SevenZip.Compression.LZMA;

[TestFixture]
public class NetworkMessageTest : AssertionHelper {
    char messageClass = 'P';
    char messageType = 'S';
    string testMessage = "500,DefaultAvatar";    

    NetworkMessage message;
    NetworkMessage receivedMessage;

    [SetUp]
    public void Init()
    {
        var testBytes = Encoding.UTF8.GetBytes(testMessage);        
        message = new NetworkMessage(messageClass, messageType, testBytes);
       
        var bytes = message.ToBytes();
        receivedMessage = new NetworkMessage(bytes);
    }

    [Test]
	public void MessageClassTest() {                              
        Debug.Log(receivedMessage.MessageClass);
        Expect(receivedMessage.MessageClass, EqualTo(messageClass));       
	}

    [Test]
    public void MessageTypeTest()
    {
        Debug.Log(receivedMessage.MessageType);
        Expect(receivedMessage.MessageType, EqualTo(messageType));        
    }

    [Test]
    public void MessageIdTest()
    {
        Debug.Log(message.MessageId.ToString());
        Debug.Log(receivedMessage.MessageId.ToString());
        Expect(receivedMessage.MessageId.ToString(), EqualTo(message.MessageId.ToString()));        
    }

    [Test]
    public void DataTest()
    {        
        string receivedTestMessage = Encoding.UTF8.GetString(receivedMessage.Data);
        Debug.Log(receivedTestMessage);
        Expect(receivedTestMessage, EqualTo(testMessage));        
    }

    [Test]
    public void DataLenghtTest()
    {
        int sentLength = Encoding.UTF8.GetBytes(testMessage).Length;
        int receivedLength = receivedMessage.Data.Length;

        Expect(receivedLength, EqualTo(sentLength));
    }

    [Test, Ignore]
    public void JsonCompressionTest()
    {
        string jsonString = message.ToJsonString();
        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);
        Debug.Log(jsonString);
        Debug.Log("uncompressed length: " + jsonBytes.Length);

        byte[] jsonBytesCompressed = SevenZipHelper.Compress(jsonBytes);
        Debug.Log("compressed length: " + jsonBytesCompressed.Length);

        Expect(jsonBytesCompressed.Length, LessThan(jsonBytes.Length));
    }

    [Test, Ignore]
    public void BytesCompressionTest()
    {
        byte[] messageBytes = message.ToBytes();
        Debug.Log("uncompressed length: " + messageBytes.Length);

        byte[] compressedMessageBytes = SevenZipHelper.Compress(messageBytes);
        Debug.Log("compressed length: " + compressedMessageBytes.Length);

        Expect(compressedMessageBytes.Length, LessThan(messageBytes.Length));
    }

    [Test]
    public void JsonFormatTest()
    {
        string jsonString = message.ToJsonString();
        var newMessage = new NetworkMessage(jsonString);

        Expect(newMessage.MessageClass, EqualTo(messageClass));
        Expect(newMessage.MessageType, EqualTo(messageType));
        Expect(newMessage.MessageId.ToString(), EqualTo(message.MessageId.ToString()));
        Expect(Encoding.UTF8.GetString(newMessage.Data), EqualTo(Encoding.UTF8.GetString(message.Data)));
    }
}
