using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raiser : MonoBehaviour
{
    public Vector2Int coord;
    public SquareEvent eventToRaise;

    [ContextMenu("Raise Event")]
    public void Raise()
    {
        eventToRaise.Raise(coord);
    }
}
