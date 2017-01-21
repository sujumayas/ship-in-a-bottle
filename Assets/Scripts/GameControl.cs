using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

    public float spacing;
    public bool inTransition;
    public Transitioner transitionScreen;

    public static GameControl instance;
    public Transform mainCharacter;
    public float mainCharMovePace;

    public TextHandler bottomText;

    void Awake () {
        instance = this;
    }

    void Start () {
        spacing = (Camera.main.orthographicSize * 2) * Camera.main.aspect;
        mainCharacter = GameObject.FindGameObjectWithTag ("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void EndTransition () {
        inTransition = false;
    }
}
