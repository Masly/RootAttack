using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSpawner : MonoBehaviour
{
    public Player.PlayerID playerID;
    RootMap rootMap;

    public int targetRow;
    public int targetColumn;

    void Awake()
    {
        rootMap = GetComponent<RootMap>();
    }
    [ContextMenu("Fill Target")]
    public void FillTarget()
    {
        FillTile(targetRow, targetColumn);
    }

    public void FillTile(int row, int column)
    {
        if (!FillIfAble(row, column)) return;
        FillLeft(row, column);
        FillRight(row, column);
        FillUp(row, column);
        FillDown(row, column);
    }

    void FillLeft(int row, int column)
    {
        for (int i = row - 1; i > 0; i--)
        {
            if (!FillIfAble(i, column)) return;

        }
    }
    void FillRight(int row, int column)
    {
        for (int i = row + 1; i < rootMap.gridSize; i++)
        {
            if (!FillIfAble(i, column)) return;
        }
    }
    void FillDown(int row, int column)
    {
        for (int j = column - 1; j > 0; j--)
        {
            if (!FillIfAble(row, j)) return;
        }
    }
    void FillUp(int row, int column)
    {
        for (int j = column + 1; j < rootMap.gridSize; j++)
        {
            if (!FillIfAble(row, j)) return;
        }
    }

    bool FillIfAble(int row, int column)
    {
        bool emptyTile = rootMap.GetTile(row, column).tileData.tileState == RootTileData.TileState.Empty;
        if (!emptyTile) return false;
        rootMap.GetTile(row, column).SetAsFull(playerID);
        return true;
    }
}
