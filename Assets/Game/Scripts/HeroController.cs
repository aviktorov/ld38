using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour {
	
	public float movementSpeed = 10.0f;
	public float jumpSpeed = 1.0f;
	public Transform targetCamera = null;
	
	private Rigidbody cachedBody;
	private bool biting;
	private FixedJoint cachedJoint;
	
	private void Awake() {
		cachedBody = GetComponent<Rigidbody>();
		biting = false;
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
		
		biting = Input.GetButton("Bite");
		if(cachedJoint && !biting) {
			GameObject.Destroy(cachedJoint);
			cachedJoint = null;
		}
		
		cachedBody.velocity = movement;
		if(movement.WithY(0.0f).sqrMagnitude > Mathf.Epsilon) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-movement), 5.0f * Time.deltaTime);
		}
	}
	
	private void OnTriggerEnter(Collider collider) {
		if(cachedJoint || !biting) return;
		
		if(collider == null) return;
		if(collider.GetComponent<Rigidbody>() == null) return;
		
		cachedJoint = gameObject.AddComponent<FixedJoint>();
		cachedJoint.connectedBody = collider.GetComponent<Rigidbody>();
	}
}
