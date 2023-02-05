using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RootTileData
{
    public enum TileState { Empty, Full, Obstacle, TreeOrigin }
    public RootTileData() { }

    public Vector2Int position;
    public bool isConnectedToTree;
    public Player.PlayerID rootOwner = Player.PlayerID.None;

    public TileState tileState = TileState.Empty;
    public Vector3 spawnPosition;






    public void SetPosition(Vector2Int pos)
    {
        position = pos;
    }
}
