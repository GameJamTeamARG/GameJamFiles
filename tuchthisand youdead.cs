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


public class TileGeneration : MonoBehaviour
{


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

	void Update()
	{
		GenerateTile();
	}

	void GenerateTile()
	{
		// calculate tile depth and width based on the mesh vertices
		Vector3[] meshVertices = this.meshFilter.mesh.vertices;
		int tileDepth = (int)Mathf.Sqrt(meshVertices.Length);
		int tileWidth = tileDepth;
		int mapDepth = tileDepth;
		int mapWidth = tileWidth;

		// create an empty noise map with the mapDepth and mapWidth coordinates
		float[,] noiseMap = new float[mapDepth, mapWidth];

		for (int zIndex = 0; zIndex < mapDepth; zIndex++)
		{
			for (int xIndex = 0; xIndex < mapWidth; xIndex++)
			{
				// calculate sample indices based on the coordinates and the scale
				float sampleX = (float)xIndex / mapScale;
				float sampleZ = (float)zIndex / mapScale;

				// generate noise value using PerlinNoise
				float noise = Mathf.PerlinNoise(sampleX, sampleZ);

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






