using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColorChanger : MonoBehaviour
{
    public Color emptyColor;
    public Color fullColor;
    public Color enemyColor;
    public Color connectedColor;

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

    public void ColorFull()
    {
        SetColor(fullColor);
    }
    public void ColorConnected()
    {
        SetColor(connectedColor);
    }

    public void ColorEnemy()
    {
        SetColor(enemyColor);
    }

    void SetColor(Color color)
    {
        meshRenderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetColor("_BaseColor", color);
        meshRenderer.SetPropertyBlock(materialPropertyBlock);
    }


}
