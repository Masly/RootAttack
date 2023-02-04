using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RootTileData
{
    public enum TileState { Empty, Full, Obstacle }
    public RootTileData(int row, int column)
    {
        this.row = row;
        this.column = column;
    }
    public int row;
    public int column;
    public bool isConnectedToTree;
    public Player.PlayerID rootOwner;

    public TileState tileState = TileState.Empty;




    public void SetCoord(int row, int column)
    {
        this.row = row;
        this.column = column;
    }
}
