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

        // SFX Player Init
        GameObject SFXObject = new GameObject("SFXPlayer");
        SFXObject.transform.parent = transform;
        SFXPlayers = new AudioSource[Channel];
        for(int i = 0; i < Channel; ++i)
        {
            SFXPlayers[i] = SFXObject.AddComponent<AudioSource>();
            SFXPlayers[i].playOnAwake = false;
            SFXPlayers[i].volume = SFXVolume;
        }
    }

    public void PlaySFX(ESFX InSFX)
    {
        // 사용 하지 않는 채널 인덱스를 구해 해당 인덱스로 오디오 실행
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
