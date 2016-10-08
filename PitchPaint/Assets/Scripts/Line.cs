using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Line :MonoBehaviour{

	// line class Empty array.
	// a function to add a single point.
	List<LinePoint> myLine =  new List<LinePoint>();

	public void AddPoint(LinePoint PointToAdd){ // Add a point to the line
		myLine.Add (PointToAdd);

	}

	public void Remove(){ // removes last indexed point
		if (myLine.Count > 0) {
			myLine.RemoveAt (myLine.Count - 1);
		}
	}

	public float StartTime { // start time for when line is begins playiugn,
		get;
		set;
	}

	public float EndTime {
		get;
		set;
	}

	public float TimeToRepeat {// float for time to push to repeat
		get;
		set;
	}

	public float Volume { // "General volume of line"
		get;
		set;
	}



	public float CurrentTime = 0; // current time for looping through thte update function
	public LinePoint CurrentPoint;  //the current point 

	public LinePoint OriginPoint; // OriginPoint aka first index of line. 

	// Update iterates through each point in the list based on its creation time, and will "play the point" 
	void Update(){ 
		Debug.Assert (myLine.Count > 0);// Line should not be created without at least 1 point. 
		CurrentTime+=1;
		foreach (LinePoint testPoint in myLine){ // if currentime matches a points creation time, add code to eventually play the point
			if (testPoint.CreationTime == CurrentTime) {
				// "Play Point"
			}
		}
		if (CurrentTime > (TimeToRepeat + myLine [myLine.Count - 1].CreationTime)) { // if current time is over 
			CurrentTime = 0;
		}

	}

}
