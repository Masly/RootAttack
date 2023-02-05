using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public enum PlayerID { Player1, Player2, None }
    public PlayerID playerID;

    public SquareEvent spawnEventToRaise;


    public void SpawnRoots()
    {
        Vector2Int coord = GetMapCoord(transform.position);
        spawnEventToRaise.Raise(coord);
    }

    Vector2Int GetMapCoord(Vector3 pos)
    {
        pos /= RootMap.Instance().cellSize;
        return new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
    }
}
