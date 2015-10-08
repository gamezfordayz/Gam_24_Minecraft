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
//http://answers.unity3d.com/questions/279750/loading-data-from-a-txt-file-c.html

public class Crafting : MonoBehaviour 
{
	public int[] craftingID;
	public int outputID;
	public int outputNumber;
	public TextAsset CraftingRecipiesFile;
	
	string fileName, fileLocation, tempData, tempLine;

	void Start () 
	{
		InitializeVariables ();
		//LoadXML ();
		//SaveXML ();
	}

	public int GetOutput (string input)
	{
		return GetCraftingData (input);
	}

	public int GetOutputNumber (string input)
	{
		return GetCraftingOutputNumber (input);
	}

	void InitializeVariables ()
	{
		craftingID = new int[9];
		fileLocation = Application.dataPath; 
		fileName = "CraftingRecipies.xml"; 
	}

	int GetCraftingOutputNumber(string input)
	{
		string line;
		string temp = input;
		int tempOut;
		
		StreamReader reader = File.OpenText(fileLocation + "\\" +  fileName); 
		using (reader)
		{
			do
			{
				line = reader.ReadLine();
				if(line != null)
				{
					string[] entries = line.Split(',');
					for (int i=0; i<entries.Length;i++)
					{
						//PUT IN COMPARE STRING THINGS
						if (entries[i] == temp)
						{
							//print(entries[i+1]);
							int.TryParse(entries[i+2], out tempOut);
							return tempOut;
						}
					}
				}
			}
			while (line != null);
			reader.Close();
		}
		return 1;
	}

	int GetCraftingData(string input)
	{
		string line;
		string temp = input;
		int tempOut;

		StreamReader reader = File.OpenText(fileLocation + "\\" +  fileName); 
		using (reader)
		{
			do
			{
				line = reader.ReadLine();
				if(line != null)
				{
					string[] entries = line.Split(',');
					for (int i=0; i<entries.Length;i++)
					{
						//PUT IN COMPARE STRING THINGS
						if (entries[i] == temp)
						{
							//print(entries[i+1]);
							int.TryParse(entries[i+1], out tempOut);
							return tempOut;
						}
					}
				}
			}
			while (line != null);
			reader.Close();
		}
		return 0;
	}

	public void ButtonDeleteXML()
	{
		FileInfo file = new FileInfo (fileLocation + "\\" + fileName);
		file.Delete ();
	}

	void LoadXML() 
	{ 
		FileInfo file = new FileInfo(fileLocation + "\\" + fileName); 
		if (!file.Exists)
		{
			tempData = null;
			//SaveXML();
		}
		StreamReader reader = File.OpenText(fileLocation + "\\" +  fileName); 
		string content = reader.ReadToEnd(); 
		reader.Close(); 
		tempData = content;
	} 
	
	public void SaveXML(string craftingIDs) 
	{ 
		LoadXML ();
		StreamWriter writer; 
		FileInfo file = new FileInfo(fileLocation + "\\" + fileName); 
		writer = file.CreateText(); 
		writer.Write (tempData);
		writer.Write(craftingIDs); 
		writer.Close(); 
	}
}
