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

	void Start()
	{
		GenerateTile();
	}

	void GenerateTile()
	{

		// calculate tile depth and width based on the mesh vertices
		// calculate tile depth and width based on the mesh vertices
		Vector3[] meshVertices = this.meshFilter.mesh.vertices;
		int tileDepth = (int)Mathf.Sqrt(meshVertices.Length);
		int tileWidth = tileDepth;

		// calculate the offsets based on the tile position
		float offsetX = this.gameObject.transform.position.x;
		float offsetZ = this.gameObject.transform.position.z;
		int mapDepth = tileDepth;
		int mapWidth = tileWidth;


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



		Color[] colorMap = new Color[tileDepth * tileWidth];
		for (int zIndex = 0; zIndex < tileDepth; zIndex++)
		{
			for (int xIndex = 0; xIndex < tileWidth; xIndex++)
			{
				// transform the 2D map index is an Array index
				int colorIndex = zIndex * tileWidth + xIndex;
				float height = heightMap[zIndex, xIndex];
				// choose a terrain type according to the height value
				TerrainType terrainType = ChooseTerrainType(height);
				// assign the color according to the terrain type
				colorMap[colorIndex] = terrainType.color;
			}
		}

		// create a new texture and set its pixel colors
		Texture2D tileTexture = new Texture2D(tileWidth, tileDepth);
		tileTexture.wrapMode = TextureWrapMode.Clamp;
		tileTexture.SetPixels(colorMap);
		tileTexture.Apply();


		// generate a heightMap using noise

		this.tileRenderer.material.mainTexture = tileTexture;

		UpdateMeshVertices(heightMap);
	}


	TerrainType ChooseTerrainType(float height)
	{
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

public class NoiseMapGeneration : MonoBehaviour
{

	public float[,] GenerateNoiseMap(int mapDepth, int mapWidth, float scale, float offsetX, float offsetZ)
	{
		// create an empty noise map with the mapDepth and mapWidth coordinates
		float[,] noiseMap = new float[mapDepth, mapWidth];

		for (int zIndex = 0; zIndex < mapDepth; zIndex++)
		{
			for (int xIndex = 0; xIndex < mapWidth; xIndex++)
			{
				// calculate sample indices based on the coordinates, the scale and the offset
				float sampleX = (xIndex + offsetX) / scale;
				float sampleZ = (zIndex + offsetZ) / scale;

				// generate noise value using PerlinNoise
				float noise = Mathf.PerlinNoise(sampleX, sampleZ);

				noiseMap[zIndex, zIndex] = noise;
			}
		}

		return noiseMap;
	}
}