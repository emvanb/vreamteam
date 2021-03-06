﻿using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MouseBrush : MonoBehaviour
{
    //public GameObject main;
    //private GameLoop liveGameLoop;
    float timeBetweenPoints = 1;
    float currentTime = 0;
    Vector3 lastPos = Vector3.zero;
    public GameObject PointPrefab;
    public GameObject EmptyPointPrefab;
    //public Line DrawingLine;
    public Vector3 LastMousePos;
    public Vector3 CurrentMousePos;
    public AudioClip TestClip;
    public GameObject DrawingLineParentPrefab;
    public GameObject CurrentDrawingLineParent;
    public float TimeOfLastPointSpawn;
    public AudioMixerGroup[] AudioMixerGroupArray;
    public int HeightofSpawnedY;

    private Vector3 lastPoint;
    private GameObject currentCylinder;
    // Use this for initialization
    void Start()
    {
        TimeOfLastPointSpawn = 0;
        //liveGameLoop = main.GetComponent<GameLoop>();
        //TestClip = liveGameLoop.currentSample;
    }

    // Update is called once per frame
    //	void FixedUpdate () {
    //		if (Input.GetMouseButtonDown (0)) {// first frame mouse is pressed, create a new line.
    //			CurrentDrawingLineParent = (GameObject) Instantiate (DrawingLineParentPrefab);
    //			CurrentDrawingLineParent.transform.position = Input.mousePosition;
    //		}
    //		else if (Input.GetMouseButton (0)==true) { // while the mouse is pressed. 
    //			{
    //				Debug.Log (Time.time - TimeOfLastPointSpawn);
    //
    //				if (true) {
    //					UpdateDraw (Input.mousePosition, CurrentDrawingLineParent);
    //					//TimeOfLastPointSpawn = Time.time;
    //				}
    //			}
    //		}
    //		else if (Input.GetMouseButtonUp (0)) {
    //			CurrentDrawingLineParent.GetComponent<Line>().LineDrawn = true;
    //		}
    //			
    //	}
    public void StartDraw()
    {
        //TestClip = liveGameLoop.currentSample;

        CurrentDrawingLineParent = (GameObject)Instantiate(DrawingLineParentPrefab);
        CurrentDrawingLineParent.transform.position = Input.mousePosition;
        CurrentDrawingLineParent.GetComponent<Line>().startTime = Time.time;
        lastPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5.0f);
        lastPoint = Camera.main.ScreenToWorldPoint(lastPoint);
        currentCylinder = (GameObject)Instantiate(EmptyPointPrefab);
        MoveCurrentCylinder(lastPoint, lastPoint + Vector3.one * .05f);
    }

    void MoveCurrentCylinder(Vector3 lastPos, Vector3 currentPos)
    {
        currentCylinder.transform.position = (currentPos + lastPos) / 2.0f;
        currentCylinder.transform.LookAt(currentPos);
        currentCylinder.transform.localScale = new Vector3(.05f, .05f, (currentPos - lastPos).magnitude * .8f);
    }
    public void UpdateDraw(Vector3 handPos, GameObject currentLine)
    {
        currentTime += Time.deltaTime;

        MoveCurrentCylinder(lastPoint, handPos);
        if (currentTime > timeBetweenPoints)
        {
            GameObject CurrentPointPrefab = (GameObject)Instantiate(PointPrefab, currentLine.transform);
            LinePoint pt = CurrentPointPrefab.GetComponent<LinePoint>();
            CurrentPointPrefab.transform.position = (handPos + lastPoint) / 2.0f;
            CurrentPointPrefab.transform.LookAt(handPos);
            CurrentPointPrefab.transform.localScale = new Vector3(.05f, .05f, Mathf.Max((handPos - lastPoint).magnitude * .6f,.005f)) ;
            //LinePoint pt = new LinePoint ();
            pt.creationTime = Time.time - currentLine.GetComponent<Line>().startTime;
            pt.pointLocation = handPos;
            pt.pointVelocity = (handPos - lastPos).normalized;

            //setting up the audiosourcemixer pitch
            //Debug.Log("HandPos is: " + handPos);
            //HeightofSpawnedY = (int)Mathf.Round(handPos.y*10)/100; // 
            //Debug.Assert(HeightofSpawnedY >= 0 && HeightofSpawnedY < 20);
            //pt.GetComponent<AudioSource> ().outputAudioMixerGroup = AudioMixerGroupArray [HeightofSpawnedY];

            CurrentPointPrefab.GetComponent<AudioSource>().clip = TestClip; // Remove GetComponent later on.
                                                                            //pt.sample = TestClip;
            currentLine.GetComponent<Line>().AddPoint(pt,true);
            pt.sample = pt.GetComponent<AudioSource>();
            pt.sample.Play();
            currentTime = 0;
            lastPoint = handPos;
        }
        if (Vector3.Distance(handPos, lastPoint) > .1f)
        {
            currentCylinder = (GameObject)Instantiate(EmptyPointPrefab);
            MoveCurrentCylinder(lastPos, handPos);
            lastPoint = handPos;
        }
        lastPos = handPos;
    }


    //	public int ConvertHandPosToHeightInt (Vector3 CurrentHandPos){ // Dont ask
    //		int HeightToReturn;
    //		if (CurrentHandPos <= 0) {
    //			HeightToReturn = 0;
    //			return(HeightToReturn);
    //		}
    //		else if (CurrentHandPos>0 &&<=1){
    //			HeightToReturn = 1;
    //			return(HeightToReturn);
    //		}
    //		else if (CurrentHandPos>1 &&<2){
    //			HeightToReturn = 2;
    //			return(HeightToReturn);
    //		}
    //		else if (CurrentHandPos>1 &&<1){
    //			HeightToReturn = 0;
    //			return(HeightToReturn);
    //		}
    //		else if (CurrentHandPos>1 &&<1){
    //			HeightToReturn = 0;
    //			return(HeightToReturn);
    //		}
    //		else if (CurrentHandPos>1 &&<1){
    //			HeightToReturn = 0;
    //			return(HeightToReturn);
    //		}
    //		else if (CurrentHandPos>1 &&<1){
    //			HeightToReturn = 0;
    //			return(HeightToReturn);
    //		}
    //		else if (CurrentHandPos>1 &&<1){
    //			HeightToReturn = 0;
    //			return(HeightToReturn);
    //		}
    //		else if (CurrentHandPos>1 &&<1){
    //			HeightToReturn = 0;
    //			return(HeightToReturn);
    //		}
    //		else if (CurrentHandPos>1 &&<1){
    //			HeightToReturn = 0;
    //			return(HeightToReturn);
    //		}
    //
    //
    //	}



}
