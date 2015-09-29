using UnityEngine;
using System.Collections;

public class HeightGenerator : MonoBehaviour {
	

	public static float Get2DPearlinNoiseHeight (float xValue , float zValue , int gradientValue ){
		xValue = (xValue / World.currentWorld.chunkLength) + World.currentWorld.offsetValueX;
		zValue = (zValue / World.currentWorld.chunkLength) + World.currentWorld.offsetValueZ;
		float height = Mathf.PerlinNoise (xValue , zValue);
		height *= gradientValue;
		height = Mathf.RoundToInt (height);
		return height;
	}
}
