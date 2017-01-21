using UnityEngine;
using System.Collections;

public class ClickeableBorder : ClickableEntity {

	public float direction;
    public Transform teleportTarget;

	// Update is called once per frame
	void Update () {
	
	}

	//
	void OnMouseDown () {
		if (!GameControl.instance.inTransition && BehaviourHundlor.instance.monoAction == null) {
			object nodeReference = walkNode;
			Debug.Log ("Will move " + GameControl.instance.mainCharacter.name + " towards " + walkNode.position);
			BehaviourHundlor.instance.AddToMono ("WalkToObjectNode", ref nodeReference);
			GameControl.instance.transitionScreen.InitTransition (new Vector3 (25 * direction, 0, 0), teleportTarget.position);
			GameControl.instance.inTransition = true;
		}
	}
}
