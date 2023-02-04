using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSpawner : MonoBehaviour
{
    public Player.PlayerID playerID;
    RootMap rootMap;

    public int targetRow;
    public int targetColumn;
    public float fillInterval = 1f;

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
        StartCoroutine(FillLeft(row, column));
        StartCoroutine(FillRight(row, column));
        StartCoroutine(FillUp(row, column));
        StartCoroutine(FillDown(row, column));

    }


    IEnumerator FillLeft(int row, int column)
    {
        for (int i = row - 1; i >= 0; i--)
        {
            yield return new WaitForSeconds(fillInterval);
            if (!FillIfAble(i, column)) yield break;
        }

    }
    IEnumerator FillRight(int row, int column)
    {
        for (int i = row + 1; i < rootMap.gridSize; i++)
        {
            yield return new WaitForSeconds(fillInterval);
            if (!FillIfAble(i, column)) yield break;
        }

    }
    IEnumerator FillDown(int row, int column)
    {
        for (int j = column - 1; j >= 0; j--)
        {
            yield return new WaitForSeconds(fillInterval);
            if (!FillIfAble(row, j)) yield break;
        }

    }
    IEnumerator FillUp(int row, int column)
    {
        for (int j = column + 1; j < rootMap.gridSize; j++)
        {
            yield return new WaitForSeconds(fillInterval);
            if (!FillIfAble(row, j)) yield break;
        }

    }

    bool FillIfAble(int row, int column)
    {
        RootTileData tileData = rootMap.GetTile(row, column).tileData;
        bool isObstacle = tileData.tileState == RootTileData.TileState.Obstacle;

        if (isObstacle) return false;
        rootMap.GetTile(row, column).SetAsFull(playerID);
        return true;
    }


}
