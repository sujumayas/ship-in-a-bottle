using UnityEngine;
using System.Collections;

public class TextHandler : MonoBehaviour {

	private bool timed = false;

	private string initialText = "Initial Text";

	void Start () {
		SetText(initialText, false);
	}

	public void SetText(string text, bool timed = false){
		transform.GetComponent<TextMesh> ().text = text;

	} 
}
