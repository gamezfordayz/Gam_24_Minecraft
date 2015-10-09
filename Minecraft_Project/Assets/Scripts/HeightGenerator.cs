using UnityEngine;
using System.Collections;
using SimplexNoise;

public class HeightGenerator {
	

	public static float Get2DPearlinNoiseHeight (float xValue , float zValue , int gradientValue ){
		xValue = (xValue / World.currentWorld.chunkLength) + World.currentWorld.offsetValueX;
		zValue = (zValue / World.currentWorld.chunkLength) + World.currentWorld.offsetValueZ;
		float height = Mathf.PerlinNoise (xValue , zValue);
		height *= gradientValue;
		height = Mathf.RoundToInt (height);
		return height;
	}

	public static bool GetSinglePearlinNoise(float value , float offset , float cutoffValue , int length)
	{
		value = (value /length ) + offset;
		if (Mathf.PerlinNoise (value, 0f) < cutoffValue)
			return true;
		return false;
	}

	public static bool Get3DSimplexNoise (float xValue ,float yValue, float zValue , float cutoffValue){
		xValue = (xValue / World.currentWorld.chunkLength) + World.currentWorld.offsetValueX;
		yValue = (yValue / 40) + World.currentWorld.offsetValueY;
		zValue = (zValue / World.currentWorld.chunkLength) + World.currentWorld.offsetValueZ;
		float height = SimplexNoise.Noise.Generate(xValue, yValue, zValue);
		if (Mathf.Abs (height) < .08f)
			return true;
		return false;
	}



}
