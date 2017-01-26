using UnityEngine;
using System.Collections.Generic;

public class ButtonCheck : MonoBehaviour {

    public string taskID;
    public int maxIndex;
    public int[] defaultComb;
    public int[] targetComb;

    Dictionary<string, int> map = new Dictionary<string, int> ();

    // Use this for initialization
    void Start () {

        map.Add ("Boton1", defaultComb[0]);
        map.Add ("Boton2", defaultComb[1]);
        map.Add ("Boton3", defaultComb[2]);
        map.Add ("Boton4", defaultComb[3]);

        foreach (string buttonName in map.Keys) {
            MoveSlider (buttonName);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Test (string buttonName) {
        if (map[buttonName] < maxIndex) {
            map[buttonName]++;
        } else {
            map[buttonName] = 0;
        }

        MoveSlider (buttonName);

        int correctValues = 0;
        for (int i = 0; i < targetComb.Length; i++) {
            if (map["Boton" + (i + 1)] == targetComb[i]) {
                correctValues++;
            }
        }

        Debug.Log ("Clicked on " + buttonName + " and its current value is: " + map[buttonName]);
        if (correctValues != targetComb.Length) {
            BehaviourHundlor.ResetTask (taskID);
        } else {
            BehaviourHundlor.instance.EnqueueAction (new MonoInstruction ("PlayOneShotAnimation", GameObject.Find ("Borbicoido").transform.FindChild ("Sprite").GetComponent<Animator> ()));
            BehaviourHundlor.instance.EnqueueAction (new MonoInstruction ("TranslateObjectUp", transform.parent));
            BehaviourHundlor.instance.EnqueueAction (new MonoInstruction ("TranslateObjectUp", GameObject.Find ("InsideOut").transform));
        }
    }

    void MoveSlider (string _buttonName) {
        SliderValueHolder currentSlider = transform.FindChild (_buttonName).FindChild ("Slider").GetComponent<SliderValueHolder> ();
        currentSlider.SetSliderHeight (currentSlider.sliderValues[map[_buttonName]]);
    }
}
