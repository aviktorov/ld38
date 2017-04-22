using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour {
	
	public GameObject world = null;
	public float movementSpeed = 10.0f;
	
	private WorldController controller;
	private Rigidbody cachedBody;
	
	private void Awake() {
		cachedBody = GetComponent<Rigidbody>();
	}
	
	private void Start() {
		controller = (world) ? world.GetComponent<WorldController>() : null;
	}
	
	private void Update() {
		if(!controller) return;
		
		// direction
		Vector3 directionVector = transform.position - world.transform.position;
		directionVector.z = 0.0f;
		
		float distance = directionVector.magnitude;
		directionVector.Normalize();
		
		// tangent
		Vector3 tangentVector = new Vector3(directionVector.y, -directionVector.x, 0.0f);
		
		// movement
		float movementImpulse = Input.GetAxis("Horizontal") * movementSpeed;
		cachedBody.velocity += tangentVector * movementImpulse * Time.deltaTime;
		
		Vector3 buoyancyForce = directionVector * (controller.GetCurrentRadius() - distance) * 10.0f;
		
		cachedBody.AddForce(buoyancyForce);
		
		transform.rotation = Quaternion.LookRotation(tangentVector, directionVector) * Quaternion.Euler(0,-90,0);
	}
}
