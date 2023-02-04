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
    [ContextMenu("Set as Player1")]
    public void SetAsPlayer1()
    {
        SetAsFull(Player.PlayerID.Player1);
    }
    [ContextMenu("Set as Player2")]
    public void SetAsPlayer2()
    {
        SetAsFull(Player.PlayerID.Player2);
    }


    public void SetAsFull(Player.PlayerID id)
    {
        tileData.tileState = RootTileData.TileState.Full;
        tileData.rootOwner = id;
        colorChanger.ColorFull();
    }
    [ContextMenu("Set as Connected")]
    public void SetAsConnected()
    {
        tileData.isConnected = true;
        colorChanger.ColorConnected();
    }




}