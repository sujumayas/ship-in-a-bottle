using UnityEngine;
using System.Collections.Generic;

public class WindmillCheck : MonoBehaviour {

    public int targetSum;
    public int currentSum;
    public string taskID;

    Dictionary<string, int> map = new Dictionary<string, int> ();

	// Use this for initialization
	void Start () {

        map.Add ("Molinos1", 2);
        map.Add ("Molinos2", 1);
        map.Add ("Molinos3", 1);
        map.Add ("Molinos4", 5);
        map.Add ("Molinos5", 3);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Sum (string windmillName) {
        if (!GameObject.Find (windmillName).GetComponent<MolinoClickeable> ().isTurnedOn) {
            if (currentSum == 0) {
                if (map[windmillName] == 1) {
                    BehaviourHundlor.SetLoopAnimation (GameObject.Find (windmillName).transform.FindChild ("Sprite").GetComponent<Animator> ());
                    currentSum += map[windmillName];

                    GameObject.Find (windmillName).GetComponent<MolinoClickeable> ().isTurnedOn =
                        !GameObject.Find (windmillName).GetComponent<MolinoClickeable> ().isTurnedOn;
                }
            } else {
                if (map[windmillName] == currentSum) {
                    BehaviourHundlor.SetLoopAnimation (GameObject.Find (windmillName).transform.FindChild ("Sprite").GetComponent<Animator> ());
                    currentSum += map[windmillName];

                    GameObject.Find (windmillName).GetComponent<MolinoClickeable> ().isTurnedOn =
                        !GameObject.Find (windmillName).GetComponent<MolinoClickeable> ().isTurnedOn;
                }
            }
        } else {
            GameObject.Find (windmillName).transform.FindChild ("Sprite").GetComponent<Animator> ().runtimeAnimatorController = null;
            GameObject.Find (windmillName).transform.FindChild ("Sprite").GetComponent<SpriteRenderer> ().sprite =
                GameObject.Find (windmillName).GetComponent<MolinoClickeable> ().defaultSprite;
            currentSum -= map[windmillName];
            GameObject.Find (windmillName).GetComponent<MolinoClickeable> ().isTurnedOn =
                !GameObject.Find (windmillName).GetComponent<MolinoClickeable> ().isTurnedOn;
        }
        GameControl.instance.purifier.SetText (currentSum + "/" + targetSum);
        if (currentSum != targetSum) {
            BehaviourHundlor.ResetTask (taskID);
        }
    }
}
