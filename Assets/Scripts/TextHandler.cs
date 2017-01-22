using UnityEngine;
using System.Collections;

public class TextHandler : MonoBehaviour {
    
	public int actualIndex;
	private string initialText;

	void Start () {
	 	initialText = transform.GetComponent<TextMesh>().text;
		SetText(initialText, false);
	}

	public void SetText(string text, bool _timed = false){
		if (!GameControl.instance.inTransition) { 
			Debug.Log(text);
            transform.GetComponent<TextMesh> ().text = text;
            if (_timed) {

            }
		}
	} 
}
