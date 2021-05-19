using System.Collections;
using System.Collections.Generic;
using GameScripts;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LostScriptAudio : MonoBehaviour
{
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
        GameController.Instance.AddOnStateChanged(PlayMusic);
    }

    void PlayMusic()
    {
        if (GameController.Instance.currentGameState == GameState.OpponentWin)
        {
            source.Play();
        }
    }

    
    
}
