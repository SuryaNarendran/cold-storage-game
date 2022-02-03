using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] string[] audioNames;

    bool busy = false;

    public void Play(int index)
    {
        Audiomanager.Play(audioNames[index]); 
    }

    public void Play()
    {
        Audiomanager.Play(audioNames[0]);
    }

    public void PlayWithInterval(float interval)
    {
        if (busy) return;
        Play();
        busy = true;
        Timer.Register(interval, () => busy = false);
    }

    public void PlayWithIntervalRandom(float interval)
    {
        if (busy) return;
        int index = Random.Range(0, audioNames.Length);
        Play(index);
        busy = true;
        Timer.Register(interval, () => busy = false);
    }
}
