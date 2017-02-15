using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using YupiPlay;
using System.Text;

[TestFixture]
public class NetworkMessageTest : AssertionHelper {
    char messageClass = 'P';
    char messageType = 'S';
    string testMessage = "mensagem de teste";    

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
}
