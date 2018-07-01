using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicSettings : MonoBehaviour {

	public AudioMixer audioMixer;
	public GameObject buttonOnSFX;
	public GameObject buttonOnMusic;
	public GameObject buttonOffSFX;
	public GameObject buttonOffMusic;

	private float volumeMusic;
	private float volumeSFX;
	
	

	void Awake() {
		
		audioMixer.GetFloat("VolumeSFX", out volumeSFX);
		Debug.Log(volumeSFX);
		audioMixer.GetFloat("VolumeMusic", out volumeMusic);
		Debug.Log(volumeMusic);

		if(volumeSFX == -80){
			buttonOnSFX.SetActive(false);
			buttonOffSFX.SetActive(true);
		}

		if(volumeMusic == -80){
			buttonOnMusic.SetActive(false);
			buttonOffMusic.SetActive(true);
		}


	}

	public void SetVolumeMaster (float volume) {

		audioMixer.SetFloat("VolumeMaster", volume);
		

		if(volume == -50f ){
			audioMixer.SetFloat("VolumeMaster", -80f);
		}

	}

	public void ActivateVolume (string parameter){	


		if(parameter == buttonOnSFX.name || parameter == buttonOffSFX.name){
			
			if(volumeSFX == 0f){
				audioMixer.SetFloat("VolumeSFX", -80f);
				audioMixer.GetFloat("VolumeSFX", out volumeSFX);
			}else{
				audioMixer.SetFloat("VolumeSFX", 0f);
				audioMixer.GetFloat("VolumeSFX", out volumeSFX);
			}
		
		}else{
			
			if(volumeMusic == 0f){

				audioMixer.SetFloat("VolumeMusic", -80f);
				audioMixer.GetFloat("VolumeMusic", out volumeMusic);

			}else{

				audioMixer.SetFloat("VolumeMusic", 0f);
				audioMixer.GetFloat("VolumeMusic", out volumeMusic);

			}

		}

		
	}

}
