using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogHouseController : MonoBehaviour {
	
	public string message = "";
	
	// Update is called once per frame
	private void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.tag != "Meat") return;
		
		UI.instance.endGame = true;
		UI.instance.message = message;
	}
}
