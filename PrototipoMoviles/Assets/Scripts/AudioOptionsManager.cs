using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioOptionsManager : MonoBehaviour
{
    //Establezco variables de volumen, sliders y un mezclador para poder manipular el nivel de los volumenes
    public AudioMixer mezclador;

    public float VolumeSound;
    public float VolumeMusic;

    public Slider sliderSound;
    public Slider sliderMusic;

    private void Start()
    {
        //Combino las variables para que todo funcione correctamente
        if (PlayerPrefs.HasKey("VolumeSFX"))
        {
            VolumeSound = PlayerPrefs.GetFloat("VolumeSFX");
            sliderSound.value = VolumeSound;
            SetFXVolume(VolumeSound);
        }
        if (PlayerPrefs.HasKey("VolumeMusic"))
        {
            VolumeMusic = PlayerPrefs.GetFloat("VolumeMusic");
            sliderMusic.value = VolumeMusic;
            SetMusicVolume(VolumeMusic);
        }
    }
    public void SetFXVolume(float volumen)
    {
        mezclador.SetFloat("VolumeSFX", volumen);
        PlayerPrefs.SetFloat("VolumeSFX", volumen);
    }
    public void SetMusicVolume(float volumen)
    {
        mezclador.SetFloat("VolumeMusic", volumen);
        PlayerPrefs.SetFloat("VolumeMusic", volumen);
    }

}
