using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PrefabSpawner
{
    GameObject activeObject;

    public void SpawnRoot(GameObject prefab, Vector3 position)
    {
        if (activeObject != null)
            GameObject.Destroy(activeObject);
        activeObject = GameObject.Instantiate(prefab, position, Quaternion.identity);
    }

    public void SpawnRoot(RootTileController controller, Player.PlayerID id)
    {
        if (activeObject != null)
            GameObject.Destroy(activeObject);
        RootTileData tileData = controller.tileData;
        Vector3 spawnPos = new Vector3(tileData.position.x, tileData.position.y, -1);
        GameObject prefabToSpawn;
        if (id == Player.PlayerID.Player1)
            prefabToSpawn = controller.gamePrefabs.playerPrefabs.player1Roots;
        else
            prefabToSpawn = controller.gamePrefabs.playerPrefabs.player2Roots;

        activeObject = GameObject.Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }
}
