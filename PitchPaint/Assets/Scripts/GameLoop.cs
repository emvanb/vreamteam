using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour {
    public GameObject effectTextObj;
    public GameObject soundTextObj;
	private TextMesh effecttxt;
	private TextMesh soundtxt;
    public TextMesh console;
	public GameObject leftC;
	public GameObject rightC;
    private Vream_Controller leftController;
    private Vream_Controller rightController;
    private int soundName = 0;
    private int effectName = 0;

	public AudioClip[] samples = new AudioClip[6];
	public AudioClip currentSample;
	private int indexSample = 0;
	private string[] sampleNames = new string[6];

	public BaseBrush myBrush;
    // Use this for initialization
    void Start () {
		currentSample = samples [indexSample];
		leftController= leftC.GetComponent<Vream_Controller>();
		rightController= rightC.GetComponent<Vream_Controller>();
        console = GameObject.Find("Console").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
		leftController= leftC.GetComponent<Vream_Controller>();
		rightController= rightC.GetComponent<Vream_Controller>();

		effecttxt = effectTextObj.GetComponent<TextMesh>();
		soundtxt = soundTextObj.GetComponent<TextMesh>();

        console.text ="heyyy!!";
		if (rightController.triggerPress) {
			console.text = "yo yo trigger pressed on right";
			if (myBrush.CurrentDrawingLineParent == null || 
				myBrush.CurrentDrawingLineParent.GetComponent<Line>().LineDrawn == true) {
				myBrush.StartDraw ();
			}
			myBrush.UpdateDraw (Input.mousePosition, myBrush.CurrentDrawingLineParent);

		} else if(myBrush.CurrentDrawingLineParent!=null) {
			myBrush.CurrentDrawingLineParent.GetComponent<Line>().LineDrawn = true;
		}

        //increment sound
		if (rightController.dpadPressRight)
        {
            console.text = "right controller right";
			if (indexSample < samples.Length-1)
				indexSample++;
			else
				indexSample=0;
			currentSample = samples [indexSample];
			soundtxt.text = currentSample.name;
        }

		//decrement sound
        if (rightController.dpadPressLeft)
        {
            console.text = "right controllerleft";
			if (indexSample > 0)
				indexSample--;
			else
				indexSample = samples.Length-1;
			currentSample = samples[indexSample];
			soundtxt.text = currentSample.name;
        }

		//increment effects
        if (leftController.dpadPressRight)
        {
            console.text = "leftcontroller right";
			effecttxt.text = "Effect_" + (soundName + 1).ToString();
        }

		//decrement effects
        if (leftController.dpadPressLeft)
        {
            console.text = "leftcontrollerleft";
			effecttxt.text = "Effect_" + (soundName - 1).ToString();
        }
    }
}
