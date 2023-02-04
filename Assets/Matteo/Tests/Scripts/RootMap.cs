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
    public int minGridWidth = -9;
    public int minGridHeight = -12;
    public int maxGridWidth = 8;
    public int maxGridHeight = 5;
    //[HideInInspector] List<RootTileController> myTiles = new List<RootTileController>();
    public Dictionary<Vector2Int, RootTileController> myTiles = new Dictionary<Vector2Int, RootTileController>();
    void Awake()
    {
        //SpawnTiles();
    }

    void Start()
    {
        Debug.Log($"I have {myTiles.Count} tiles");
        Invoke("ResetPosHack", 0.2f);
    }

    void ResetPosHack()
    {
        print("Starting hack");
        foreach (var tile in myTiles)
        {
            tile.Value.tileData.SetPosition(tile.Key);
        }
        print("Hack finished");
    }


    void SpawnTiles()
    {
        for (int i = 0; i < maxGridWidth; i++)
        {
            for (int j = 0; j < maxGridHeight; j++)
            {
                SpawnTile(new Vector2Int(i, j));
            }
        }
    }



    public void SpawnTile(Vector2Int pos)
    {
        /*
        Vector3 spawnPosition = new Vector3(pos.x * spawnDistance, pos.y * spawnDistance, 0);
        GameObject newTile = GameObject.Instantiate(tilePrefab, spawnPosition, Quaternion.identity, tileSpawnParent);
        RootTileController tileController = newTile.GetComponent<RootTileController>();
        tileController.tileData.SetPosition(pos);
        myTiles.Add(pos, tileController);
        */
    }

    public RootTileController GetTile(Vector2Int pos)
    {
        if (pos.x < minGridWidth || pos.x > maxGridWidth || pos.y < minGridHeight || pos.y > maxGridHeight) return null;
        RootTileController tile = myTiles[pos];
        Assert.IsNotNull(tile);
        return tile;
    }

    public List<RootTileController> GetNeighbourTiles(Vector2Int pos)
    {
        List<RootTileController> neighbours = new List<RootTileController>();
        if (pos.x < maxGridWidth - 1)
            neighbours.Add(GetTile(new Vector2Int(pos.x + 1, pos.y)));
        if (pos.x > 0)
            neighbours.Add(GetTile(new Vector2Int(pos.x - 1, pos.y)));
        if (pos.y < maxGridHeight - 1)
            neighbours.Add(GetTile(new Vector2Int(pos.x, pos.y + 1)));
        if (pos.y > 0)
            neighbours.Add(GetTile(new Vector2Int(pos.x, pos.y - 1)));
        return neighbours;
    }
}
