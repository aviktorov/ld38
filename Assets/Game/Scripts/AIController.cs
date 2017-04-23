using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DogController))]
public class AIController : MonoBehaviour {
	
	public Transform targetMeat = null;
	public Transform targetHouse = null;
	public float jumpTimeMin = 2.0f;
	public float jumpTimeMax = 10.0f;
	public float approachDistance = 2.0f;
	
	private DogController controller;
	private float currentTime;
	
	private void Awake() {
		controller = GetComponent<DogController>();
		currentTime = Random.Range(jumpTimeMin, jumpTimeMax);
	}
	
	private void Update() {
		if(!controller) return;
		if(!targetMeat) return;
		if(!targetHouse) return;
		
		Transform target = (controller.meat) ? targetHouse : targetMeat;
		Vector3 movement = (target.position - transform.position).WithY(0.0f).normalized;
		if(movement.sqrMagnitude < approachDistance * approachDistance) movement = Vector3.zero;
		
		currentTime -= Time.deltaTime;
		
		controller.movement = movement;
		controller.jumping = currentTime < 0.0f;
		
		if(currentTime < 0.0f) {
			currentTime = Random.Range(jumpTimeMin, jumpTimeMax);
			controller.biting = !controller.biting;
		}
		
	}
}
