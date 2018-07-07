using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicSettings : MonoBehaviour {

	public Dropdown dropdownGraphics;
	public Dropdown dropdownResolutions;
	public GameObject check;

	private int qualityIndex;
	private int currentResolutionIndex;
	
	private Resolution [] resolutions;

	void Awake () {

		qualityIndex = QualitySettings.GetQualityLevel();
		dropdownGraphics.value = qualityIndex;

		if(Screen.fullScreen == true){
			check.SetActive(true);
		}else{
			check.SetActive(false);
		}

	}

	void Start () {

		resolutions = Screen.resolutions;
		dropdownResolutions.ClearOptions();

		List<string> options = new List<string>();

		for(int i = 0; i < resolutions.Length; i++){
			string option =  resolutions[i].width + " x " + resolutions[i].height;
			options.Add(option);

		}
		
		dropdownResolutions.AddOptions(options);
		dropdownResolutions.value = currentResolutionIndex;
		dropdownResolutions.RefreshShownValue();

		
	
	}
	
	public void SetQuality (int qualityIndex) {

		QualitySettings.SetQualityLevel(qualityIndex);

	}

	public void SetRResolution (int resolutionIndex){
		Resolution resolution = resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}

	public void SetFullscreen  (bool isFullscreen){
		Screen.fullScreen = isFullscreen;
	}
}
