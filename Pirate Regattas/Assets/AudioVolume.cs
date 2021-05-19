using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private Slider musicSlider;
    public AudioMixer audioMixer;


    void Start()
    {
        PlayerPrefs.SetFloat("music",-10f);
        musicSlider.value = PlayerPrefs.GetFloat("music");
        PlayerPrefs.Save();
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("music"));
    }

    public void SetMusicValue()
    {
        PlayerPrefs.SetFloat("music", musicSlider.value);
        PlayerPrefs.Save();
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("music"));
    }
}
