using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGridManager : MonoBehaviour
{
    public List<GameObject> tileResources;
    public GameObject tileContainer;

    public int mapSizeX, mapSizeY;
    public float heightScale;
    public float[,] gridHeights;
    public GameObject[,] gridTiles;


    void Awake()
    {
        InstantiateTiles();
        // PrintGrids();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("PopulateResources")]
    public void PopulateTileList()
    {
        List<GameObject> returnArray = new List<GameObject>();

        foreach (GameObject gameObject in Resources.LoadAll("Tiles", typeof(GameObject)))
        {
            returnArray.Add(gameObject);
        }

        tileResources = returnArray;
    }

    public void GenerateHeightMap()
    {
        gridHeights = new float[mapSizeX, mapSizeY];
        if (heightScale != 0)
        {
            for (int zIndex = 0; zIndex < mapSizeY; zIndex++)
            {
                for (int xIndex = 0; xIndex < mapSizeX; xIndex++)
                {
                    // calculate sample indices based on the coordinates and the scale
                    float sampleX = xIndex / heightScale;
                    float sampleZ = zIndex / heightScale;

                    // generate noise value using PerlinNoise
                    float noise = Mathf.PerlinNoise(sampleX, sampleZ);

                    gridHeights[xIndex, zIndex] = noise;
                }
            }
        }
    }

    public void GenerateEmptyGrid()
    {
        //Creates dummy array and objects
        gridTiles = new GameObject[mapSizeX, mapSizeY];
        for (int j = 0; j < gridTiles.GetLength(1); j++)
        {
            for (int i = 0; i < gridTiles.GetLength(0); i++)
            {
                string name = i + "," + j + "-Null";
                gridTiles[i, j] = new GameObject(name);
                gridTiles[i, j].transform.parent = tileContainer.transform;
            }
        }
    }

    [ContextMenu("InstantiateTiles")]
    public void InstantiateTiles()
    {
        //Generates Height map
        GenerateHeightMap();
        //Deletes previous items
        while (tileContainer.transform.childCount > 0)
        {
            DestroyImmediate(tileContainer.transform.GetChild(0).gameObject, true);
        }
        //Creates dummy objects
        GenerateEmptyGrid();
        //calculate offset to auto-center
        float offsetX = -gridTiles.GetLength(0) / 2;
        float offsetY = -gridTiles.GetLength(1) / 2;
        //Run over grid
        for (int j = 0; j < gridTiles.GetLength(1); j++)
        {
            for (int i = 0; i < gridTiles.GetLength(0); i++)
            {
                //TODO: change to variable tile name

                //Instantiates TileDefault as child
                GameObject temp = Instantiate(tileResources.Find((x) => x.name == "TileDefault"));
                temp.transform.parent = gridTiles[i, j].transform.parent;
                //Position on Grid
                temp.transform.localPosition = new Vector3(offsetX + i, gridHeights[i, j], offsetY + j);
                temp.transform.localRotation = Quaternion.Euler(-90, 0, 0);
                //Set name
                string name = i + "," + j + "-" + "TileDefault";
                temp.name = name;
                //Write vars
                temp.GetComponent<TileVariablesScript>().posX = i;
                temp.GetComponent<TileVariablesScript>().posY = j;
                temp.GetComponent<TileVariablesScript>().posZ = Mathf.RoundToInt(gridHeights[i, j]);
                //Remove dummy object
                DestroyImmediate(gridTiles[i, j], true);
                //Saves tile to grid array
                gridTiles[i, j] = temp;
            }
        }
    }

    [ContextMenu("PrintGrids")]
    public void PrintGrids()
    {
        string output = "";
        Debug.Log("----------GridHeights:");
        for (int j = 0; j < gridHeights.GetLength(1); j++)
        {
            output = "";
            for (int i = 0; i < gridHeights.GetLength(0); i++)
            {
                output = output + gridHeights[i, j] + " ";
            }
            Debug.Log(j + ": " + output);
        }

        Debug.Log("----------GridTiles:");
        for (int j = 0; j < gridTiles.GetLength(1); j++)
        {
            output = "";
            for (int i = 0; i < gridTiles.GetLength(0); i++)
            {
                output = output + gridTiles[i, j].name + " ";
            }
            Debug.Log(j + ": " + output);
        }
    }

    public void HighlightPossibleMoves(Vector2Int actorPos, int range)
    {
        Debug.Log("Highlighting on " + actorPos.x + "," + actorPos.y + " - Range: " + range);

    }

}
