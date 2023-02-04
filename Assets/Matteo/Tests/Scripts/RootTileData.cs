using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RootTileData
{
    public enum TileState { Empty, Full, Obstacle }
    public RootTileData(Vector2Int position)
    {
        this.position = position;
    }
    public Vector2Int position;
    public bool isConnectedToTree;
    public Player.PlayerID rootOwner;

    public TileState tileState = TileState.Empty;




    public void SetPosition(Vector2Int pos)
    {
        position = pos;
    }
}
