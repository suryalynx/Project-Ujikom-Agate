using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    [Header("Sound Effects Prefabs")]
    public GameObject[] sfxPrefabs;

    [Header("Background Music Prefab")]
    public GameObject bgmPrefab;

    private AudioSource sfxAudioSource;
    private AudioSource bgmAudioSource;
    private Dictionary<string, AudioClip> sfxDictionary = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        instance = this;

        sfxAudioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        sfxAudioSource.playOnAwake = false;
        bgmAudioSource.loop = true;
        bgmAudioSource.volume = 0.30f;

        LoadSFX();
        LoadBGM();
    }

    private void LoadSFX()
    {
        foreach (GameObject prefab in sfxPrefabs)
        {
            AudioSource audioSource = prefab.GetComponent<AudioSource>();
            if (audioSource != null && audioSource.clip != null)
            {
                sfxDictionary[prefab.name] = audioSource.clip;
            }
        }
    }

    private void LoadBGM()
    {
        if (bgmPrefab != null)
        {
            AudioSource audioSource = bgmPrefab.GetComponent<AudioSource>();
            if (audioSource != null && audioSource.clip != null)
            {
                bgmAudioSource.clip = audioSource.clip;
            }
        }
    }

    public void PlaySFX(string prefabName)
    {
        if (sfxDictionary.ContainsKey(prefabName))
        {
            sfxAudioSource.PlayOneShot(sfxDictionary[prefabName]);
        }
    }

    public void PlayBGM()
    {
        if (bgmAudioSource.clip != null)
        {
            bgmAudioSource.Play();
        }
    }

    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }

    public void PauseBGM()
    {
        bgmAudioSource.Pause();
    }

    public void ResumeBGM()
    {
        bgmAudioSource.UnPause();
    }
}
