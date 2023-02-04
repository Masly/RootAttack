using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{

    [SerializeField] private int playerIndex;
    public int PlayerIndex { get; set; }

    [SerializeField] private ClipList walkConcrete;
    public ClipList WalkConcrete { get; set; }

    [SerializeField] private ClipList walkGrass;
    public ClipList WalkGrass { get; set; }

    [SerializeField] private ClipList walkMetal;
    public ClipList WalkMetal { get; set; }

    [SerializeField] private ClipList bump;
    public ClipList Bump { get; set; }

    //[SerializeField] private ClipList jump;
    //public ClipList Jump { get; set; }

    //[SerializeField] private ClipList doubleJump;
    //public ClipList DoubleJump { get; set; }

    [SerializeField] private ClipList getHit;
    public ClipList GetHit { get; set; }

    //[SerializeField] private ClipList bounce;
    //public ClipList Bounce { get; set; }

    //[SerializeField] private ClipList stop;
    //public ClipList Stop { get; set; }

    //[SerializeField] private ClipList land;
    //public ClipList Land { get; set; }

    //[SerializeField] private ClipList fall;
    //public ClipList Fall { get; set; }

    //[SerializeField] private ClipList stun;
    //public ClipList Stun { get; set; }

    //[SerializeField] private ClipList collect;
    //public ClipList Collect { get; set; }

    //[SerializeField] private ClipList loseAura;
    //public ClipList LoseAura { get; set; }

    //[SerializeField] private ClipList gainAura;
    //public ClipList GainAura { get; set; }


    [Header("Plant Seed")]

    [SerializeField] private ClipList plantSeed;
    public ClipList PlantSeed { get; set; }
    /*[SerializeField] private ClipList weaponMaster;
    public ClipList WeaponMaster { get; set; }

    [SerializeField] private ClipList weaponAttack;
    public ClipList WeaponAttack { get; set; }

    [SerializeField] private ClipList weaponRecatch;
    public ClipList WeaponRecatch { get; set; }

    [SerializeField] private ClipList weaponCharge;
    public ClipList WeaponCharge { get; set; }

    [SerializeField] private ClipList weaponHitCollectable;
    public ClipList WeaponHitCollectable { get; set; }

    [SerializeField] private ClipList weaponHitCharacter;
    public ClipList WeaponHitCharacter { get; set; }

    [SerializeField] private ClipList weaponOngoing;
    public ClipList WeaponOngoing { get; set; }

    [SerializeField] private ClipList weaponChargeMax;
    public ClipList WeaponChargeMax { get; set; }*/

    // Start is called before the first frame update
    void Start()
    {
        WalkGrass = walkGrass;
        WalkConcrete = walkConcrete;
        WalkMetal = walkMetal;
        Bump = bump;
        PlantSeed = plantSeed;
    }
}
