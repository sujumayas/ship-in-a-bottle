using UnityEngine;
using System.Collections;

public class TextHandler : MonoBehaviour {

	private bool timed = false;
	public int actualIndex;
	private string initialText; 

	void Start () {
	 	initialText = transform.GetComponent<TextMesh>().text;
		SetText(initialText, false);
	}

	public void SetText(string text, bool timed = false){
		if (!GameControl.instance.inTransition) { /* Es esto necesario? */
			Debug.Log(text);
			if(!timed){
				transform.GetComponent<TextMesh> ().text = text;	
			}else{
				//Esperar a que hagan click con el mouse?
				transform.GetComponent<TextMesh> ().text = text;
			}
		}
	} 
}
