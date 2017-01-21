using UnityEngine;
using System.Collections;

public class ClickableEntity : MonoBehaviour {

    Transform walkNode;

	// Use this for initialization
	void Start () {
        walkNode = transform.GetChild (0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter () {
        Debug.Log ("You Just put your mouse over a Clickable");
    }

    void OnMouseDown () {
        Debug.Log ("Will move " + GameControl.instance.mainCharacter.name + " towards " + walkNode.position);
    }
}
