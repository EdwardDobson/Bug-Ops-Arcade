using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider MusicSlider;
    public Slider EffectsSlider;
    // Start is called before the first frame update
    void Start()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        EffectsSlider.value = PlayerPrefs.GetFloat("EffectsVolume");
        audioMixer.SetFloat("Music",PlayerPrefs.GetFloat("MusicVolume"));
        audioMixer.SetFloat("Effects", PlayerPrefs.GetFloat("EffectsVolume"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMusic(float _volume)
    {
        audioMixer.SetFloat("Music", _volume);
        PlayerPrefs.SetFloat("MusicVolume", _volume);
    }
    public void SetEffects(float _volume)
    {
        audioMixer.SetFloat("Effects", _volume);
        PlayerPrefs.SetFloat("EffectsVolume", _volume);
    }
}
