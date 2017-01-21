using UnityEngine;
using System.Collections;

public class ClickableEntity : MonoBehaviour {

    protected Transform walkNode;
    public string hoverText;

	// Use this for initialization
	void Start () {
        walkNode = transform.GetChild (0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter () {
        if (!GameControl.instance.inTransition) {
            Debug.Log ("You Just put your mouse over a Clickable, setting Hover text on BottomBox");
            GameControl.instance.bottomText.SetText (hoverText);
        }
    }

    void OnMouseExit () {
        if (!GameControl.instance.inTransition) {
            GameControl.instance.bottomText.SetText ("");
        }
    }

    void OnMouseDown () {
        if (!GameControl.instance.inTransition) {
			Debug.Log("Chau");
            object nodeReference = walkNode;
            Debug.Log ("Will move " + GameControl.instance.mainCharacter.name + " towards " + walkNode.position);
            BehaviourHundlor.instance.AddToMono ("WalkToObjectNode", ref nodeReference);
        }
    }
}
