using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour {
    private TextMesh effectText;
    private TextMesh soundText;
    public TextMesh console;
    private Vream_Controller leftController;
    private Vream_Controller rightController;
    private int soundName = 0;
    private int effectName = 0;
    // Use this for initialization
    void Start () {
        effectText = GameObject.Find("Effect_Text").GetComponent<TextMesh>();
        soundText = GameObject.Find("Sound_Text").GetComponent<TextMesh>();
        rightController = GameObject.Find("Controller (right)").GetComponent<Vream_Controller>();
        leftController = GameObject.Find("Controller (left)").GetComponent<Vream_Controller>();
        console = GameObject.Find("Console").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        console.text ="heyyy!!";
        if (rightController.triggerPress)
        {
            console.text = "yo yo trigger pressed on right";
        }

        if (rightController.dpadPressRight)
        {
            console.text = "right controller right";

            soundText.text = "Sound_" + (soundName + 1).ToString();
        }

        if (rightController.dpadPressLeft)
        {
            console.text = "right controllerleft";

            soundText.text = "Sound_" + (soundName - 1).ToString();
        }
        if (leftController.dpadPressRight)
        {
            console.text = "leftcontroller right";

            soundText.text = "Effect_" + (soundName + 1).ToString();
        }

        if (leftController.dpadPressLeft)
        {
            console.text = "leftcontrollerleft";

            soundText.text = "Effect_" + (soundName - 1).ToString();
        }
    }
}
