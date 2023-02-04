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

    [SerializeField] private ClipList walkOpponentRoot;
    public ClipList WalkOpponentRoot { get; set; }

    [SerializeField] private ClipList bump;
    public ClipList Bump { get; set; }

    [Header("Roots")]

    [SerializeField] private ClipList plantSeed;
    public ClipList PlantSeed { get; set; }

    [SerializeField] private ClipList rootGrow;
    public ClipList RootGrow { get; set; }

    [SerializeField] private ClipList rootCut;
    public ClipList RootCut { get; set; }
    

    // Start is called before the first frame update
    void Start()
    {
        WalkGround = walkGround;
        WalkSelfRoot = walkSelfRoot;
        WalkOpponentRoot = walkOpponentRoot;
        Bump = bump;
        PlantSeed = plantSeed;
        RootGrow = rootGrow;
        RootCut = rootCut;
    }
}
