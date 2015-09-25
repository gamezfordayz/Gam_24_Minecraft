using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapBuilder : MonoBehaviour {

	public int mapWidth , mapLength = 0;
	public GameObject cubeFab = null;
	[Range (0, 300)]
	public int gradientValue = 0;
	float xOrgin, zOrgin = 0;
	List<GameObject> mapList = new List<GameObject>();

	public void BuildMap()
	{
		if (mapList.Count > 0) {
			DestroyMap();
		} else {
			xOrgin = Random.Range (0f, .9999f);
			zOrgin = Random.Range (0f, .9999f);
			float height = 0;
			for (float i = 0; i < mapWidth; i++) {
				for (float j = 0; j < mapLength; j++) {
					height = GetCubeHeight (i, j, gradientValue);
					GameObject temp = (GameObject)Instantiate (cubeFab, new Vector3 (i, height, j), Quaternion.identity);
					mapList.Add(temp);
				}
			}
		}
	}

	float GetCubeHeight(float xValue , float zValue , int gradientValue ){
		xValue = (xValue /  mapWidth) + xOrgin;
		zValue = (zValue / mapLength) + zOrgin;
		float height = Mathf.PerlinNoise (xValue , zValue);
		height *= gradientValue;
		height = Mathf.RoundToInt (height);
		return height;
	}

	void DestroyMap(){
		for ( int i = mapList.Count -1 ; i >= 0; i--) {
			DestroyImmediate(mapList[i]);
			mapList.RemoveAt(i);
		}
	}

	public void KillWorld(){
		GameObject[] temp = GameObject.FindGameObjectsWithTag("World");
		for (int i = temp.Length -1; i >= 0; i--) {
			DestroyImmediate(temp[i]);
		}
	}

}
