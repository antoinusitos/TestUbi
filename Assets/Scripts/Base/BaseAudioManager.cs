using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAudioManager : MonoBehaviour
{
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip fireSound;
    public AudioClip dieSound;
    public AudioClip startSound;
    public AudioClip playerDieSound;

    public enum audioToPlay
    {
        win,
        lose,
        fire,
        die,
        start,
        playerDie,
    };

    private static BaseAudioManager _instance;

    public static BaseAudioManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }

    public void PlaySound(audioToPlay toPlay)
    {
        switch (toPlay)
        {
            case audioToPlay.win:
                GetComponent<AudioSource>().PlayOneShot(winSound);
                break;
            case audioToPlay.lose:
                GetComponent<AudioSource>().PlayOneShot(loseSound);
                break;
            case audioToPlay.fire:
                GetComponent<AudioSource>().PlayOneShot(fireSound);
                break;
            case audioToPlay.die:
                GetComponent<AudioSource>().PlayOneShot(dieSound);
                break;
            case audioToPlay.start:
                GetComponent<AudioSource>().PlayOneShot(startSound);
                break;
            case audioToPlay.playerDie:
                GetComponent<AudioSource>().PlayOneShot(playerDieSound);
                break;
        }
      
    }

    public void PlayBackground()
    {
        GetComponent<AudioSource>().Play();
    }

    public void StopBackground()
    {
        GetComponent<AudioSource>().Stop();
    }
}
