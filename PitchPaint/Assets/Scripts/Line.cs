using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Line{

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



}
