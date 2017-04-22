using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	
	public Transform target = null;
	
	private void LateUpdate() {
		transform.position = target.position.WithZ(-10.0f);
		transform.rotation = target.rotation;
	}
}
