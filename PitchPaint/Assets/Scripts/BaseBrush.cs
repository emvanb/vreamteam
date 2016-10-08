using UnityEngine;
using System.Collections;

public class BaseBrush : MonoBehaviour {
	public AudioSource sample;
	float timeBetweenPoints =1;
	float currentTime = 0;
	Vector3 lastPos = Vector3.zero;
	public GameObject PointPrefab;
	//public Line DrawingLine;
	public Vector3 LastMousePos;
	public Vector3 CurrentMousePos;
	public AudioClip TestClip;
	public GameObject DrawingLineParentPrefab;
	public GameObject CurrentDrawingLineParent;
	public float TimeOfLastPointSpawn;

    private Vector3 lastPoint;
	// Use this for initialization
	void Start () {
		TimeOfLastPointSpawn= 0;
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
		CurrentDrawingLineParent = (GameObject)Instantiate (DrawingLineParentPrefab);
		CurrentDrawingLineParent.transform.position = Input.mousePosition;
		CurrentDrawingLineParent.GetComponent<Line> ().startTime = Time.time;
        lastPoint =  new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5.0f);
        lastPoint = Camera.main.ScreenToWorldPoint(lastPoint);
            
    }
	public void UpdateDraw(Vector3 handPos, GameObject currentLine)
	{
		currentTime += Time.deltaTime;
		if (currentTime > timeBetweenPoints) {
			GameObject CurrentPointPrefab = (GameObject)Instantiate (PointPrefab, currentLine.transform);
			LinePoint pt = CurrentPointPrefab.GetComponent<LinePoint> ();
            CurrentPointPrefab.transform.position = (handPos + lastPoint) /2.0f;
            CurrentPointPrefab.transform.LookAt(handPos);
            CurrentPointPrefab.transform.localScale = new Vector3(.5f,.5f,(handPos - lastPoint).magnitude *.8f );
            //LinePoint pt = new LinePoint ();
            pt.creationTime = Time.time - currentLine.GetComponent<Line>().startTime;
			pt.pointLocation = handPos;
			pt.pointVelocity = (handPos - lastPos).normalized;
			CurrentPointPrefab.GetComponent<AudioSource> ().clip = TestClip; // Remove GetComponent later on.
			//pt.sample = TestClip;
			currentLine.GetComponent<Line>().AddPoint(pt);
			pt.sample = pt.GetComponent<AudioSource> ();
			pt.sample.Play ();
			currentTime = 0;
            lastPoint = handPos;
		}
		lastPos = handPos;
	}
}
