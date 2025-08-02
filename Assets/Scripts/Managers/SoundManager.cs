using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public SoundManager Instance {  get { return instance; } }
    private float soundEffectVolume;
    private float musicVolume;

    private AudioSource musicAudioSource;
    public AudioClip musicClip;

    public SoundSource soundSourcePrefab;


    [SerializeField] private AudioClip buttonClip;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        PrefCheck();
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;
        musicAudioSource.Stop();
        
    }

    private void Start()
    {
        ChangeBackGroundMusic(musicClip);
    }

    public void PrefCheck()
    {
        if (PlayerPrefs.HasKey("BgmVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("BgmVolume");
        }
        else
        {
            musicVolume = 1.0f;
        }

        if (PlayerPrefs.HasKey("SfxVolume"))
        {
            soundEffectVolume = PlayerPrefs.GetFloat("SfxVolume");
        }
        else
        {
            soundEffectVolume = 1.0f;
        }
    }

    public void BgmSliderChanged(float changedData)
    {
        musicVolume = changedData;
        musicAudioSource.volume = musicVolume;
        PlayerPrefs.SetFloat("BgmVolume", musicVolume);
    }

    public void SfxSliderChanged(float changedData)
    {
        soundEffectVolume = changedData;
        PlayerPrefs.SetFloat("SfxVolume", soundEffectVolume);
    }




    public void ChangeBackGroundMusic(AudioClip clip)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    public static AudioSource PlayClip(AudioClip clip, bool loop)
    {
        if (clip == null) return null;
        SoundSource obj = Instantiate(instance.soundSourcePrefab);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        return soundSource.Play(clip, instance.soundEffectVolume, loop);
    }

    public void ButtonSound()
    {
        PlayClip(buttonClip, false);
    }
}
