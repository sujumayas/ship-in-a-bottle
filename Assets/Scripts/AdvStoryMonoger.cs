using UnityEngine;
using System.Collections.Generic;

public class Task {
    public bool done;
    public string target;
    public bool hasOutcome;

    public string outcome;
    public object parameter;

    public Task (string _target, bool _hasOutcome = false) {
        target = _target;
        hasOutcome = _hasOutcome;
    }
}

public class Puzzle {
    public readonly string id;
    public List<Task> tasks;
    public string toActive;

    public List<MonoInstruction> outcomes;

    public Puzzle (string _id, string _toActive = null) {
        tasks = new List<Task> ();
        outcomes = new List<MonoInstruction> ();
        id = _id;
        toActive = _toActive;
    }

    public void AddTask (Task task) {
        tasks.Add (task);
    }

    public void SetOutcome (string _methodName, object _reference) {
        outcomes.Add (new MonoInstruction (_methodName, _reference));
    }

    public void Check (string _id) {
        foreach (Task task in tasks) {
            if (!task.done && (_id == task.target)) {
                task.done = true;
            }
        }
    }
}

public class AdvStoryMonoger : MonoBehaviour {

    public static AdvStoryMonoger instance;
    public readonly List<Puzzle> puzzles = new List<Puzzle> ();
    public readonly List<Puzzle> activePuzzles = new List<Puzzle> ();
    public readonly List<Puzzle> completed = new List<Puzzle> ();




    void Awake () {
        instance = this;
    }

    void Start () {
		

		Puzzle puzzle;
//		puzzle = new Puzzle("PZ00", "PZ01");

		//---------------------------------//
		//********* Fix The Tower *********//
		//---------------------------------//
        puzzle = new Puzzle ("PZ01", "PZ02");
        puzzle.AddTask (new Task ("TK01")); // Click on Broken Tower
        puzzle.SetOutcome ("PlayWorkAnimation", GameControl.instance.mainCharacter.GetComponent<Animator> ());
        puzzle.SetOutcome ("DisableObject", GameObject.Find ("BrokenAntena").transform.FindChild ("Sprite2").gameObject);
        puzzle.SetOutcome ("PlayOneShotAnimation", GameObject.Find ("BrokenAntena").transform.FindChild ("Sprite").GetComponent<Animator> ());
        puzzles.Add (puzzle);
        //---------------------------------//
		//***** Puzzles on The Tower ******//
		//---------------------------------//
        puzzle = new Puzzle ("PZ02", "PZ03");
        puzzle.AddTask (new Task ("TK02")); //Click on Main Tower
		puzzle.SetOutcome("SetNextTCCComsText", GameObject.Find("TCComs").gameObject); 
		puzzle.SetOutcome("SetNextTCCComsText", GameObject.Find("TCComs").gameObject);
		puzzle.SetOutcome("SetNextTCCComsText", GameObject.Find("TCComs").gameObject); 
		puzzles.Add (puzzle);
		//---------------------------------//
		//****** Puzzles on Molinos *******//
		//---------------------------------//
		puzzle = new Puzzle ("PZ03", "PZ04");
		puzzle.AddTask(new Task("TK03")); //Click on lvl 1 molino
		puzzle.SetOutcome("checkMolinoState", GameObject.Find("Molino1").gameObject);

		puzzle = new Puzzle("PZ04", "PZ05");
		puzzle.AddTask(new Task("TK04")); //Click on lvl 1 molino
		puzzle.SetOutcome("checkMolinoState", GameObject.Find("Molino2").gameObject);

		puzzle = new Puzzle("PZ05", "PZ06");
		puzzle.AddTask(new Task("TK05")); //Click on lvl 2 molino (Solo posible si T3 y T4 estan ON y ningun otro molino)
		puzzle.SetOutcome("checkMolinoState", GameObject.Find("Molino3").gameObject);

		puzzle = new Puzzle("PZ06", "PZ07");
		puzzle.AddTask(new Task("TK06")); //Click on lvl 3 molino (Solo posible si T5 y (T3 o T4) estan ON y ningun otro molino)
		puzzle.SetOutcome("checkMolinoState", GameObject.Find("Molino4").gameObject);

		puzzle = new Puzzle("PZ07", "PZ08");
		puzzle.AddTask(new Task("TK07")); //Click on lvl 5 molino (Solo posible si T6 y ((T3 y T4) o T05) estan ON y ningun otro molino). 
		puzzle.SetOutcome("checkMolinoState", GameObject.Find("Molino5").gameObject);

		//---------------------------------//
        //---------------------------------//
        activePuzzles.Add (Search ("PZ01"));
    }

    public void Check (string _id) {
        List<Puzzle> removals = new List<Puzzle> ();
        foreach (Puzzle puzzle in activePuzzles) {
            puzzle.Check (_id);
            int totalTasks = 0;
            foreach (Task task in puzzle.tasks) {
                if (!task.done) {
                    break;
                } else {
                    totalTasks++;
                }
            }
            if (totalTasks == puzzle.tasks.Count) {
                removals.Add (puzzle);
            }
        }
        foreach (Puzzle puzzle in removals) {
            completed.Add (puzzle);
            activePuzzles.Remove (puzzle);
            CallOnDone (puzzle);
        }
    }

    public Puzzle Search (string _id) {
        for (int i = 0; i < puzzles.Count; i++) {
            if (puzzles[i].id == _id) {
                return puzzles[i];
            }
        }
        Debug.LogWarning ("There's no puzzle with ID: " + _id);
        return null;
    }

    void CallOnDone (Puzzle puzzle) {
        if (!string.IsNullOrEmpty (puzzle.toActive)) {
            activePuzzles.Add (Search (puzzle.toActive));
        }
        foreach (MonoInstruction outcome in puzzle.outcomes) {
            BehaviourHundlor.instance.EnqueueAction (outcome);
        }
        Debug.Log ("Activated " + puzzle.toActive);
    }

    // Update is called once per frame
    void Update () {
	    
	}
}
