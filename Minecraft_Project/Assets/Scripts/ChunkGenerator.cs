using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(ChunkProperties))]

public class ChunkGenerator : MonoBehaviour {
	World world = null;

	public byte [,,] cubes;


	protected Mesh visualMesh = null;
	protected MeshRenderer meshRender = null;
	protected MeshCollider meshCollider = null;
	protected MeshFilter meshFilter = null;

	protected ChunkProperties chunkProp = null;
	CubeProperties cubeType;



	// Use this for initialization
	void Start () {
		InitializeVariables ();
		if (chunkProp.betweemBiomes != true) {
			CreateChunk();
		}
	}

	public void CreateChunk(){
		InitializeChunk ();
		CreateVisualMesh ();
	}

	public void InitializeChunk()
	{
		for (int x = 0; x < world.chunkLength; x++) 
		{
			for (int z = 0; z < world.chunkLength; z++) 
			{
				float tempHeight = GetHeight(x , z) + world.groundOffset;
				int min = (int)tempHeight - Random.Range(chunkProp.stoneHeightMin,chunkProp.stoneHeightMax+1);
				for(int y = 0; y < tempHeight; y++ )
				{
					if(y < min )
						cubes[x,y,z] = (int)CubeProperties.cubeIndexes.stone;
					else 
						if(y == tempHeight -1 )
							cubes[x,y,z] = (int)CubeProperties.cubeIndexes.grass;
						else
						cubes[x,y,z] = (int)CubeProperties.cubeIndexes.dirt;
				}
			}
		}
	}

	public void CreateVisualMesh()
	{
		visualMesh = new Mesh ();

		List<Vector3> verts = new List<Vector3> ();
		List<int> tris = new List<int> ();
		List<Vector2> uvs = new List<Vector2> ();

		const int TOP = 0;
		const int SIDE = 1;
		const int BOTTOM = 2;

		for (int x = 0; x < world.chunkLength; x++) 
		{
			for (int z = 0; z < world.chunkLength; z++) 
			{
				for(int y = 0; y < world.chunkHeight; y++)
				{
					if(cubes[x, y , z] == 0) continue;

						// Check if there is a "cube" next to it and if there isnt then draw this one
					if(isCubeTransparent (x - 1 , y, z))			// Left Face
						DrawFace( x,y,z, new Vector3(x, y, z), Vector3.up , Vector3.forward , false, ref verts , ref tris , ref uvs, SIDE); 			
					if(isCubeTransparent (x + 1, y, z))				// Right Face
						DrawFace( x,y,z, new Vector3(x + 1, y, z), Vector3.up , Vector3.forward , true, ref verts , ref tris , ref uvs , SIDE); 		

					if(isCubeTransparent (x, y - 1, z) && y != 0)	// Bottom Face
						DrawFace( x,y,z, new Vector3(x, y, z), Vector3.forward , Vector3.right , false, ref verts , ref tris , ref uvs, BOTTOM); 		
					if(isCubeTransparent (x, y + 1, z))				// Top Face
						DrawFace( x,y,z, new Vector3(x, y + 1, z ), Vector3.forward , Vector3.right , true, ref verts , ref tris , ref uvs, TOP );	

					if(isCubeTransparent (x, y, z - 1))				// Back Face
						DrawFace( x,y,z, new Vector3(x, y, z), Vector3.up , Vector3.right , true, ref verts , ref tris , ref uvs, SIDE);				
					if(isCubeTransparent (x, y, z + 1))				// Forward Face
						DrawFace( x,y,z, new Vector3(x, y , z + 1), Vector3.up , Vector3.right , false, ref verts , ref tris , ref uvs, SIDE);		
				}
			}
		}

		visualMesh.vertices = verts.ToArray ();
		visualMesh.triangles = tris.ToArray ();
		visualMesh.uv = uvs.ToArray ();
		//visualMesh.RecalculateBounds ();
		visualMesh.RecalculateNormals ();
		meshFilter.mesh = visualMesh;
		meshCollider.sharedMesh = visualMesh;
	}

	public void DrawFace(int x, int y ,int z, Vector3 corner, Vector3 up, Vector3 right, bool reversed,
	                     ref List<Vector3> verts, ref List<int> tris, ref List<Vector2> uvs , int uvIndex )
	{
		int index = verts.Count;
		AssignVerts(ref verts , corner , up , right);
		AssignUvs (ref uvs, x, y, z , uvIndex);
		AssignTris (ref tris, index , reversed);

	}

	public bool isCubeTransparent(int x , int y, int z)
	{
		byte cube = GetCube (x, y, z);
		if (cube == 0)
			return true;
		return false;
	}

	public byte GetCube(int x, int y, int z)
	{
		if ((x < 0) || (y < 0) || (z < 0) || (x >= world.chunkLength) || (y >= world.chunkHeight) || (z >= world.chunkLength))
			return 0;
		return cubes [x, y, z];
	}


	void AssignVerts(ref List<Vector3> verts , Vector3 corner, Vector3 up, Vector3 right)
	{
		verts.Add (corner);
		verts.Add (corner + up);
		verts.Add (corner + up + right);
		verts.Add (corner + right);
	}

	void AssignUvs( ref List<Vector2> uvs, int x, int y, int z , int uvIndex)
	{

		Vector2 uvSize = cubeType.uvSize;
		if (cubes [x, y, z] != 0) 
		{
			Vector2 uvCorner = Vector2.Scale(cubeType.cubeDict[(CubeProperties.cubeIndexes)cubes [x, y, z]].uvIndexes[uvIndex] , uvSize);
			uvs.Add (uvCorner);
			uvs.Add (new Vector2 (uvCorner.x, uvCorner.y + uvSize.y));				
			uvs.Add (new Vector2 (uvCorner.x + uvSize.x, uvCorner.y + uvSize.y));
			uvs.Add (new Vector2 (uvCorner.x + uvSize.x, uvCorner.y));
		}
	}

	void AssignTris (ref List<int> tris, int index , bool reversed){
		if (reversed) 
		{
			tris.Add (index);
			tris.Add (index + 1);
			tris.Add (index + 2);
			//second tri
			tris.Add (index + 2);
			tris.Add (index + 3);
			tris.Add (index);
		} 
		else
		{
			tris.Add (index + 1);
			tris.Add (index);
			tris.Add (index + 2);
			//second tri
			tris.Add (index + 3);
			tris.Add (index + 2);
			tris.Add (index);
		}

	}

	float GetHeight(int x , int z)
	{
		float ceiling = 2000000;
		float PosX = transform.position.x + ceiling;
		float Posz = transform.position.z + ceiling;
		if (chunkProp.betweemBiomes)
		{
			if(chunkProp.corner != true)
			{
				if (chunkProp.xAxis)
					chunkProp.gradiantValue = SmoothGradiant (chunkProp.lowerGradiant , chunkProp.upperGradiant , x);//  left chunk gradiant to right chunk gradiant
				else
					chunkProp.gradiantValue = SmoothGradiant (chunkProp.lowerGradiant , chunkProp.upperGradiant , z ); // from bottom chunk gradiant to top chunk gradiant
			}
			else chunkProp.gradiantValue = SmoothGradiantCorner(chunkProp.lowerGradiant , chunkProp.upperGradiant , x, z) ;
		}
		return HeightGenerator.Get2DPearlinNoiseHeight (PosX + x, Posz + z, chunkProp.gradiantValue);
	}

	int SmoothGradiant(int current, int target , int index)
	{
		index += 1;
		float gradiant = (float)Mathf.Abs (current - target);
		gradiant /= 20f;
		gradiant *= index;
		if (current < target)
			gradiant += current;
		else
			gradiant = current - gradiant;
		return (int)Mathf.Round (gradiant);
	}

	int SmoothGradiantCorner(int lower, int upper , int x, int z)
	{
		if (!chunkProp.reversed)
			x = world.chunkLength - x;
		if (chunkProp.lower)
		{
			z = world.chunkLength - z;
			return (SmoothGradiant(SmoothGradiant(lower , upper , x) , lower , z));
		}
		if (z < world.chunkLength/2 )
			return (SmoothGradiant(SmoothGradiant(lower , upper , z) , upper , x));
		else return (SmoothGradiant(SmoothGradiant(lower , upper , x) , upper , z));		
	}

	void InitializeVariables ()
	{
		world = World.currentWorld;
		cubes = new byte[world.chunkLength, world.chunkHeight, world.chunkLength];

		meshRender = GetComponent<MeshRenderer> ();
		meshCollider = GetComponent<MeshCollider> ();
		meshFilter = GetComponent<MeshFilter> ();

		chunkProp = GetComponent<ChunkProperties> ();
		cubeType = CubeProperties.cubeType;
	}


}
