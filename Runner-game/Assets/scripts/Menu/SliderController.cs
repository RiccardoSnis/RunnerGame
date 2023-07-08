using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;
    
    [SerializeField] public TMP_Text valueVolumeText;

    [SerializeField] private Slider effectsSlider = null;
    
    [SerializeField] public TMP_Text valueEffectsText;

    [SerializeField] public AudioSource menuSource = null;

    public GameObject volumes;

    public GameObject mainMenu;

    void Start() {
        LoadValues();
    }


    public void openOptions()
    {
        mainMenu.SetActive(false);
        volumes.SetActive(true);
    }
    
    /*public void OnSliderChangedVolume(float value)
    {
        valueVolumeText.text= value.ToString("0.0");
    }

    public void OnSliderChangedEffects(float value)
    {
        valueEffectsText.text= value.ToString("0.0");
    }*/

    public void SaveVolumeButton()
    {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        PlayerPrefs.Save();
        LoadVolumeValues();
        volumes.SetActive(false);
        mainMenu.SetActive(true);

    }

    void LoadVolumeValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        //runSource.volume = volumeValue;
        menuSource.volume = volumeValue;
    }
    public void SaveEffectsButton()
    {
        float effectValue = effectsSlider.value;
        PlayerPrefs.SetFloat("EffectValue", effectValue);
        PlayerPrefs.Save();
        LoadEffectsValues();
    }

    void LoadEffectsValues()
    {
        float effectValue = PlayerPrefs.GetFloat("EffectValue");
        effectsSlider.value = effectValue;
    }

    void LoadValues(){
        LoadVolumeValues();
        LoadEffectsValues();
    }

    public void SaveButton () {
        SaveEffectsButton();
        SaveVolumeButton();
    }
}
