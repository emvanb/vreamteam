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
    // Use this for initialization
    void Start () {

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
        if (rightController.triggerPress)
        {
            console.text = "yo yo trigger pressed on right";
        }

        if (rightController.dpadPressRight)
        {
            console.text = "right controller right";
			soundtxt.text = "Sound_" + (soundName + 1).ToString();
        }

        if (rightController.dpadPressLeft)
        {
            console.text = "right controllerleft";
			soundtxt.text = "Sound_" + (soundName - 1).ToString();
        }
        if (leftController.dpadPressRight)
        {
            console.text = "leftcontroller right";
			effecttxt.text = "Effect_" + (soundName + 1).ToString();
        }

        if (leftController.dpadPressLeft)
        {
            console.text = "leftcontrollerleft";
			effecttxt.text = "Effect_" + (soundName - 1).ToString();
        }
    }
}
