 using UnityEngine;
using System.Collections;
using System.Xml;					//System.Xml contains basic XML functions and processing
using System.Xml.Serialization;		//System.Xml.Serialization serializes XML files to be formatted correctly
using System.IO;					//System.IO is used for the script 'StreamWriter' which writes to a file from a 'FileInfo' which holds information on the file.
using System.Text;

//More about System.Xml
//https://msdn.microsoft.com/en-us/library/system.xml(v=vs.110).aspx

//More about System.Xml.Serialization
//https://msdn.microsoft.com/en-us/library/system.xml.serialization(v=vs.110).aspx

//More about System.IO:
//https://msdn.microsoft.com/en-us/library/system.io%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396

//Main Tutorials found for future use
//http://wiki.unity3d.com/index.php/Save_and_Load_from_XML
//http://forum.unity3d.com/threads/saving-and-loading-data-xmlserializer.85925/
//http://wiki.unity3d.com/index.php?title=Saving_and_Loading_Data:_XmlSerializer

public class Crafting : MonoBehaviour 
{
	public string fileName, fileLocation, tempData;
	public int[] craftingID;
	public int outputID;
	public string craftingIDs;

	void Start () 
	{
		InitializeVariables ();
		LoadXML ();
		SaveXML ();
	}

	void InitializeVariables ()
	{
		craftingID = new int[9];
		fileLocation = Application.dataPath; 
		fileName = "SaveData.xml"; 
	}

	public void ButtonSaveNewXML()
	{
		for(int i=0; i < 9; i++)
		{
			craftingID[i] = (int)Random.Range(0,10);
			craftingIDs += craftingID[i].ToString() + " ";
		}
		outputID = (int)Random.Range (0, 5);
		craftingIDs += outputID.ToString() + "\n";
		SaveXML ();
	}

	public void ButtonDeleteXML()
	{
		FileInfo file = new FileInfo (fileLocation + "\\" + fileName);
		file.Delete ();
		print ("File Deleted");
	}

	void LoadXML() 
	{ 
		StreamReader reader = File.OpenText(fileLocation + "\\" +  fileName); 
		string content = reader.ReadToEnd(); 
		reader.Close(); 

		tempData = content; 
		Debug.Log (tempData);
		print("File Loaded and Read"); 
	} 
	
	public void SaveXML() 
	{ 
		StreamWriter writer; 
		FileInfo file = new FileInfo(fileLocation + "\\" + fileName); 
		if(!file.Exists) 
		{ 
			writer = file.CreateText(); 
			print("Was not there and written.");
		} 
		else 
		{ 
			//file.Delete(); 
			writer = file.CreateText(); 
			print("Was Written.");
		} 
		writer.Write (tempData);
		writer.Write(craftingIDs); 
		writer.Close(); 
	}
}
