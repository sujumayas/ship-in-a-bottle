using UnityEngine;
using System.Collections;

public class MolinoClickeable : ClickableEntity {

	// Use this for initialization
	protected Transform walkNode;
	public bool itsSpining;
	public int numValue; 

	// Use this for initialization
	void Start () {
		walkNode = transform.GetChild (0);
		itsSpining = false;
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown () {
		if (!GameControl.instance.inTransition) {
			object nodeReference = walkNode;
			Debug.Log ("Will move " + GameControl.instance.mainCharacter.name + " towards " + walkNode.position);
			BehaviourHundlor.instance.AddToMono ("WalkToObjectNode", ref nodeReference);
		}
	}
}
