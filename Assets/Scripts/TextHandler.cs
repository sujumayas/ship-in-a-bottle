using UnityEngine;
using System.Collections;

public class TextHandler : MonoBehaviour {

	private bool timed = false;

	private string initialText; 

	void Start () {
	 	initialText = transform.GetComponent<TextMesh>().text;
		SetText(initialText, false);
	}

	public void SetText(string text, bool timed = false){
		transform.GetComponent<TextMesh> ().text = text;

	} 
}
