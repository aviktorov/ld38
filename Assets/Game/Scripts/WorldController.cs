using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum WorldState {
	Idle,
	Shrinking,
	End,
}

public class WorldController : MonoBehaviour {
	
	public float radius = 100;
	public float shrinkTime = 100;
	public UnityEvent onWorldEnd;
	
	private float currentTime;
	private float currentRadius;
	private WorldState currentState;
	
	public float GetCurrentRadius() {
		return currentRadius;
	}
	
	private void Start() {
		currentTime = shrinkTime;
		currentState = WorldState.Idle;
		currentRadius = radius;
	}
	
	private void Update () {
		switch(currentState) {
			case WorldState.Shrinking: {
				
				currentTime -= Time.deltaTime;
				currentRadius = radius * Mathf.Clamp01(currentTime / shrinkTime);
				
				if(currentTime < 0.0f) {
					currentTime = 0.0f;
					currentState = WorldState.End;
					onWorldEnd.Invoke();
				}
			} break;
			
			// TODO: other states
		}
		
		transform.localScale = Vector3.one * currentRadius * 2.0f;
	}
}
