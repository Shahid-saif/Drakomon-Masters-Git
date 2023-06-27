using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Source properties")]
    [SerializeField] AudioSource MusicAS;
    [SerializeField] AudioSource EffectAS;

    [Header("Scene Sound properties")]
    [SerializeField] List<SoundEffect> SoundEffects;


    #region Singleton
    public static SoundManager Instance;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {


    }

    public void PlayeSoundEffectWithId(string Id)
    {
        foreach (var SoundEffect in SoundEffects)
        {
            if (Id==SoundEffect.ClipId)
            {
                EffectAS.PlayOneShot(SoundEffect.Clip);
                return;
            }
        }
    }

    public void ChangeLevelMusic(AudioClip MusicClip)
    {
        MusicAS.clip = MusicClip;
        MusicAS.Play();
    }
    public void setMusicPlay(bool play)
    {
        if (play)
        {
            MusicAS.Play();
        }
        else
        {
            MusicAS.Stop();
        }
     
    }

}
