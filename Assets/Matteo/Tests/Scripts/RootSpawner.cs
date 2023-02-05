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

    public void Player1GrowRoots(Vector2Int coord)
    {
        FillTile(coord, Player.PlayerID.Player1);
    }

    public void Player2GrowRoots(Vector2Int coord)
    {
        FillTile(coord, Player.PlayerID.Player2);
    }

    // Legacy, DON'T USE
    [ContextMenu("Fill Target")]
    public void FillTarget()
    {
        FillTile(targetPosition, playerID);
    }
    // Legacy, DON'T USE
    public void FillTile(Vector2Int pos, Player.PlayerID id)
    {


        if (!FillIfAble(pos, id)) return;
        StartCoroutine(FillDirection(Vector2Int.left, pos, id));
        StartCoroutine(FillDirection(Vector2Int.right, pos, id));
        StartCoroutine(FillDirection(Vector2Int.up, pos, id));
        StartCoroutine(FillDirection(Vector2Int.down, pos, id));
    }

    IEnumerator FillDirection(Vector2Int direction, Vector2Int pos, Player.PlayerID id)
    {

        for (int i = 0; i < stepsToFill; i++)
        {
            pos += direction;
            yield return new WaitForSeconds(fillInterval);
            if (!FillIfAble(pos, id)) yield break;
        }
    }

    bool FillIfAble(Vector2Int pos, Player.PlayerID id)
    {
        RootTileData tileData = rootMap.GetTile(pos)?.tileData;
        if (tileData == null) return false;
        bool isObstacle = tileData.tileState == RootTileData.TileState.Obstacle;

        if (isObstacle) return false;
        rootMap.GetTile(pos)?.SetAsFull(id);
        return true;
    }


}
