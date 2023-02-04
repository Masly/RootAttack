using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager i;
    [SerializeField] private List<CharacterAudio> characterAudios;

    [Header ("Menu")]
    [SerializeField] private AudioSource menuTheme;
    [SerializeField] private AudioSource menuAction;

    [Header ("Play")]
    [SerializeField] private AudioSource victory;
    [SerializeField] private AudioSource fightTheme;

    private int groundTypeP1;
    private int groundTypeP2;
    private int groundTypeP3;
    private int groundTypeP4;

    #region GettersSetters
    public List<CharacterAudio> CharacterAudios { get => characterAudios; set => characterAudios = value; }
    public int GroundTypeP1 { get => groundTypeP1; set => groundTypeP1 = value; } // 7, 13 = concrete; 27 = grass; 28 = metal
    public int GroundTypeP2 { get => groundTypeP2; set => groundTypeP2 = value; } // 7, 13 = concrete; 27 = grass; 28 = metal
    public int GroundTypeP3 { get => groundTypeP3; set => groundTypeP3 = value; } // 7, 13 = concrete; 27 = grass; 28 = metal
    public int GroundTypeP4 { get => groundTypeP4; set => groundTypeP4 = value; } // 7, 13 = concrete; 27 = grass; 28 = metal
    public AudioSource MenuTheme { get => menuTheme; set => menuTheme = value; }
    public AudioSource MenuAction { get => menuAction; set => menuAction = value; }
    public AudioSource Victory { get => victory; set => victory = value; }
    public AudioSource FightTheme { get => fightTheme; set => fightTheme = value; }
    #endregion

    void Awake()
    {
        i = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartGameAudio();
        GroundTypeP1 = -1;
        GroundTypeP2 = -1;
        GroundTypeP3 = -1;
        GroundTypeP4 = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 1 = Play, 0 = Stop, 2 = Pause
    public void PlayClipList(ClipList audio, int playStatus = 1)
    {
        if (playStatus == 0)
        {
            audio.Source.Stop();
        }
        else if (playStatus == 1)
        {
            audio.InjectClip();
            audio.Source.Play();
        } else if (playStatus == 2)
        {
            audio.Source.Pause();
        } else
        {
            print("Play status number not recognized. Dude what are you doing");
        }
    }

    // 1 = Play, 0 = Stop, 2 = Pause
    public void PlayAudioSource(AudioSource audio, int playStatus = 1)
    {
        if (playStatus == 0)
        {
            audio.Stop();
        }
        else if (playStatus == 1)
        {
            audio.Play();
        }
        else if (playStatus == 2)
        {
            audio.Pause();
        }
        else
        {
            print("Play status number not recognized. Dude what are you doing");
        }
    }

    public void VolumeUp(AudioSource audio, float ratio, float finalVolume = 1f, bool needsToStart = true)
    {
        StartCoroutine(VolumeUpCoroutine(audio, ratio, finalVolume, needsToStart));
    }

    private IEnumerator VolumeUpCoroutine(AudioSource audio, float ratio, float finalVolume, bool needsToStart)
    {
        if (needsToStart)
        {
            audio.Play();
        }

        while (audio.volume < finalVolume)
        {
            audio.volume += ratio;
            audio.volume = Mathf.Clamp(audio.volume, 0, finalVolume);
            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }

    public void VolumeDown(AudioSource audio, float ratio, float finalVolume = 0f, bool willBeStopped = true)
    {
        StartCoroutine(VolumeDownCoroutine(audio, ratio, finalVolume, willBeStopped));
    }

    private IEnumerator VolumeDownCoroutine(AudioSource audio, float ratio, float finalVolume, bool willBeStopped = true)
    {
        float startingVolume = audio.volume;
        while(audio.volume > finalVolume)
        {
            audio.volume -=ratio;
            audio.volume = Mathf.Clamp(audio.volume, finalVolume, startingVolume);
            yield return new WaitForFixedUpdate();
        }

        if (willBeStopped)
        {
            audio.Stop();
        }
        yield return null;
    }

    // Fade out menu theme and fade in fight theme
    public void StartFightAudio()
    {
        VolumeDown(MenuTheme, 0.001f, 0, true);
        PlayAudioSource(FightTheme);
        VolumeUp(FightTheme, 0.001f, 1, false);
    }

    public void SelectWalkSoundClip(int index, bool isPlaying = true)
    {
        switch (index)
        {
            case 0:
                {
                    PlayWalkClip(GroundTypeP1, characterAudios[0]);
                    break;
                }
            case 1:
                {
                    PlayWalkClip(GroundTypeP2, characterAudios[1]);
                    break;
                }
            case 2:
                {
                    PlayWalkClip(GroundTypeP3, characterAudios[2]);
                    break;
                }
            case 3:
                {
                    PlayWalkClip(GroundTypeP4, characterAudios[3]);
                    break;
                }
            default:
                {
                    print("Player index not recognized!");
                    break;
                }
        }
    }

    public void PlayWalkClip(int groundType, CharacterAudio charaAudio)
    {
        switch (groundType)
        {
            case 7:
                PlayClipList(charaAudio.WalkGround);
                break;
            case 13:
                PlayClipList(charaAudio.WalkSelfRoot);
                break;
            case 27:
                PlayClipList(charaAudio.WalkOtherRoot);
                break;
            default:
                break;

        }
    }
} 
