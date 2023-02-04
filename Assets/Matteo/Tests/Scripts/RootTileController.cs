using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTileController
{
    public RootTileData tileData;
    public PrefabSpawner spawner;

    public GameObject tempPrefab;

    public List<RootTileData> connectedTiles = new List<RootTileData>();

    public RootTileController()
    {
        tileData = new RootTileData();
    }
    public RootTileController(RootTileData tileData)
    {

        this.tileData = tileData;
    }
    [ContextMenu("Set as Empty")]
    public void SetAsEmpty()
    {
        tileData.tileState = RootTileData.TileState.Empty;
        //colorChanger.ColorEmpty();
    }
    [ContextMenu("Set as Player1")]
    public void SetAsPlayer1()
    {
        SetAsFull(Player.PlayerID.Player1);
    }
    [ContextMenu("Set as Player2")]
    public void SetAsPlayer2()
    {
        SetAsFull(Player.PlayerID.Player2);
    }

    public void SetAsFull(Player.PlayerID id)
    {
        tileData.tileState = RootTileData.TileState.Full;
        if (tileData.tileState == RootTileData.TileState.Full && tileData.rootOwner != id)
            BreakConnections();
        tileData.rootOwner = id;
        ConnectToAvailableNeighbours();
        Vector3 spawnPos = new Vector3(tileData.position.x, tileData.position.y, -1);
        GameObject.Instantiate(tempPrefab, spawnPos, Quaternion.identity);
        //colorChanger.ColorFull(tileData.rootOwner);

    }

    private void BreakConnections()
    {
        //
    }

    [ContextMenu("Set as Connected")]
    public void SetAsConnected()
    {
        tileData.isConnectedToTree = true;
        //colorChanger.ColorConnected();
    }

    public void ConnectToAvailableNeighbours()
    {
        //colorChanger.ColorError();
        List<RootTileController> neighbours = RootMap.Instance().GetNeighbourTiles(tileData.position);
        foreach (RootTileController neighbour in neighbours)
        {
            if (this.tileData.rootOwner == neighbour.tileData.rootOwner)
            {
                ConnectRoots(this, neighbour);

            }

        }

    }

    public void ConnectRoots(RootTileController origin, RootTileController neighbour)
    {
        if (!neighbour.connectedTiles.Contains(origin.tileData))
            neighbour.connectedTiles.Add(origin.tileData);
        if (!origin.connectedTiles.Contains(neighbour.tileData))
            origin.connectedTiles.Add(neighbour.tileData);

    }

}
