using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "GamePrefabs")]
public class PrefabsSO : ScriptableObject
{
    [System.Serializable]
    public class PlayerPrefabs
    {
        public GameObject player1Roots;
        public GameObject player2Roots;
    }

    public PlayerPrefabs playerPrefabs;

    public GameObject debugPrefab;




}
