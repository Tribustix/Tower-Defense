using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopRate : MonoBehaviour {

	public Vector3 roation;

	void Start () {
		
	}
	
	void Update () {
		transform.Rotate(roation * Time.deltaTime);
	}
}
