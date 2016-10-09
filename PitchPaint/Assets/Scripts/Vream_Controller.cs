using UnityEngine;
using System.Collections;

public class Vream_Controller : MonoBehaviour {

    public bool triggerPress = false;
    public bool dpadPressUp = false;
    public bool dpadPressDown = false;
	public bool dpadPressCenter = false;
    public bool dpadPressLeft = false;
    public bool dpadPressRight = false;
    //coordinates are a float from 0-1. Where origin is center  of the touch pad and  top right corner  is 1,1
    private float dpadCoordX = 0;
    private float dpadCoordY=0;

	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private Valve.VR.EVRButtonId padButton = Valve.VR.EVRButtonId.k_EButton_Axis1;

    private SteamVR_Controller.Device controller {
		get { return SteamVR_Controller.Input((int)trackedObj.index);
		}
    }

	public SteamVR_TrackedObject trackedObj;
	void Start() {
		//trackedObj = GetComponent();rer
	}

	void Update() {
        dpadPressUp = false;
        dpadPressDown = false;
        dpadPressLeft = false;
		dpadPressRight = false;
		dpadPressCenter = false;
        dpadCoordX = controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x;
        dpadCoordY = controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y;

        if (controller == null) {
			Debug.Log("Controller not initialized");
			return;
		}

		if(controller.GetPressDown(triggerButton))
		{
			triggerPress = true;
		}
		if (controller.GetPressUp (triggerButton)) {
			triggerPress = false;
		}
		if (controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) && dpadCoordX>.3333)
        {
            dpadPressRight = true;
			Debug.Log("pad right");
        }

		if (controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) && dpadCoordX<-.3333)
        {
			dpadPressLeft = true;
			Debug.Log("pad  left");
        }
			
		if (controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) && dpadCoordX>=-.3333 && dpadCoordX<=.3333 &&dpadCoordY>-0.2f )
		{
			dpadPressCenter = true;
			Debug.Log("pad  center");
		}
		if (controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) && dpadCoordX>=-.3333 && dpadCoordX<=.3333 &&dpadCoordY<=-0.2f )
		{
			dpadPressDown = true;
			Debug.Log("pad  down");
		}
    }

}


