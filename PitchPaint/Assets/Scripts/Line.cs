using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Line :MonoBehaviour{

	// line class Empty array.
	// a function to add a single point.
	List<LinePoint> myLine =  new List<LinePoint>();

    List<int> soundIndexes = new List<int>();
    //Every time we add a sound, add it to soundIndexArray
    //This way we know what's going on in terms of each sound point
    //Then, every time that we update, check if we're at 1) next sound and 2) next tube play, using lastSound.startTime + n  * tubeTime
    //I think each point is going to store a DeltaT

	public void AddPoint(LinePoint PointToAdd, bool isSound){ // Add a point to the line

        if (isSound)
        {

            //Get # of points added since last sound (INCLUDE last sound point!)
            int counter = myLine.Count - soundIndexes[soundIndexes.Count - 1];
            for (int i = soundIndexes[soundIndexes.Count - 1]; i < myLine.Count; i++)
            {
                myLine[i].deltaT = (PointToAdd.creationTime - myLine[soundIndexes[soundIndexes.Count - 1]].creationTime) / counter;
            }
            soundIndexes.Add(myLine.Count);
        }
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

    private int soundIndex = 0;

	void Update(){
		if (endTime < startTime + 4) {
			endTime = startTime + 4;
		}
		if ((myLine.Count > 0)&& LineDrawn==true) {// Check if line has any points first. 

			if (currentTime > myLine [soundIndexes[soundIndex]].creationTime  && myLine[soundIndexes[soundIndex]].sample!=null) {
                //"Play Point" 
                // e.g myLine.[currentPoint].gameObject.GetComponent<AudioSource>().play....
                myLine[soundIndexes[soundIndex]].sample.Stop();
                myLine[soundIndexes[soundIndex]].sample.Play();
				if (soundIndexes[soundIndex] + 1 > myLine.Count - 1) { // check if we have reached last index of line
					currentTime = 0;
                    soundIndex = 0;
				} else {
                    soundIndex += 1;
                }
                currentPoint = 0;
            }
            else
            {
                if(currentTime > myLine[soundIndexes[soundIndex-1]].creationTime + currentPoint* myLine[soundIndexes[soundIndex - 1]].deltaT)
                {
                    currentPoint += 1;
                    myLine[soundIndexes[soundIndex - 1] + currentPoint].animating = true;
                }
            }
            currentTime += Time.deltaTime;

        }
	}
}
