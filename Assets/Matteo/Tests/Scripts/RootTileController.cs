using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTileController : MonoBehaviour
{
    public RootTileData tileData;
    public TileColorChanger colorChanger;
    public List<RootTileData> connectedTiles;

    void Awake()
    {
        colorChanger = GetComponent<TileColorChanger>();
    }
    [ContextMenu("Set as Empty")]
    public void SetAsEmpty()
    {
        tileData.tileState = RootTileData.TileState.Empty;
        colorChanger.ColorEmpty();
    }
    [ContextMenu("Set as Full")]
    public void SetAsFull()
    {
        tileData.tileState = RootTileData.TileState.Full;
        colorChanger.ColorFull();
    }
    [ContextMenu("Set as Connected")]
    public void SetAsConnected()
    {
        tileData.tileState = RootTileData.TileState.Connected;
        colorChanger.ColorConnected();
    }

    [ContextMenu("Set as Enemy")]
    public void SetAsEnemy()
    {
        tileData.tileState = RootTileData.TileState.Enemy;
        colorChanger.ColorEnemy();
    }


}
