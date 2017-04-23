using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoSingleton<UI> {
	
	[HideInInspector]
	public string message = "";
	
	[HideInInspector]
	public bool endGame = false;
	
	private void OnGUI() {
		if(!endGame) return;
		
		GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200));
		GUILayout.BeginVertical("box");
		GUILayout.Label(message);
		if(GUILayout.Button("Restart")) {
			SceneManager.LoadScene("Gameplay");
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}
}