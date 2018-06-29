using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	private AudioSource soundEffectAudio;
	public static SoundManager Instance = null;

	public AudioClip fireTowerAttack;
	public AudioClip gameLose;
	public AudioClip gameWin;
	public AudioClip iceTowerAttack;
	public AudioClip stoneTowerAttack;

	void Start () {

		if(Instance == null){
			
			Instance = this;

		}else if(Instance == this){
			Destroy(gameObject);
		}

		AudioSource [] sources = GetComponents<AudioSource>();
		
		foreach(AudioSource source in sources){
			if(source.clip == null){
				soundEffectAudio = source;
			}
		}



	}
	
	
	void Update () {
		
	}

	public void PlayOneShot (AudioClip clip, float volume) {
		soundEffectAudio.PlayOneShot(clip, volume);
	}
	


}
