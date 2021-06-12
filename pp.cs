using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TerrainType
{
	public string name;
	public float height;
	public Color color;
}

[System.Serializable]
public class Wave
{
	public float seed;
	public float frequency;
	public float amplitude;
}

public class TileGeneration : MonoBehaviour
{
	[SerializeField]
	private Wave[] waves;

	[SerializeField]
	private TerrainType[] terrainTypes;

	[SerializeField]
	private MeshRenderer tileRenderer;

	[SerializeField]
	private MeshFilter meshFilter;

	[SerializeField]
	private MeshCollider meshCollider;

	[SerializeField]
	private float mapScale;

	[SerializeField]
	private float heightMultiplier;

	[SerializeField]
	private AnimationCurve heightCurve;

	[SerializeField]
	private VisualizationMode visualizationMode;

	[SerializeField]
	private TerrainType[] heightTerrainTypes;

	[SerializeField]
	private TerrainType[] heatTerrainTypes;

	private void UpdateMeshVertices(float[,] heightMap)
	{
		int tileDepth = heightMap.GetLength(0);
		int tileWidth = heightMap.GetLength(1);

		Vector3[] meshVertices = this.meshFilter.mesh.vertices;

		// iterate through all the heightMap coordinates, updating the vertex index
		int vertexIndex = 0;
		for (int zIndex = 0; zIndex < tileDepth; zIndex++)
		{
			for (int xIndex = 0; xIndex < tileWidth; xIndex++)
			{
				float height = heightMap[zIndex, xIndex];

				Vector3 vertex = meshVertices[vertexIndex];
				// change the vertex Y coordinate, proportional to the height value. The height value is evaluated by the heightCurve function, in order to correct it.
				meshVertices[vertexIndex] = new Vector3(vertex.x, this.heightCurve.Evaluate(height) * this.heightMultiplier, vertex.z);

				vertexIndex++;
			}
		}

		// update the vertices in the mesh and update its properties
		this.meshFilter.mesh.vertices = meshVertices;
		this.meshFilter.mesh.RecalculateBounds();
		this.meshFilter.mesh.RecalculateNormals();
		// update the mesh collider
		this.meshCollider.sharedMesh = this.meshFilter.mesh;
	}

	void Update()
	{
		GenerateTile(41f, 10f);
	}
	private Texture2D BuildTexture(float[,] heightMap, TerrainType[] terrainTypes)
	{
		int tileDepth = heightMap.GetLength(0);
		int tileWidth = heightMap.GetLength(1);

		Color[] colorMap = new Color[tileDepth * tileWidth];
		for (int zIndex = 0; zIndex < tileDepth; zIndex++)
		{
			for (int xIndex = 0; xIndex < tileWidth; xIndex++)
			{
				// transform the 2D map index is an Array index
				int colorIndex = zIndex * tileWidth + xIndex;
				float height = heightMap[zIndex, xIndex];
				// choose a terrain type according to the height value
				TerrainType terrainType = ChooseTerrainType(height, terrainTypes);
				// assign the color according to the terrain type
				colorMap[colorIndex] = terrainType.color;
			}
		}

		// create a new texture and set its pixel colors
		Texture2D tileTexture = new Texture2D(tileWidth, tileDepth);
		tileTexture.wrapMode = TextureWrapMode.Clamp;
		tileTexture.SetPixels(colorMap);
		tileTexture.Apply();

		return tileTexture;
	}
	void GenerateTile(float centerVertexZ, float maxDistanceZ)
	{

		// calculate tile depth and width based on the mesh vertices
		// calculate tile depth and width based on the mesh vertices
		Vector3[] meshVertices = this.meshFilter.mesh.vertices;
		int tileDepth = (int)Mathf.Sqrt(meshVertices.Length);
		int tileWidth = tileDepth;
		int mapDepth = (int)Mathf.Sqrt(meshVertices.Length);
		int mapWidth = tileDepth;

		// calculate the offsets based on the tile position
		float offsetX = this.gameObject.transform.position.x;
		float offsetZ = this.gameObject.transform.position.z;



		// create an empty noise map with the mapDepth and mapWidth coordinates
		float[,] noiseMap = new float[mapDepth, mapWidth];

		for (int zIndex = 0; zIndex < mapDepth; zIndex++)
		{
			for (int xIndex = 0; xIndex < mapWidth; xIndex++)
			{
				// calculate sample indices based on the coordinates and the scale
				float sampleX = (float)((xIndex - offsetX) / mapScale);
				float sampleZ = (float)((zIndex - offsetZ) / mapScale);

				float noise = 0f;
				float normalization = 0f;
				foreach (Wave wave in waves)
				{
					// generate noise value using PerlinNoise for a given Wave
					noise += wave.amplitude * Mathf.PerlinNoise(sampleX * wave.frequency + wave.seed, sampleZ * wave.frequency + wave.seed);
					normalization += wave.amplitude;
				}
				// normalize the noise value so that it is within 0 and 1
				noise /= normalization;

				noiseMap[zIndex, xIndex] = noise;
			}
		}
		// calculate the offsets based on the tile position
		float[,] heightMap = noiseMap;



		// calculate vertex offset based on the Tile position and the distance between vertices
		Vector3 tileDimensions = this.meshFilter.mesh.bounds.size;
		float distanceBetweenVertices = tileDimensions.z / (float)tileDepth;
		float vertexOffsetZ = this.gameObject.transform.position.z / distanceBetweenVertices;

		// generate a heatMap using uniform noise


		// create an empty noise map with the mapDepth and mapWidth coordinates
		float[,] heatMap = new float[mapDepth, mapWidth];

		for (int zIndex = 0; zIndex < mapDepth; zIndex++)
		{
			// calculate the sampleZ by summing the index and the offset
			float sampleZ = zIndex + offsetZ;
			// calculate the noise proportional to the distance of the sample to the center of the level
			float noise = Mathf.Abs(sampleZ - centerVertexZ) / maxDistanceZ;
			// apply the noise for all points with this Z coordinate
			for (int xIndex = 0; xIndex < mapWidth; xIndex++)
			{
				heatMap[mapDepth - zIndex - 1, xIndex] = noise;
			}
		}



		//color




		// build a Texture2D from the height map
		Texture2D heightTexture = BuildTexture(heightMap, this.heightTerrainTypes);
		// build a Texture2D from the heat map
		Texture2D heatTexture = BuildTexture(heatMap, this.heatTerrainTypes);

		switch (this.visualizationMode)
		{
			case VisualizationMode.Height:
				// assign material texture to be the heightTexture
				this.tileRenderer.material.mainTexture = heightTexture;
				break;
			case VisualizationMode.Heat:
				// assign material texture to be the heatTexture
				this.tileRenderer.material.mainTexture = heatTexture;
				break;
		}

		// update the tile mesh vertices according to the height map
		UpdateMeshVertices(heightMap);
	}
	enum VisualizationMode { Height, Heat }
	TerrainType ChooseTerrainType(float height, TerrainType [] terrainTypes)
	{
		Debug.Log(terrainTypes);
		
		// for each terrain type, check if the height is lower than the one for the terrain type
		foreach (TerrainType terrainType in terrainTypes)
		{
			// return the first terrain type whose height is higher than the generated one
			if (height < terrainType.height)
			{
				return terrainType;
			}
		}
		return terrainTypes[terrainTypes.Length - 1];

	}
}