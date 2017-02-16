using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Text;
using System;
using SevenZip.Compression.LZMA;

public class IntStringSizeTest {

	[Test]
	public void EditorTest() {
		//Arrange
		var gameObject = new GameObject();

        short rating = 1234;
        string ratingString = rating.ToString();

        byte[] stringBytes = Encoding.UTF8.GetBytes(ratingString);
        byte[] shortBytes = BitConverter.GetBytes(rating);                
        byte[] compressedShort = SevenZipHelper.Compress(shortBytes);
        byte[] compressedString = SevenZipHelper.Compress(stringBytes);

        Debug.Log(stringBytes.Length);
        Debug.Log(shortBytes.Length);
        Debug.Log("compressed short " + compressedShort.Length);
        Debug.Log("compressed string " + compressedString.Length);
        Debug.Log(ratingString);
        //Act
		//Try to rename the GameObject
		var newGameObjectName = "My game object";
		gameObject.name = newGameObjectName;

		//Assert
		//The object has a new name
		Assert.AreEqual(newGameObjectName, gameObject.name);
	}
}
