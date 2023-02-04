using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerID { Player1, Player2, None }
    public PlayerID playerID;

    void Update()
    {

    }

    // Vector2Int GetMapCoord(Vector3 pos)
    // {
    //     pos/=RootMap.Instance().cellSize;
    //     return new Vector2Int(Mathf.RoundToInt(pos.x),Mathf)
    // }
}
