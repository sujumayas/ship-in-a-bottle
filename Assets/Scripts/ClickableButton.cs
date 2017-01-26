using UnityEngine;
using System.Collections;

public class ClickableButton : ClickableEntity {

    void Start () {

    }

    void OnMouseDown () {
        if (!GameControl.instance.inTransition && BehaviourHundlor.instance.monoAction == null) {
            BehaviourHundlor.instance.lastReference = transform;
            AdvStoryMonoger.instance.Check (currentID);
        }
    }
}
