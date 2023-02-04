using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu(menuName = "RootTile")]
public class RootTile : Tile
{
    public RootTileController tileController;
    public RootTileData.TileState tileState;

    [HideInInspector] public RootTileData tileData;
    public PrefabsSO gamePrefabs;

    public override bool StartUp(Vector3Int location, ITilemap tilemap, GameObject go)
    {
        tileData = new RootTileData();
        tileController = new RootTileController(tileData);
        tileController.gamePrefabs = gamePrefabs;
        //spawner = new PrefabSpawner();

        //tileController = new RootTileController(spawner);
        Vector2Int position = new Vector2Int(location.x, location.y);
        //Debug.LogWarning(position);
        tileController.tileData.SetPosition(position);
        tileController.tileData.spawnPosition = this.transform.GetPosition();
        tileController.tileData.spawnPosition.z = -10;
        tileController.tileData.tileState = tileState;
        if (!RootMap.Instance().myTiles.ContainsKey(position))
            RootMap.Instance().myTiles.Add(position, tileController);


        return true;
    }

}
