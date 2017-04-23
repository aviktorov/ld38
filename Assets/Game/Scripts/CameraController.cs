using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	
	public float linearDamping = 5.0f;
	public float angularDamping = 5.0f;
	public float distance = 10.0f;
	public float heightOffset = 2.0f;
	
	public Transform target = null;
	public Transform center = null;
	
	private void LateUpdate() {
		if(!target) return;
		if(!center) return;
		
		Vector3 desiredDirection = (center.position - transform.position).normalized;
		if(desiredDirection.sqrMagnitude < Mathf.Epsilon) return;
		
		Vector3 desiredPosition = target.position - desiredDirection.WithY(0.0f).normalized * distance;
		desiredPosition.y += Mathf.Lerp(target.position.y + heightOffset, Mathf.Max(center.position.y, target.position.y + heightOffset), 0.5f);
		
		float linearDampingClamped = Mathf.Clamp01(linearDamping * Time.deltaTime);
		float angularDampingClamped = Mathf.Clamp01(angularDamping * Time.deltaTime);
		
		transform.position = Vector3.Lerp(transform.position, desiredPosition, linearDampingClamped);
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredDirection), angularDampingClamped);
	}
}
