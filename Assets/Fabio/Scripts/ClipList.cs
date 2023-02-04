using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class ClipList : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clips;
    public List<AudioClip> Clips { get; set; }

    [SerializeField] private AudioSource source;
    public AudioSource Source { get; set; }

    [HideInInspector] public bool randomPitch = false;
    //public bool RandomPitch { get; set; }

    [HideInInspector] public float minPitch;
    //public float MinPitch { get; set; }

    [HideInInspector] public float maxPitch;
    //public float MaxPitch { get; set; }

    [HideInInspector] public bool randomVolume = false;
    //public bool RandomVolume { get; set; }

    [HideInInspector] public float minVolume;
    //public float MinVolume { get; set; }

    [HideInInspector] public float maxVolume;
    //public float MaxVolume { get; set; }

    private void Start()
    {
        Clips = clips;
        Source = source;
        //RandomPitch = randomPitch;
        //MinPitch = minPitch;
        //MaxPitch = maxPitch;
        //RandomVolume = randomVolume;
        //MinVolume = minVolume;
        //MaxVolume = maxVolume;
    }

    private AudioClip SelectRandomClip()
    {
        if (clips.Capacity != 1)
        {
            int randomIndex = Random.Range(0, clips.Capacity);
            return clips[randomIndex];
        }
        else
        {
            return clips[0];
        }
    }

    private float ChooseRandomPitch() // Add in inspector min and max value possible using slider
    {
        return Random.Range(minPitch, maxPitch);
    }

    private float ChooseRandomVolume()
    {
        return Random.Range(minVolume, maxVolume);
    }

    public void InjectClip()
    {
        AudioClip chosenClip = SelectRandomClip();

        if (randomPitch)
        {
            source.pitch = ChooseRandomPitch();
        }

        if (randomVolume)
        {
            source.volume = ChooseRandomVolume();
        }

        source.clip = chosenClip;

    }
}


