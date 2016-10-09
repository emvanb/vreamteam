using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLoop : MonoBehaviour {
	public GameObject effectTextObj;
	public GameObject soundTextObj;
	public GameObject leftSoundTextObj;
	private TextMesh effecttxt;
	private TextMesh soundtxt;
	private TextMesh leftSoundtxt;
    public TextMesh console;
	public GameObject leftC;
	public GameObject rightC;
    private Vream_Controller leftController;
    private Vream_Controller rightController;
    private int soundName = 0;
    private int effectName = 0;

	public AudioClip[] samples = new AudioClip[6];
	public AudioClip rightSample;
	public AudioClip leftSample;

	private GameObject CurrentPointPrefab;
	private GameObject CurrentLeftPointPrefab;
	private int rightIndexSample = 0;
	private int leftIndexSample = 0;
	private string[] sampleNames = new string[6];
	public List<GameObject> SpawnedLines = new List<GameObject>();

	public bool UseHarmoniousMixer;

	public BaseBrush rightBrush;
	public BaseBrush leftBrush;
    // Use this for initialization
	void Start () {
		rightSample = samples [rightIndexSample];
		leftSample = samples [leftIndexSample];
		leftController= leftC.GetComponent<Vream_Controller>();
		rightController= rightC.GetComponent<Vream_Controller>();
		leftBrush.TestClip=leftSample;
		rightBrush.TestClip=rightSample;
        //console = GameObject.Find("Console").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
		leftController= leftC.GetComponent<Vream_Controller>();
		rightController= rightC.GetComponent<Vream_Controller>();

		effecttxt = effectTextObj.GetComponent<TextMesh>();
		soundtxt = soundTextObj.GetComponent<TextMesh>();
		leftSoundtxt = leftSoundTextObj.GetComponent<TextMesh>();

        console.text ="heyyy!!";
		if (rightController.triggerPress) {
			console.text = "yo yo trigger pressed on right";
			if (rightBrush.CurrentDrawingLineParent == null || rightBrush.CurrentDrawingLineParent.GetComponent<Line> ().LineDrawn == true) {
				rightBrush.StartDraw (rightC.transform.position);
			} 
			/*else if (rightBrush.CurrentDrawingLineParent.GetComponent<Line> ().LineDrawn == true) {
				Debug.Log ("linedrawn");
				rightBrush.StartDraw (rightC.transform.position, rightBrush.CurrentDrawingLineParent);
			}*/
			rightBrush.UpdateDraw (rightC.transform.position);

		} else if(rightBrush.CurrentDrawingLineParent!=null) {
			rightBrush.UpdateLastCylinder ();
			rightBrush.CurrentDrawingLineParent.GetComponent<Line>().LineDrawn = true;
		}

        //increment sound
		if (rightController.dpadPressRight)
        {
            console.text = "right controller right";
			if (rightIndexSample < samples.Length-1)
				rightIndexSample++;
			else
				rightIndexSample=0;
			rightSample = samples [rightIndexSample];
			rightBrush.TestClip=rightSample;
			soundtxt.text = rightSample.name;
        }

		//decrement sound
        if (rightController.dpadPressLeft)
        {
            console.text = "right controllerleft";
			if (rightIndexSample > 0)
				rightIndexSample--;
			else
				rightIndexSample = samples.Length-1;
			rightSample = samples[rightIndexSample];

			rightBrush.TestClip=rightSample;
			soundtxt.text = rightSample.name;
        }

		if (rightController.dpadPressCenter)
		{
			console.text = "right controllerleft";
			if (CurrentPointPrefab != null) {
				Destroy (CurrentPointPrefab);
			}
		    CurrentPointPrefab = rightBrush.ProducePoint (rightC.transform.position);
			CurrentPointPrefab.GetComponentInChildren<MeshRenderer> ().enabled = false;
			LinePoint pt = CurrentPointPrefab.GetComponent<LinePoint> ();
			//pt.sample = TestClip;
			pt.sample = pt.GetComponent<AudioSource> ();
			pt.sample.Play ();

		}
		//LEFT CONTROLLER
		if (leftController.triggerPress) {
			console.text = "yo yo trigger pressed on right";
			if (leftBrush.CurrentDrawingLineParent == null || leftBrush.CurrentDrawingLineParent.GetComponent<Line> ().LineDrawn == true) {
				leftBrush.StartDraw (leftC.transform.position);
			} 
			/*else if (leftBrush.CurrentDrawingLineParent.GetComponent<Line> ().LineDrawn == true) {
				Debug.Log ("linedrawn");
				leftBrush.StartDraw (rightC.transform.position, leftBrush.CurrentDrawingLineParent);
			}*/
			leftBrush.UpdateDraw (leftC.transform.position);

		} else if(leftBrush.CurrentDrawingLineParent!=null) {
			rightBrush.UpdateLastCylinder ();
			leftBrush.CurrentDrawingLineParent.GetComponent<Line>().LineDrawn = true;
		}

		//increment sound
		if (leftController.dpadPressRight)
		{
			console.text = "right controller right";
			if (leftIndexSample < samples.Length-1)
				leftIndexSample++;
			else
				leftIndexSample=0;
			leftSample = samples [leftIndexSample];
			leftBrush.TestClip=leftSample;
			leftSoundtxt.text = leftSample.name;
		}

		//decrement sound
		if (leftController.dpadPressLeft)
		{
			console.text = "right controllerleft";
			if (leftIndexSample > 0)
				leftIndexSample--;
			else
				leftIndexSample = samples.Length-1;
			leftSample = samples[leftIndexSample];

			leftBrush.TestClip=leftSample;
			leftSoundtxt.text = leftSample.name;
		}

		if (leftController.dpadPressCenter)
		{
			console.text = "right controllerleft";
			if (CurrentLeftPointPrefab != null) {
				Destroy (CurrentLeftPointPrefab);
			}
			CurrentLeftPointPrefab = leftBrush.ProducePoint (leftC.transform.position);
			CurrentLeftPointPrefab.GetComponentInChildren<MeshRenderer> ().enabled = false;
			LinePoint pt = CurrentLeftPointPrefab.GetComponent<LinePoint> ();
			//pt.sample = TestClip;
			pt.sample = pt.GetComponent<AudioSource> ();
			pt.sample.Play ();

		}
		//increment effects
        //if (leftController.dpadPressRight)
        //{
          //  console.text = "leftcontroller right";
		//	effecttxt.text = "Effect_" + (soundName + 1).ToString();
      //  }

		//decrement effects
    //    if (leftController.dpadPressLeft)
        //{
      //      console.text = "leftcontrollerleft";
	//		effecttxt.text = "Effect_" + (soundName - 1).ToString();
//        }
		if (leftController.dpadPressDown ||rightController.dpadPressDown ) {
			DeleteLastLine ();
		}
    }

	public void DeleteLastLine(){
		if (SpawnedLines.Count > 0) {
			Destroy (SpawnedLines [SpawnedLines.Count - 1]);
			SpawnedLines.RemoveAt (SpawnedLines.Count - 1);
		}
	}
}
