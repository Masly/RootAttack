using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSpawner : MonoBehaviour
{
    public Player.PlayerID playerID;
    public int stepsToFill = 5;
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
        StartCoroutine(FillDirection(Vector2Int.left, row, column));
        StartCoroutine(FillDirection(Vector2Int.right, row, column));
        StartCoroutine(FillDirection(Vector2Int.up, row, column));
        StartCoroutine(FillDirection(Vector2Int.down, row, column));
    }

    IEnumerator FillDirection(Vector2Int direction, int row, int column)
    {
        Vector2Int position = new Vector2Int(row, column);
        for (int i = 0; i < stepsToFill; i++)
        {
            position += direction;
            yield return new WaitForSeconds(fillInterval);
            if (!FillIfAble(position.x, position.y)) yield break;
        }
    }

    bool FillIfAble(int row, int column)
    {
        RootTileData tileData = rootMap.GetTile(row, column)?.tileData;
        if (tileData == null) return false;
        bool isObstacle = tileData.tileState == RootTileData.TileState.Obstacle;

        if (isObstacle) return false;
        rootMap.GetTile(row, column)?.SetAsFull(playerID);
        return true;
    }


}
