using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour {
	
	[HideInInspector]
	public bool biting = false;
	
	[HideInInspector]
	public bool jumping = false;
	
	[HideInInspector]
	public Vector3 movement = Vector3.zero;
	
	public float damping = 2.0f;
	public float movementSpeed = 5.0f;
	public float jumpSpeed = 10.0f;
	
	private Rigidbody cachedBody;
	private FixedJoint cachedJoint;
	private bool ground;
	
	public bool meat {
		get { return cachedJoint != null; }
	}
	
	private void Awake() {
		cachedBody = GetComponent<Rigidbody>();
		biting = false;
		ground = true;
	}
	
	private void Update() {
		if(!cachedBody) return;
		if(UI.instance.endGame) return;
		
		if(movement.sqrMagnitude > 1.0f) movement.Normalize();
		movement *= movementSpeed;
		
		movement.y = cachedBody.velocity.y;
		if(jumping && ground) {
			ground = false;
			movement.y = jumpSpeed;
		}
		
		if(cachedJoint && !biting) {
			GameObject.Destroy(cachedJoint);
			cachedJoint = null;
		}
		
		cachedBody.velocity = movement;
		Quaternion targetRotation = Quaternion.LookRotation(transform.forward.WithY(0.0f).normalized);
		
		if(movement.WithY(0.0f).sqrMagnitude > Mathf.Epsilon) {
			targetRotation = Quaternion.LookRotation(-movement.normalized);
		}
		
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, damping * Time.deltaTime);
	}
	
	private void OnCollisionEnter(Collision collision) {
		if(cachedJoint && collision.gameObject.tag == "Dog") {
			GameObject.Destroy(cachedJoint);
			cachedJoint = null;
			biting = false;
		}
		
		if(ground) return;
		
		for(int i = 0; i < collision.contacts.Length; i++) {
			ContactPoint contact = collision.contacts[i];
			if(contact.normal.y > 0.9f) {
				ground = true;
				return;
			}
		}
	}
	
	private void OnTriggerEnter(Collider collider) {
		if(cachedJoint || !biting) return;
		
		if(collider == null) return;
		if(collider.gameObject.tag != "Meat") return;
		if(collider.GetComponent<Rigidbody>() == null) return;
		
		cachedJoint = gameObject.AddComponent<FixedJoint>();
		cachedJoint.connectedBody = collider.GetComponent<Rigidbody>();
	}
}
