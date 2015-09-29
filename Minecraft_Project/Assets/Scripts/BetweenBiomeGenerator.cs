using UnityEngine;
using System.Collections;

public class BetweenBiomeGenerator : MonoBehaviour {

	public GameObject chunkFab = null;
	public int length = 0;
	public int innerBiomeGradiant = 0, outterBiomeGradiant = 0;
	//public int biomeSize = 4;
	//public int biomeOffset = 3;

	
	void Start(){
		StartCoroutine (SpawnChunks(length));
	}

	IEnumerator SpawnChunks(int length){
		for(int x = 0; x < length; x++)
		{
			for(int z = 0; z < length; z++)
			{
				if((x < 2 ) || (x > 7) || z < 2 || z > 7)
				{
					GameObject temp = (GameObject)Instantiate(chunkFab , new Vector3(x * 20f, 0 , z * 20f) , Quaternion.identity);
					temp.GetComponent<ChunkProperties>().gradiantValue = outterBiomeGradiant;
					yield return null;
				}
				else if( (x == 2 && (z != 2 && z != 7)) || (x == 7&& (z != 2 && z != 7)))
				{
					GameObject temp = (GameObject)Instantiate(chunkFab , new Vector3(x * 20f, 0 , z * 20f) , Quaternion.identity);
					temp.GetComponent<ChunkProperties>().betweemBiomes = true;
					temp.GetComponent<ChunkProperties>().xAxis = true;
					temp.GetComponent<ChunkProperties>().hasSetSides = false;
					yield return null;
				}
				else if( (z == 2 && (x != 2 && x != 7)) || (z == 7&& (x != 2 && x != 7)))
				{
					GameObject temp = (GameObject)Instantiate(chunkFab , new Vector3(x * 20f, 0 , z * 20f) , Quaternion.identity);
					temp.GetComponent<ChunkProperties>().betweemBiomes = true;
					temp.GetComponent<ChunkProperties>().xAxis = false;
					temp.GetComponent<ChunkProperties>().hasSetSides = false;
					yield return null;
				}
				else if( x == 2 && z == 2 || x == 2 && z == 7 || x == 7 && z == 2 || x == 7 && z == 7)
				{
					GameObject temp = (GameObject)Instantiate(chunkFab , new Vector3(x * 20f, 0 , z * 20f) , Quaternion.identity);
					if(x == 2 && z == 2 || x == 7 && z == 7)
						temp.GetComponent<ChunkProperties>().left = true;
					else
						temp.GetComponent<ChunkProperties>().left = false;
					if(z == 2)
						temp.GetComponent<ChunkProperties>().lower = true;
					temp.GetComponent<ChunkProperties>().corner = true;
					temp.GetComponent<ChunkProperties>().betweemBiomes = true;
					temp.GetComponent<ChunkProperties>().hasSetSides = false;
					yield return null;
				}
				else
				{
					GameObject temp = (GameObject)Instantiate(chunkFab , new Vector3(x * 20f, 0 , z * 20f) , Quaternion.identity);
					temp.GetComponent<ChunkProperties>().gradiantValue = innerBiomeGradiant;
					yield return null;
				}
			}
		}
	}

}
