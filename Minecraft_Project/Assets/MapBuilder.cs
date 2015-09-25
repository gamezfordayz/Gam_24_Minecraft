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
			StartCoroutine(DestroyMap());
		} else {
			xOrgin = Random.Range (0f, .9999f);
			zOrgin = Random.Range (0f, .9999f);
			for (float i = 0; i < mapWidth; i++) {
				for (float j = 0; j < mapLength; j++) {
					GameObject temp = (GameObject)Instantiate (cubeFab, new Vector3 (i, GetCubeHeight (i, j, gradientValue), j), Quaternion.identity);
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

	IEnumerator DestroyMap(){
		while (mapList.Count > 0) {
			int count = 0;
			if(mapList.Count > 100){
				 count = 100;
			}else{
				count = mapList.Count;
			}
			for ( int i = count ; i > 0; i--) {
				Destroy(mapList[i]);
				mapList.RemoveAt(i);
			}
			yield return new WaitForSeconds(2f);
		}
		BuildMap();
	}

}
