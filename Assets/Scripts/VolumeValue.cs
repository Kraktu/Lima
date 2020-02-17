using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeValue : MonoBehaviour
{
	AudioSource audioSrc;
	public float musicVolume;
    public Slider slider;
    void Start()
    {
		audioSrc = GetComponent<AudioSource>();
        slider.value = musicVolume;
    }

    void Update()
    {
		audioSrc.volume = musicVolume;
    }

	public void SetVolume(float vol)
	{
		musicVolume = vol;
	}
}
