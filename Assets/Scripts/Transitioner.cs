using UnityEngine;
using System.Collections;

public class Transitioner : MonoBehaviour {

    public enum FadeState {
        Idle,
        Init,
        Set
    }

    public FadeState currentState;
    public float fadeSpeed;
    Vector3 currentMove;
    Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer> ();
        Color temp = rend.material.color;
        temp.a = 0;
        rend.material.color = temp;
        currentState = FadeState.Idle;
    }
	
	// Update is called once per frame
	void Update () {
	
        if (currentState != FadeState.Idle) {
            if (currentState == FadeState.Init) {
                if (rend.material.color.a < 1) {
                    Color temp = rend.material.color;
                    temp.a += fadeSpeed * Time.deltaTime;
                    rend.material.color = temp;
                } else {
                    Color temp = rend.material.color;
                    temp.a = 1;
                    rend.material.color = temp;
                    currentState = FadeState.Set;
                    Camera.main.transform.Translate (currentMove);
                    currentMove = Vector3.zero;
                }
            } else if (currentState == FadeState.Set) {
                if (rend.material.color.a > 0) {
                    Color temp = rend.material.color;
                    temp.a -= fadeSpeed * Time.deltaTime;
                    rend.material.color = temp;
                } else {
                    Color temp = rend.material.color;
                    temp.a = 0;
                    rend.material.color = temp;
                    currentState = FadeState.Idle;
                    GameControl.instance.EndTransition ();
                }
            }
        }
	}

    public void InitTransition (Vector3 moveDir) {
        currentState = FadeState.Init;
        currentMove = moveDir;
    }
}
