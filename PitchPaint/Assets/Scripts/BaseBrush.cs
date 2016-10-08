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

	void PlaySample()
	{
		sample.Play ();
	}

	void UpdateDraw(Vector3 handPos, Line currentLine)
	{
		currentTime += Time.deltaTime;
		if (currentTime > timeBetweenPoints) {
			LinePoint pt = new LinePoint ();
			pt.CreationTime = Time.time - currentLine.startTime;
			pt.PointLocation = handPos;
			pt.Velocity = (handPos - lastPos).normalized;
			currentLine.AddPoint(pt);
		}
		lastPos = handPos;
	}
}
