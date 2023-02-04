using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Vector3 spawnPosition;
    public void Spawn(Vector3 pos)
    {
        //Vector3 pos = RootMap.Instance().GetTile(position).tileData.spawnPosition;
        GameObject.Instantiate(prefab, pos, Quaternion.identity);
    }
}
