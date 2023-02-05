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

    RootTileController player1Tree;
    RootTileController player2Tree;

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
    }

    [ContextMenu("Start tracer at tracingStart")]
    //this will only work if placed at the start of a root
    void StartTracingDebug()
    {
        RootTileController mockupTree = GetTile(tracingStart);
        //mockupTree.tileData.tileState = RootTileData.TileState.TreeOrigin;
        mockupTree.tileData.isConnectedToTree = true;
        Tracer newTracer = new Tracer(mockupTree, null);
        squareRootEvents.resetScoreEvent.Raise();
    }
    void Track(Vector2Int unusedVariable)
    {
        StartTrackingTree1();
        StartTrackingTree2();
    }

    void StartTrackingTree1()
    {
        player1Tree.tileData.isConnectedToTree = true;
        Tracer newTracer = new Tracer(player1Tree, null);
        squareRootEvents.resetScoreEvent.Raise();
    }

    void StartTrackingTree2()
    {
        player2Tree.tileData.isConnectedToTree = true;
        Tracer newTracer = new Tracer(player2Tree, null);
        squareRootEvents.resetScoreEvent.Raise();
    }

    void ResetPosHack()
    {
        print("Starting hack");
        foreach (var tile in myTiles)
        {
            tile.Value.tileData.SetPosition(tile.Key);
            if (tile.Value.tileData.tileState == RootTileData.TileState.TreeOrigin1)
                player1Tree = tile.Value;
            if (tile.Value.tileData.tileState == RootTileData.TileState.TreeOrigin2)
                player2Tree = tile.Value;
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
        //Debug.LogError("NON ERRORE importante: se le radici vanno oltre i muri controlla RotMap.GetNeighbourTiles");
        List<RootTileController> neighbours = new List<RootTileController>();
        if (pos.x < maxGridWidth)
            neighbours.Add(GetTile(new Vector2Int(pos.x + 1, pos.y)));
        if (pos.x > minGridWidth)
            neighbours.Add(GetTile(new Vector2Int(pos.x - 1, pos.y)));
        if (pos.y < maxGridHeight)
            neighbours.Add(GetTile(new Vector2Int(pos.x, pos.y + 1)));
        if (pos.y > minGridWidth)
            neighbours.Add(GetTile(new Vector2Int(pos.x, pos.y - 1)));
        for (int i = neighbours.Count - 1; i >= 0; i--)
        {
            if (neighbours[i].tileData.tileState == RootTileData.TileState.Obstacle)
                neighbours.RemoveAt(i);
        }
        return neighbours;
    }

    void OnEnable()
    {
        squareRootEvents.rootSpawnedEvent.RegisterListener(Track);
    }

    void OnDisable()
    {
        squareRootEvents.rootSpawnedEvent.UnregisterListener(Track);
    }
}
