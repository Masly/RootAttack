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
    Dictionary<Vector2Int, RootTileController> myTiles = new Dictionary<Vector2Int, RootTileController>();
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
                SpawnTile(new Vector2Int(i, j));
            }
        }
    }

    void DeleteTiles()
    {
        foreach (KeyValuePair<Vector2Int, RootTileController> tile in myTiles)
        {
            Destroy(tile.Value.gameObject);
        }
    }

    public void SpawnTile(Vector2Int pos)
    {
        Vector3 spawnPosition = new Vector3(pos.x * spawnDistance, pos.y * spawnDistance, 0);
        GameObject newTile = GameObject.Instantiate(tilePrefab, spawnPosition, Quaternion.identity, tileSpawnParent);
        RootTileController tileController = newTile.GetComponent<RootTileController>();
        tileController.tileData.SetPosition(pos);
        myTiles.Add(pos, tileController);
    }

    public RootTileController GetTile(Vector2Int pos)
    {
        if (pos.x < 0 || pos.x > gridSize - 1 || pos.y < 0 || pos.y > gridSize - 1) return null;
        RootTileController tile = myTiles[pos];
        Assert.IsNotNull(tile);
        return tile;
    }

    public List<RootTileController> GetNeighbourTiles(Vector2Int pos)
    {
        List<RootTileController> neighbours = new List<RootTileController>();
        if (pos.x < gridSize - 1)
            neighbours.Add(GetTile(new Vector2Int(pos.x + 1, pos.y)));
        if (pos.x > 0)
            neighbours.Add(GetTile(new Vector2Int(pos.x - 1, pos.y)));
        if (pos.y < gridSize - 1)
            neighbours.Add(GetTile(new Vector2Int(pos.x, pos.y + 1)));
        if (pos.y > 0)
            neighbours.Add(GetTile(new Vector2Int(pos.x, pos.y - 1)));
        return neighbours;
    }
}
