using UnityEngine;
using System.Collections;

public class BaseBrush : MonoBehaviour {
	public AudioSource sample;
	float timeBetweenPoints =1;
	float currentTime = 0;
	Vector3 lastPos = Vector3.zero;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateDraw(Vector3 handPos, Line currentLine)
	{
		currentTime += Time.deltaTime;
		if (currentTime > timeBetweenPoints) {
			LinePoint pt = new LinePoint ();
			pt.creationTime = Time.time - currentLine.startTime;
			pt.pointLocation = handPos;
			pt.pointVelocity = (handPos - lastPos).normalized;
			currentLine.AddPoint(pt);
			pt.sample.Play ();
		}
		lastPos = handPos;
	}
}
