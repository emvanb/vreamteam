﻿using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class BaseBrush : MonoBehaviour {
	public GameObject main;
	private GameLoop liveGameLoop;
	float timeBetweenPoints = .6f;
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
	public AudioMixerGroup[] HarmoniousGroupArray;
	public int HeightofSpawnedY;

    private Vector3 lastPoint;
    private GameObject currentCylinder;
	// Use this for initialization
	void Start () {
		TimeOfLastPointSpawn= 0;
		liveGameLoop = main.GetComponent<GameLoop> ();
		//TestClip = liveGameLoop.currentSample;
		CurrentDrawingLineParent = (GameObject)Instantiate (DrawingLineParentPrefab);
		CurrentDrawingLineParent.GetComponent<Line> ().LineDrawn = true;
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
	public void StartDraw(Vector3 handPos)
	{
		//TestClip = liveGameLoop.currentSample;

		CurrentDrawingLineParent = (GameObject)Instantiate (DrawingLineParentPrefab);
		liveGameLoop.SpawnedLines.Add (CurrentDrawingLineParent);
		CurrentDrawingLineParent.transform.position = handPos;
		CurrentDrawingLineParent.GetComponent<Line> ().startTime = Time.time;
		lastPoint = handPos;
		lastPos = lastPoint + Vector3.one * .05f;
		currentTime = 0;
		

		GameObject CurrentPointPrefab = ProducePoint (handPos);
		CurrentPointPrefab.transform.parent = CurrentDrawingLineParent.transform;
		LinePoint pt = CurrentPointPrefab.GetComponent<LinePoint> ();
		pt.creationTime -=CurrentDrawingLineParent.GetComponentInChildren<Line>().startTime;


        currentCylinder = (GameObject)Instantiate(EmptyPointPrefab, CurrentDrawingLineParent.transform);
		CurrentDrawingLineParent.GetComponent<Line>().AddPoint(currentCylinder.GetComponent<LinePoint>(), false);
        MoveCurrentCylinder(lastPoint, lastPoint + Vector3.one * .05f);

        //pt.sample = TestClip;
		CurrentDrawingLineParent.GetComponent<Line>().AddPoint(pt,true);
		pt.sample = pt.GetComponent<AudioSource> ();

		if(!pt.sample.isPlaying)
		{
			pt.sample.Play ();
    	}
	}

    void MoveCurrentCylinder(Vector3 lastPos, Vector3 currentPos)
    {
        currentCylinder.transform.position = (currentPos + lastPos) / 2.0f;
        currentCylinder.transform.LookAt(currentPos);
        currentCylinder.transform.localScale = new Vector3(.05f, .05f, (currentPos - lastPos).magnitude * .8f);
    }
	public void UpdateDraw(Vector3 handPos )
	{
		float speed = ((handPos - lastPos)/Time.deltaTime).magnitude;
		currentTime += Time.deltaTime * speed;
		MoveCurrentCylinder(lastPoint, handPos);
		if (currentTime > timeBetweenPoints) {
			
			GameObject CurrentPointPrefab = ProducePoint (handPos);
			CurrentPointPrefab.transform.parent = CurrentDrawingLineParent.transform;
			LinePoint pt = CurrentPointPrefab.GetComponent<LinePoint> ();
			pt.creationTime -=CurrentDrawingLineParent.GetComponent<Line>().startTime;

			//pt.sample = TestClip;
			CurrentDrawingLineParent.GetComponent<Line>().AddPoint(pt,true);

			pt.sample = pt.GetComponent<AudioSource> ();
			if(!pt.sample.isPlaying)
				pt.sample.Play ();
			currentTime = 0;
            lastPoint = handPos;
		}
		if (Vector3.Distance(handPos, lastPoint) > .1f)
		{
			HeightofSpawnedY = (int)Mathf.Round((currentCylinder.transform.position.y-0.5f)*10);
			currentCylinder.GetComponent<LinePoint> ().SetHeightColor (HeightofSpawnedY);
			currentCylinder.GetComponent<LinePoint> ().UpdateStartAndEnd ();
			currentCylinder = (GameObject)Instantiate(EmptyPointPrefab,CurrentDrawingLineParent.transform);
			CurrentDrawingLineParent.GetComponent<Line>().AddPoint(currentCylinder.GetComponent<LinePoint>(), false);
			MoveCurrentCylinder(lastPos, handPos);
			lastPoint = handPos;
		}
        lastPos = handPos;
	}
	public void UpdateLastCylinder()
	{
		if(currentCylinder!=null)
		{
		currentCylinder.GetComponent<LinePoint> ().SetHeightColor (HeightofSpawnedY);
		currentCylinder.GetComponent<LinePoint> ().UpdateStartAndEnd ();
		}
	}
	public GameObject ProducePoint(Vector3 position)
	{
		GameObject CurrentPointPrefab = (GameObject)Instantiate (PointPrefab);
		LinePoint pt = CurrentPointPrefab.GetComponent<LinePoint> ();
		CurrentPointPrefab.transform.position = (position + lastPoint) /2.0f;
		CurrentPointPrefab.transform.LookAt(position);
		CurrentPointPrefab.transform.localScale = new Vector3(.05f,.05f,Mathf.Max((position - lastPoint).magnitude *.8f ,.005f));
		//LinePoint pt = new LinePoint ();
		pt.creationTime = Time.time;// - currentLine.GetComponent<Line>().startTime;
		pt.pointLocation = position;
		pt.pointVelocity = (position - lastPos)/Time.deltaTime;


		//setting up the audiosourcemixer pitch
		HeightofSpawnedY = (int)Mathf.Round((position.y-0.5f)*10); // 
		HeightofSpawnedY = Mathf.Clamp(HeightofSpawnedY,0,AudioMixerGroupArray.Length-1);

		Debug.Assert(HeightofSpawnedY>=0 && HeightofSpawnedY<20);
		if (!liveGameLoop.UseHarmoniousMixer)
			pt.GetComponent<AudioSource> ().outputAudioMixerGroup = AudioMixerGroupArray [HeightofSpawnedY];
		else {
			if (HeightofSpawnedY > HarmoniousGroupArray.Length-1) {
				HeightofSpawnedY =  HarmoniousGroupArray.Length-1;
			}
			pt.GetComponent<AudioSource> ().outputAudioMixerGroup = HarmoniousGroupArray [HeightofSpawnedY];
		

		}
		pt.SetHeightColor (HeightofSpawnedY);

		//setting up the audiosource volume
		//pt.GetComponent<AudioSource>().volume = pt.pointVelocity;

		CurrentPointPrefab.GetComponent<AudioSource> ().clip = TestClip; // Remove GetComponent later on.
		return CurrentPointPrefab;
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
