using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour {
	
	public Transform target = null;
	public Transform pivot = null;
	public float angle = 30.0f;
	public float damping = 5.0f;
	
	private void Update() {
		Quaternion targetRotation = pivot.rotation;
		
		if(target) {
			Vector3 direction = (target.position - transform.position).normalized;
			if(Vector3.Dot(pivot.forward, direction) > Mathf.Cos(angle * Mathf.Deg2Rad) && direction.sqrMagnitude > Mathf.Epsilon) {
				targetRotation = Quaternion.LookRotation(direction);
			}
		}
		
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, damping * Time.deltaTime);
	}
}
