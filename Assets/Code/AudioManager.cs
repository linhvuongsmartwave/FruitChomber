using CandyCoded.HapticFeedback;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioClip win;
    public AudioClip effect;
    public AudioClip coin;
    public AudioClip openPopup;
    public AudioClip buttonClick;
    public AudioClip drop;
    public AudioClip coinPickup;
    public AudioSource audioSoure;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        audioSoure = GetComponent<AudioSource>();
    }



    public void AudioOpen()
    {
        audioSoure.PlayOneShot(openPopup);
    }
    public void AudioDrop()
    {
        audioSoure.PlayOneShot(drop);
    }

    public void AudioButtonClick()
    {
        audioSoure.PlayOneShot(buttonClick);
    }

    public void AudioCoin()
    {
        audioSoure.PlayOneShot(coin);
    }

    public void AudioWin()
    {
        audioSoure.PlayOneShot(win);
    } 
    public void AudioEffect()
    {
        audioSoure.PlayOneShot(effect);
    } 
    public void AudioCoinPickUp()
    {
        audioSoure.PlayOneShot(coinPickup);
    }

    public void SetActive(bool isActive)
    {
        if (isActive) audioSoure.volume = 1f;
        else audioSoure.volume = 0f;
    }
    public void Vibrate()
    {
        HapticFeedback.MediumFeedback();
    }
}
