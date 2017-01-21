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

    public string outcome;
    public object parameter;

    public Puzzle (string _id, string _toActive = null) {
        tasks = new List<Task> ();
        id = _id;
        toActive = _toActive;
    }

    public void AddTask (Task task) {
        tasks.Add (task);
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

    public readonly List<Puzzle> puzzles = new List<Puzzle> ();
    public readonly List<Puzzle> activePuzzles = new List<Puzzle> ();
    public readonly List<Puzzle> completed = new List<Puzzle> ();
    // Use this for initialization
    void Start () {
        Puzzle puzzle;
        puzzle = new Puzzle ("PZ01", "PZ02");
        puzzle.AddTask (new Task ("TK01"));
        puzzles.Add (puzzle);
        //---------------------------------//
        puzzle = new Puzzle ("PZ01");
        puzzle.AddTask (new Task ("TK02"));
        puzzles.Add (puzzle);
        //---------------------------------//
        //---------------------------------//
        //---------------------------------//
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
            puzzles.Remove (puzzle);
        }
    }

    // Update is called once per frame
    void Update () {
	    
	}
}
