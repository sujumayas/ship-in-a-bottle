using UnityEngine;
using System.Collections;

public class ClickableZoom : ClickableEntity {

    public float direction;
    public Transform zoomObject;

    void OnMouseDown () {
        if (!GameControl.instance.inTransition && BehaviourHundlor.instance.monoAction == null) {
            zoomObject.Translate (0, direction * 25, 0);
        }
    }
}
