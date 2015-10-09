using UnityEngine;
using System.Collections;
using SimplexNoise;

public class HeightGenerator : MonoBehaviour {
	

	public static float Get2DPearlinNoiseHeight (float xValue , float zValue , int gradientValue ){
		xValue = (xValue / World.currentWorld.chunkLength) + World.currentWorld.offsetValueX;
		zValue = (zValue / World.currentWorld.chunkLength) + World.currentWorld.offsetValueZ;
		float height = Mathf.PerlinNoise (xValue , zValue);
		height *= gradientValue;
		height = Mathf.RoundToInt (height);
		return height;
	}

	public static float Get3DSimplexNoise (float xValue ,float yValue, float zValue ){
		xValue = (xValue / World.currentWorld.chunkLength) + World.currentWorld.offsetValueX;
		yValue = (yValue / 40) + World.currentWorld.offsetValueY;
		zValue = (zValue / World.currentWorld.chunkLength) + World.currentWorld.offsetValueZ;
		float height = Mathf.PerlinNoise (xValue , zValue);

		height = Mathf.RoundToInt (height);
		return height;
	}



}
