using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("BGM")]
    public AudioClip    BGMClip;
    public float        BGMVolume;
    AudioSource         BGMPlayer;
    AudioHighPassFilter BGMEffect;

    [Header("SFX")]
    public AudioClip[]  SFXClips;
    public float        SFXVolume;
    public int          Channel;
    AudioSource[]       SFXPlayers;
    int                 ChannelIndex;

    public enum ESFX { Dead, Hit, LevelUp = 3, Lose, Melee, Range = 7, Select, Win }

    private void Awake()
    {
        Instance = this;

        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Custom
    void Init()
    {
        // BGM Player Init
        GameObject BGMObject = new GameObject("BGMPlayer");
        BGMObject.transform.parent = transform;
        BGMPlayer = BGMObject.AddComponent<AudioSource>();
        BGMPlayer.playOnAwake = false;
        BGMPlayer.loop = true;
        BGMPlayer.volume = BGMVolume;
        BGMPlayer.clip = BGMClip;
        BGMEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        // SFX Player Init
        GameObject SFXObject = new GameObject("SFXPlayer");
        SFXObject.transform.parent = transform;
        SFXPlayers = new AudioSource[Channel];
        for(int i = 0; i < Channel; ++i)
        {
            SFXPlayers[i] = SFXObject.AddComponent<AudioSource>();
            SFXPlayers[i].playOnAwake = false;
            SFXPlayers[i].bypassListenerEffects = true;
            SFXPlayers[i].volume = SFXVolume;
        }
    }

    public void PlayBGM(bool IsPlay)
    {
        if(true == IsPlay) BGMPlayer.Play();
        else BGMPlayer.Stop();
    }

    public void EffectBGM(bool IsPlay)
    {
        BGMEffect.enabled = IsPlay;
    }

    public void PlaySFX(ESFX InSFX)
    {
        // ��� ���� �ʴ� ä�� �ε����� ���� �ش� �ε����� ����� ����
        for(int i = 0; i < Channel; ++i)
        {
            int LoopIndex = (i + ChannelIndex) % Channel;
            if (true == SFXPlayers[LoopIndex].isPlaying) continue;

            int RandomIndex = 0;
            if (ESFX.Hit == InSFX || ESFX.Melee == InSFX) RandomIndex = Random.Range(0, 2);

            ChannelIndex = LoopIndex;
            SFXPlayers[ChannelIndex].clip = SFXClips[(int)InSFX + RandomIndex];
            SFXPlayers[ChannelIndex].Play();
            break;
        }

    }
    #endregion
}
