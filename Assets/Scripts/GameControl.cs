using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

    public float spacing;
    public bool inTransition;
    public Transitioner transitionScreen;

    public static GameControl instance;
    public Transform mainCharacter;
    public float mainCharMovePace;

    void Awake () {
        instance = this;
    }

    void Start () {
        spacing = (Camera.main.orthographicSize * 2) * Camera.main.aspect;
        mainCharacter = GameObject.FindGameObjectWithTag ("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (!inTransition) {
            if (Input.GetKeyDown (KeyCode.LeftArrow)) {
                transitionScreen.InitTransition (new Vector3 (-spacing, 0, 0));
                inTransition = true;
            } else if (Input.GetKeyDown (KeyCode.RightArrow)) {
                transitionScreen.InitTransition (new Vector3 (spacing, 0, 0));
                inTransition = true;
            }
        }
    }

    public void EndTransition () {
        inTransition = false;
    }
}
