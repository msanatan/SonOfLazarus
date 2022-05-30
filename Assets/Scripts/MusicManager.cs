using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    private static MusicManager musicManagerInstance;
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] float menuMusicVolume = 1f;
    [SerializeField] float gameMusicVolume = 0.4f;
    AudioSource audioSource;

    void Awake()
    {
        DontDestroyOnLoad(this);
        if (musicManagerInstance == null)
        {
            musicManagerInstance = this;
            audioSource = GetComponent<AudioSource>();
        }
        else if (musicManagerInstance != this)
        {
            Destroy(musicManagerInstance.gameObject);
            musicManagerInstance = this;
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

    public void SwitchMusic(string songToPlay)
    {
        if (songToPlay == "Menu" && audioSource.clip.name != menuMusic.name)
        {
            audioSource.Stop();
            audioSource.clip = menuMusic;
            audioSource.volume = menuMusicVolume;
            audioSource.Play();
        }
        else if (songToPlay == "Game" && audioSource.clip.name != gameMusic.name)
        {
            audioSource.Stop();
            audioSource.clip = gameMusic;
            audioSource.volume = gameMusicVolume;
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
