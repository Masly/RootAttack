using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{

    [SerializeField] private int playerIndex;
    public int PlayerIndex { get; set; }

    [SerializeField] private ClipList walkGround;
    public ClipList WalkGround { get; set; }

    [SerializeField] private ClipList walkSelfRoot;
    public ClipList WalkSelfRoot { get; set; }

    [SerializeField] private ClipList walkOtherRoot;
    public ClipList WalkOtherRoot { get; set; }

    [SerializeField] private ClipList bump;
    public ClipList Bump { get; set; }


    [Header("Plant Seed")]
    [SerializeField] private ClipList plantSeed;
    public ClipList PlantSeed { get; set; }

    void Start()
    {
        WalkGround = walkGround;
        WalkSelfRoot = walkSelfRoot;
        WalkOtherRoot = walkOtherRoot;
        Bump = bump;
        PlantSeed = plantSeed;
    }
}
