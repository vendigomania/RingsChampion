using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    [SerializeField] private AudioSource clickAudio;
    [SerializeField] private AudioSource jumpAudio;
    [SerializeField] private AudioSource winAudio;
    [SerializeField] private AudioSource loseAudio;

    [SerializeField, Header("Music")] private AudioSource musicObject;

    public static GameAudio Instance { get; private set; }

    [HideInInspector] public bool SoundIsPlaying = true;

    public bool MusicIsPlaying
    {
        get => !musicObject.mute;
        set => musicObject.mute = !value;
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void Click()
    {
        if (SoundIsPlaying) clickAudio.Play();
    }

    public void Jump()
    {
        if (SoundIsPlaying) jumpAudio.Play();
    }

    public void Lose()
    {
        if (SoundIsPlaying) loseAudio.Play();
    }

    public void Win()
    {
        if (SoundIsPlaying) winAudio.Play();
    }
}
