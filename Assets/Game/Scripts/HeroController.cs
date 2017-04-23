using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour {
	
	public float movementSpeed = 10.0f;
	public float jumpSpeed = 1.0f;
	public Transform targetCamera = null;
	
	private Rigidbody cachedBody;
	
	private void Awake() {
		cachedBody = GetComponent<Rigidbody>();
	}
	
	private void Update() {
		if(!cachedBody) return;
		if(!targetCamera) return;
		
		Vector3 cameraRight = targetCamera.right.WithY(0.0f).normalized;
		Vector3 cameraForward = targetCamera.forward.WithY(0.0f).normalized;
		
		// movement
		Vector3 movement = cameraRight * Input.GetAxis("Horizontal") + cameraForward * Input.GetAxis("Vertical");
		if(movement.sqrMagnitude > 1.0f) movement.Normalize();
		movement *= movementSpeed;
		
		movement.y = cachedBody.velocity.y;
		if(Input.GetButtonDown("Jump")) {
			movement.y = jumpSpeed;
		}
		
		cachedBody.velocity = movement;
	}
}
