using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

	public float secondsBeforeDestruction = 3f;

	void Start() {

		Destroy(gameObject, secondsBeforeDestruction);

	}
}
