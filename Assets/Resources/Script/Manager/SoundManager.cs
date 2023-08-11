using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource BGMSound;
    public AudioClip[] bgmList;

    private void Start()
    {
        BGMSoundPlay(bgmList[0]);
    }

    public void PlayNextBGM()
    {
        GameManager.instance.isPlayed = true;
        BGMSound.Stop();
        BGMSoundPlay(bgmList[1]);
    }

    public void BGMVolume(float var)
    {
        mixer.SetFloat("BGM", var);
    }

    public void SFXVolume(float var)
    {
        mixer.SetFloat("SFX", var);
    }

    // 효과음 재생
    public void SFXPlay(string sfxName, AudioSource sfxAudioSource)
    {
        GameObject sound = new GameObject(sfxName + "Sound");
        AudioSource audioSource = sound.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = sfxAudioSource.clip;
        audioSource.volume = sfxAudioSource.volume;
        audioSource.pitch = sfxAudioSource.pitch;
        audioSource.spatialBlend = sfxAudioSource.spatialBlend;
        audioSource.maxDistance = sfxAudioSource.maxDistance;
        audioSource.Play();

        Destroy(sound, sfxAudioSource.clip.length);
    }
    
    // 배경음 재생
    public void BGMSoundPlay(AudioClip clip)
    {
        BGMSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
        BGMSound.clip = clip;
        BGMSound.loop = true;
        BGMSound.volume = 0.3f;
        BGMSound.Play();
    }
}
