using UnityEngine;
using System;
using System.Reflection;
using System.Collections;

public class BehaviourHundlor : MonoBehaviour {

    public delegate void MonoAction ();
    public MonoAction monoAction;
    static public BehaviourHundlor instance;

    void Awake () {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        if (monoAction != null) {
            monoAction ();
        }
    }

    public void AddToMono (string _methodName, ref object _runtimeObject) {
        Type actionsType = typeof (BehaviourHundlor);
        MethodInfo actionFound = actionsType.GetMethod (_methodName);
        if (actionFound != null && monoAction == null) {
            monoAction = (MonoAction) Delegate.CreateDelegate (typeof (MonoAction), _runtimeObject, actionFound);
        }
    }

    public void WalkToObjectNode (object _objReference) {
        Debug.Log ("HERE");
        GameControl.instance.mainCharacter.position = Vector3.MoveTowards (GameControl.instance.mainCharacter.position, (_objReference as Transform).position,
            GameControl.instance.mainCharMovePace);
        if (GameControl.instance.mainCharacter.position == (_objReference as Transform).position) {
            Debug.Log ("I made it!");
            monoAction = null;
        }
    }
}
