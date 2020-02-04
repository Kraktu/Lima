using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeValue : MonoBehaviour
{
	AudioSource audioSrc;
	float musicVolume = 1;
    void Start()
    {
		audioSrc = GetComponent<AudioSource>();
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
