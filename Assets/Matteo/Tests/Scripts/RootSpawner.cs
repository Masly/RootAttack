using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSpawner : MonoBehaviour
{
    public Player.PlayerID playerID;
    public int stepsToFill = 5;
    RootMap rootMap;

    public Vector2Int targetPosition;
    public float fillInterval = 1f;

    void Awake()
    {
        rootMap = GetComponent<RootMap>();
    }
    [ContextMenu("Fill Target")]
    public void FillTarget()
    {
        FillTile(targetPosition);
    }

    public void FillTile(Vector2Int pos)
    {


        if (!FillIfAble(pos)) return;
        StartCoroutine(FillDirection(Vector2Int.left, pos));
        StartCoroutine(FillDirection(Vector2Int.right, pos));
        StartCoroutine(FillDirection(Vector2Int.up, pos));
        StartCoroutine(FillDirection(Vector2Int.down, pos));
    }

    IEnumerator FillDirection(Vector2Int direction, Vector2Int pos)
    {

        for (int i = 0; i < stepsToFill; i++)
        {
            pos += direction;
            yield return new WaitForSeconds(fillInterval);
            if (!FillIfAble(pos)) yield break;
        }
    }

    bool FillIfAble(Vector2Int pos)
    {
        RootTileData tileData = rootMap.GetTile(pos)?.tileData;
        if (tileData == null) return false;
        bool isObstacle = tileData.tileState == RootTileData.TileState.Obstacle;

        if (isObstacle) return false;
        rootMap.GetTile(pos)?.SetAsFull(playerID);
        return true;
    }


}
