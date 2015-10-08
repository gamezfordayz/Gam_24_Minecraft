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
	public bool hasBeenInitialized = false;
	bool firstPass = true;



	// Use this for initialization
	void Start () {
		InitializeVariables ();
		if (chunkProp.betweemBiomes != true) {
			StartCoroutine(CreateChunk());
		}
	}

	public IEnumerator CreateChunk(){
		while(!chunkProp.hasSetBiomeInfo)
		{
			yield return null;
		}
		InitializeChunk ();
		yield return new WaitForSeconds (.1f);
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
						cubes[x,y,z] = (byte)CubeProperties.itemIDs.stone;
					else 
						if(y == tempHeight -1 )

							cubes[x,y,z] = (byte)chunkProp.defaultGrass;
						else
						cubes[x,y,z] = (byte)chunkProp.defaultCube;
				}
			}
		}
		hasBeenInitialized = true;
	}

	public void CreateVisualMesh()
	{
		StartCoroutine (CreateVisualMeshDelayed());
	}
	public IEnumerator CreateVisualMeshDelayed()
	{
		visualMesh = new Mesh ();

		List<Vector3> verts = new List<Vector3> ();
		List<int> tris = new List<int> ();
		List<Vector2> uvs = new List<Vector2> ();

		const int TOP = 0;
		const int SIDE = 1;
		const int BOTTOM = 2;
		bool hasNotYeilded = true;
		for (int x = 0; x < world.chunkLength; x++) 
		{
			if(x > world.chunkLength/2 && hasNotYeilded)
			{
				yield return null;
				hasNotYeilded = false;
			}

			for (int z = 0; z < world.chunkLength; z++) 
			{
				for(int y = 0; y < world.chunkHeight; y++)
				{
					if(cubes[x, y , z] == 0) continue;

						// Check if there is a "cube" next to it and if there isnt then draw this one
					if(IsCubeTransparent (x - 1 , y, z))			// Left Face
						DrawFace( x,y,z, new Vector3(x, y, z), Vector3.up , Vector3.forward , false, ref verts , ref tris , ref uvs, SIDE); 			
					if(IsCubeTransparent (x + 1, y, z))				// Right Face
						DrawFace( x,y,z, new Vector3(x + 1, y, z), Vector3.up , Vector3.forward , true, ref verts , ref tris , ref uvs , SIDE); 		

					if(IsCubeTransparent (x, y - 1, z) && y != 0)	// Bottom Face
						DrawFace( x,y,z, new Vector3(x, y, z), Vector3.forward , Vector3.right , false, ref verts , ref tris , ref uvs, BOTTOM); 		
					if(IsCubeTransparent (x, y + 1, z))				// Top Face
						DrawFace( x,y,z, new Vector3(x, y + 1, z ), Vector3.forward , Vector3.right , true, ref verts , ref tris , ref uvs, TOP );	

					if(IsCubeTransparent (x, y, z - 1))				// Back Face
						DrawFace( x,y,z, new Vector3(x, y, z), Vector3.up , Vector3.right , true, ref verts , ref tris , ref uvs, SIDE);				
					if(IsCubeTransparent (x, y, z + 1))				// Forward Face
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
		if(firstPass && chunkProp.corner)
		{
			StartCoroutine(WaitAndReDraw());
			firstPass = false;
		}

	}

	IEnumerator WaitAndReDraw()
	{
		yield return new WaitForSeconds (.5f);
		CreateVisualMesh ();
		yield return new WaitForSeconds(.2f);
		world.chunks[world.FindChunk(GetChunkCoords(new Vector2(-1,0)))].GetComponent<ChunkGenerator>().CreateVisualMesh();
		yield return new WaitForSeconds(.2f);
		world.chunks[world.FindChunk(GetChunkCoords(new Vector2(0,-1)))].GetComponent<ChunkGenerator>().CreateVisualMesh();
		yield return new WaitForSeconds(.2f);
		world.chunks[world.FindChunk(GetChunkCoords(new Vector2(1,0)))].GetComponent<ChunkGenerator>().CreateVisualMesh();
		yield return new WaitForSeconds(.2f);
		world.chunks[world.FindChunk(GetChunkCoords(new Vector2(0,1)))].GetComponent<ChunkGenerator>().CreateVisualMesh();
	}

	public bool IsCubeTransparent(int x , int y, int z)
	{
		byte cube = GetCube (x, y, z);
		if (cube == 0)
			return true;
		return false;
	}

	public void DrawFace(int x, int y ,int z, Vector3 corner, Vector3 up, Vector3 right, bool reversed,
	                     ref List<Vector3> verts, ref List<int> tris, ref List<Vector2> uvs , int uvIndex )
	{
		int index = verts.Count;
		AssignVerts(ref verts , corner , up , right);
		AssignUvs (ref uvs, x, y, z , uvIndex);
		AssignTris (ref tris, index , reversed);

	}


	public byte GetCube(int x, int y, int z)
	{
		if ((x < 0) || (y < 0) || (z < 0) || (x >= world.chunkLength) || (y >= world.chunkHeight) || (z >= world.chunkLength))
			return GetTheoreticalCube(x, y, z); // if neighbor exists get thier vube if it doesnt get theoritical height
		return cubes [x, y, z];
	}

	public byte GetTheoreticalCube(int x , int y , int z)
	{
		int index = -1;
		if(x >= world.chunkLength)
			index = world.FindChunk(GetChunkCoords(new Vector2(1,0)));
		if(z >= world.chunkLength)
			index = world.FindChunk(GetChunkCoords(new Vector2(0,1)));
		if(x < 0)
			index = world.FindChunk(GetChunkCoords(new Vector2(-1,0)));
		if(z < 0)
			index = world.FindChunk(GetChunkCoords(new Vector2(0,-1)));
		
		if(index == -1 || !world.chunks [index].GetComponent<ChunkGenerator> ().hasBeenInitialized )//or if the chunk hasnt been initialized
		{
			if (y >= (GetHeight (x, z) + world.groundOffset)) 
				return 0;
			else
				return 1;
		}
		if (x >= world.chunkLength)
			x -= world.chunkLength;
		if (x < 0)
			x += world.chunkLength;
		if (z >= world.chunkLength)
			z -= world.chunkLength;
		if (z < 0)
			z += world.chunkLength;
		return world.chunks [index].GetComponent<ChunkGenerator> ().GetCube (x , y , z);


	}

	public void DestroyCube(int x, int y , int z)
	{
		byte cube = GetCube (x, y, z);
		cubes [x, y, z] = 0;
		if (x == 0 || z == 0 || z == world.chunkLength - 1 || x == world.chunkLength -1 )
		{
			if(x == 0 )
				world.chunks[world.FindChunk(GetChunkCoords(new Vector2(-1,0)))].GetComponent<ChunkGenerator>().CreateVisualMesh();
			if(z == 0 )
				world.chunks[world.FindChunk(GetChunkCoords(new Vector2(0,-1)))].GetComponent<ChunkGenerator>().CreateVisualMesh();
			if(x == world.chunkLength -1 )
				world.chunks[world.FindChunk(GetChunkCoords(new Vector2(1,0)))].GetComponent<ChunkGenerator>().CreateVisualMesh();
			if(z == world.chunkLength -1 )
				world.chunks[world.FindChunk(GetChunkCoords(new Vector2(0,1)))].GetComponent<ChunkGenerator>().CreateVisualMesh();

		}
		Vector3 spawnPos = new Vector3 (transform.position.x + x +0.5f , transform.position.y + y + 0.5f , transform.position.z + z + 0.5f);
		Instantiate (CubeProperties.cubeProperties.itemDict[(CubeProperties.itemIDs)cube].gameObjectToDrop ,spawnPos ,Quaternion.identity );
		CreateVisualMesh ();
	}

	public void CreateCube(int x , int y, int z, CubeProperties.itemIDs cubeType)
	{
		int index = -1;
		if(x >= world.chunkLength)
			index = world.FindChunk(GetChunkCoords(new Vector2(1,0)));
		else if(z >= world.chunkLength)
			index = world.FindChunk(GetChunkCoords(new Vector2(0,1)));
		else if(x < 0)
			index = world.FindChunk(GetChunkCoords(new Vector2(-1,0)));
		else if(z < 0)
			index = world.FindChunk(GetChunkCoords(new Vector2(0,-1)));
		if (x >= world.chunkLength)
			x -= world.chunkLength;
		else if (x < 0)
			x += world.chunkLength;
		else if (z >= world.chunkLength)
			z -= world.chunkLength;
		else if (z < 0)
			z += world.chunkLength;
		if(index != -1)
		{
			if(world.chunks [index].GetComponent<ChunkGenerator> ().GetCube (x , y , z) == 0 )
			{
				world.chunks [index].GetComponent<ChunkGenerator> ().CreateCube (x , y , z , cubeType);
			}
		}
		else
		{
			if (GetCube (x, y, z) == 0) 
			{
				cubes [x, y, z] = (byte)cubeType;
				CreateVisualMesh ();
			}
		}
	}

	public Vector3 GetChunkCoords(Vector2 dir)
	{
		return new Vector3(transform.position.x + dir.x*(world.chunkLength) , transform.position.y ,transform.position.z + dir.y*(world.chunkLength) );
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
		float offset = .004f;
		//float heightOffset = .005f;
		Vector2 uvSize = cubeType.uvSize;
		if (cubes [x, y, z] != 0) 
		{

			Vector2 uvCorner = Vector2.Scale(cubeType.itemDict[(CubeProperties.itemIDs)cubes [x, y, z]].uvIndexes[uvIndex] , uvSize);
			uvs.Add (uvCorner + new Vector2(offset , offset));
			uvs.Add (new Vector2 (uvCorner.x + offset, uvCorner.y + uvSize.y - offset));				
			uvs.Add (new Vector2 (uvCorner.x + uvSize.x - offset, uvCorner.y + uvSize.y - offset));
			uvs.Add (new Vector2 (uvCorner.x + uvSize.x -offset, uvCorner.y + offset));
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
		int tempGradiant = chunkProp.gradiantValue;
		if (chunkProp.betweemBiomes)
		{
			if(chunkProp.corner != true)
			{
				if (chunkProp.xAxis)
					tempGradiant = SmoothGradiant (chunkProp.lowerGradiant , chunkProp.upperGradiant , x , 20 , 0);//  left chunk gradiant to right chunk gradiant
				else
					tempGradiant = SmoothGradiant (chunkProp.lowerGradiant , chunkProp.upperGradiant , z , 20 , 0); // from bottom chunk gradiant to top chunk gradiant
			}
			else tempGradiant = SmoothGradiantCorner(chunkProp.bottomLeft,chunkProp.bottomRight , chunkProp.topLeft , chunkProp.topRight , x, z) ;
		}
		return HeightGenerator.Get2DPearlinNoiseHeight (PosX + x, Posz + z, tempGradiant);
	}

	int SmoothGradiant(int startGradiant, int targetGradiant , int index ,  int length , int startingAt)
	{
		//index += 1;
		index -= startingAt;
		float gradiant = targetGradiant - startGradiant;
		gradiant /= length;
		gradiant *= index;
		gradiant += startGradiant;
		return (int)Mathf.Round (gradiant);
	}

	int SmoothGradiantCorner(int bL , int bR, int tL, int tR , int x, int z)
	{
		int xGradiant = -1;
		int zGradiant = -1;
		z += 1;
		x += 1;
		xGradiant = Mathf.RoundToInt(((float)SmoothGradiant(SmoothGradiant(bL , tL , z ,20 , 0) , SmoothGradiant(bR , tR , z ,20 , 0) , x , 20 ,0) * ((float)x/ 20f)));
		zGradiant = Mathf.RoundToInt(((float)SmoothGradiant(SmoothGradiant(bL , bR , x ,20 , 0) , SmoothGradiant(tL , tR , x ,20 , 0) , z ,20 ,0)* ((float)z/ 20f)));
		return Mathf.RoundToInt(((xGradiant + zGradiant) / ((float)x/20f + (float)z/20f))) ;
	}

	void InitializeVariables ()
	{
		world = World.currentWorld;
		cubes = new byte[world.chunkLength, world.chunkHeight, world.chunkLength];

		meshRender = GetComponent<MeshRenderer> ();
		meshCollider = GetComponent<MeshCollider> ();
		meshFilter = GetComponent<MeshFilter> ();

		chunkProp = GetComponent<ChunkProperties> ();
		cubeType = CubeProperties.cubeProperties;
	}

	// ok if the byte im looking for is off my grid then find the byte i need by calling get virtual .. this will check the height and see if there is a cube at that pos
}
