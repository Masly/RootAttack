using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColorChanger : MonoBehaviour
{
    public Color emptyColor;
    public Color player1Color;
    public Color player2Color;
    public Color connectedColor;
    public Color errorColor;

    MeshRenderer meshRenderer;
    MaterialPropertyBlock materialPropertyBlock;

    void Awake()
    {
        materialPropertyBlock = new MaterialPropertyBlock();
        meshRenderer = GetComponent<MeshRenderer>();
        ColorEmpty();
    }

    public void ColorEmpty()
    {
        SetColor(emptyColor);
    }


    public void ColorConnected()
    {
        SetColor(connectedColor);
    }

    public void ColorError()
    {
        SetColor(errorColor);
    }

    public void ColorFull(Player.PlayerID id)
    {
        // no need to overcomplicate rn
        if (id == Player.PlayerID.Player1)
            SetColor(player1Color);
        else
            SetColor(player2Color);
    }

    void SetColor(Color color)
    {
        meshRenderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetColor("_BaseColor", color);
        meshRenderer.SetPropertyBlock(materialPropertyBlock);
    }


}
