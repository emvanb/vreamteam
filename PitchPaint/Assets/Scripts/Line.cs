using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Line :MonoBehaviour{

	// line class Empty array.
	// a function to add a single point.
	List<LinePoint> myLine =  new List<LinePoint>();

	public void AddPoint(LinePoint PointToAdd){ // Add a point to the line
		Debug.Log(PointToAdd.creationTime);
		myLine.Add (PointToAdd);

	}

	public void Remove(){ // removes last indexed point
		if (myLine.Count > 0) {
			myLine.RemoveAt (myLine.Count - 1);
		}
	}

	public float startTime { // start time for when line is begins playiugn,
		get;
		set;
	}

	public float endTime {
		get;
		set;
	}

	public float timeToRepeat {// float for time to push to repeat
		get;
		set;
	}

	public float lineVolume { // "General lineVolume of line"
		get;
		set;
	}

	public float currentTime = 0; // current time for looping through thte update function
	public int currentPoint =0;  //current point indexhe current point 

	public LinePoint OriginPoint; // OriginPoint aka first index of line. 
	public bool LineDrawn= false;


	void Update(){
		if (endTime < startTime + 2) {
			endTime = startTime + 2;
		}
		if ((myLine.Count > 0)&& LineDrawn==true) {// Check if line has any points first. 
			if (currentPoint < myLine.Count) {
				if (currentTime > myLine [currentPoint].creationTime && myLine [currentPoint].sample != null) {
					//"Play Point" 
					// e.g myLine.[currentPoint].gameObject.GetComponent<AudioSource>().play....
					myLine [currentPoint].sample.Stop ();
					myLine [currentPoint].sample.Play ();
					currentPoint += 1;
				} 
			}else if (currentTime > endTime - startTime) {
				
					currentPoint = 0; // reset current point to 0
					currentTime = 0;
			}
            currentTime += Time.deltaTime;

        }
	}
}
