using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DogController))]
public class HeroController : MonoBehaviour {
	
	public Transform targetCamera = null;
	
	private DogController controller;
	
	private void Awake() {
		controller = GetComponent<DogController>();
	}
	
	private void Update() {
		if(!controller) return;
		
		Vector3 cameraRight = targetCamera.right.WithY(0.0f).normalized;
		Vector3 cameraForward = targetCamera.forward.WithY(0.0f).normalized;
		
		controller.movement = cameraRight * Input.GetAxis("Horizontal") + cameraForward * Input.GetAxis("Vertical");
		controller.jumping = Input.GetButtonDown("Jump");
		controller.biting = Input.GetButton("Bite");
	}
}
