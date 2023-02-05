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
    [HideInInspector] public float cellSize = 0;
    //[HideInInspector] List<RootTileController> myTiles = new List<RootTileController>();
    public Dictionary<Vector2Int, RootTileController> myTiles = new Dictionary<Vector2Int, RootTileController>();
    //remember to hide this
    public List<Tracer> tracers = new List<Tracer>();
    public List<Tracer> tracersToBeAdded = new List<Tracer>();

    public Vector2Int tracingStart;
    public SquareRootsEventsSO squareRootEvents;

    void Awake()
    {
        //SpawnTiles();
    }

    void Start()
    {
        Debug.Log($"I have {myTiles.Count} tiles");
        Invoke("ResetPosHack", 0.2f);
    }
    void Update()
    {

        List<Tracer> tracersToRemove1 = new List<Tracer>();
        List<Tracer> tracersToRemove2 = new List<Tracer>();
        foreach (Tracer tracerToBeAdded in tracersToBeAdded)
        {
            tracers.Add(tracerToBeAdded);
            tracersToRemove2.Add(tracerToBeAdded);

        }
        foreach (Tracer tracer in tracers)
        {
            if (!tracer.Tick())
                tracersToRemove1.Add(tracer);
        }
        foreach (Tracer tracerToRemove in tracersToRemove1)
        {
            tracers.Remove(tracerToRemove);
        }
        foreach (Tracer tracerToRemove in tracersToRemove2)
        {
            tracersToBeAdded.Remove(tracerToRemove);
        }
        // //loop in reverse to be able to delete items
        // for (int i = tracers.Count - 1; i >= 0; i--)
        // {
        //     if (!tracers[i].Tick())
        //         //Debug.LogWarning("I should remove tracer");
        //         tracers.RemoveAt(i);
        // }
    }

    [ContextMenu("Start tracer at tracingStart")]
    //this will only work if placed at the start of a root
    void StartTracing()
    {
        RootTileController mockupTree = GetTile(tracingStart);
        mockupTree.tileData.tileState = RootTileData.TileState.TreeOrigin;
        mockupTree.tileData.isConnectedToTree = true;
        Tracer newTracer = new Tracer(mockupTree, null);
        squareRootEvents.resetScoreEvent.Raise();
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
        if (pos.x > minGridWidth + 1)
            neighbours.Add(GetTile(new Vector2Int(pos.x - 1, pos.y)));
        if (pos.y < maxGridHeight - 1)
            neighbours.Add(GetTile(new Vector2Int(pos.x, pos.y + 1)));
        if (pos.y > minGridWidth + 1)
            neighbours.Add(GetTile(new Vector2Int(pos.x, pos.y - 1)));
        return neighbours;
    }
}
