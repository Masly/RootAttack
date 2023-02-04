using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class RootMap : MonoBehaviour
{
    private static RootMap _instance;
    public static RootMap Instance()
    {
        if (_instance == null)
            _instance = GameObject.FindObjectOfType<RootMap>();
        return _instance;
    }
    public Transform tileSpawnParent;
    public GameObject tilePrefab;
    public float spawnDistance = .2f;
    public int gridSize = 10;
    //[HideInInspector] List<RootTileController> myTiles = new List<RootTileController>();
    Dictionary<Tuple<int, int>, RootTileController> myTiles = new Dictionary<System.Tuple<int, int>, RootTileController>();
    void Awake()
    {
        SpawnTiles();
    }


    void SpawnTiles()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                SpawnTile(i, j);
            }
        }
    }

    void DeleteTiles()
    {
        foreach (KeyValuePair<Tuple<int, int>, RootTileController> tile in myTiles)
        {
            Destroy(tile.Value.gameObject);
        }
    }

    public void SpawnTile(int row, int column)
    {
        Vector3 spawnPosition = new Vector3(row * spawnDistance, column * spawnDistance, 0);
        GameObject newTile = GameObject.Instantiate(tilePrefab, spawnPosition, Quaternion.identity, tileSpawnParent);
        RootTileController tileController = newTile.GetComponent<RootTileController>();
        tileController.tileData.SetCoord(row, column);
        myTiles.Add(new Tuple<int, int>(row, column), tileController);
    }

    public RootTileController GetTile(int row, int column)
    {
        if (row < 0 || row > gridSize - 1 || column < 0 || column > gridSize - 1) return null;
        RootTileController tile = myTiles[new Tuple<int, int>(row, column)];
        Assert.IsNotNull(tile);
        return tile;
    }

    public List<RootTileController> GetNeighbourTiles(int row, int column)
    {
        List<RootTileController> neighbours = new List<RootTileController>();
        if (row < gridSize - 1)
            neighbours.Add(GetTile(row + 1, column));
        if (row > 0)
            neighbours.Add(GetTile(row - 1, column));
        if (column < gridSize - 1)
            neighbours.Add(GetTile(row, column + 1));
        if (column > 0)
            neighbours.Add(GetTile(row, column - 1));
        return neighbours;
    }
}
