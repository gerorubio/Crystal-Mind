using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour {
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectsSlider;

    private void Start() {
        if (PlayerPrefs.HasKey("masterVolume") || PlayerPrefs.HasKey("musicVolume") || PlayerPrefs.HasKey("effectsVolume")) {
            LoadVolume();
        } else {
            SetMasterVolume();
            SetMusicVolume();
            SetEffectsVolume();
        }
    }

    public void SetMasterVolume() {
        float volume = masterSlider.value;
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("masterVolume", volume);
    }
    public void SetMusicVolume() {
        float volume = musicSlider.value;
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void SetEffectsVolume() {
        float volume = effectsSlider.value;
        audioMixer.SetFloat("effectsVolume", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("effectsVolume", volume);
    }

    private void LoadVolume() {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        effectsSlider.value = PlayerPrefs.GetFloat("effectsVolume");

        SetMasterVolume();
        SetMusicVolume();
        SetEffectsVolume();
    }
}