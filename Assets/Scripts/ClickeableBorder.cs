using UnityEngine;
using System.Collections;

public class ClickeableBorder : ClickableEntity {

	public float direction;

	// Update is called once per frame
	void Update () {
	
	}

	//
	void OnMouseDown () {
		if (!GameControl.instance.inTransition) {
			object nodeReference = walkNode;
			Debug.Log ("Will move " + GameControl.instance.mainCharacter.name + " towards " + walkNode.position);
			BehaviourHundlor.instance.AddToMono ("WalkToObjectNode", ref nodeReference);
			Debug.Log("Hola");
			GameControl.instance.transitionScreen.InitTransition (new Vector3 (GameControl.instance.spacing * direction, 0, 0));
			GameControl.instance.inTransition = true;
		}
	}
}
