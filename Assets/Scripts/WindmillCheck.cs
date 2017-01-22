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
        map.Add ("Molinos3", 3);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Sum (string windmillName) {

    }
}
