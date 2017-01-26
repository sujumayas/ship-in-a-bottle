using UnityEngine;
using System.Collections.Generic;

public class SliderValueHolder : MonoBehaviour {

    public List<float> sliderValues;

	// Use this for initialization
	void Start () {
	
	}

    public void SetSliderHeight (float targetValue) {
        Vector3 temp = transform.localScale;
        temp.y = targetValue;
        transform.localScale = temp;
    }
}
