using UnityEngine;
using System.Collections;

public class MouseGameLoop : MonoBehaviour
{
    
    public MouseBrush myBrush;
    // Use this for initialization
    void Start()
    {

        //leftcontroller = leftc.getcomponent<vream_controller>();
        //rightcontroller = rightc.getcomponent<vream_controller>();
        //console = gameobject.find("console").getcomponent<textmesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (myBrush.CurrentDrawingLineParent == null ||
                myBrush.CurrentDrawingLineParent.GetComponent<Line>().LineDrawn == true)
            {
                myBrush.StartDraw();
            }
            Vector3 pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5.0f);
            myBrush.UpdateDraw(Camera.main.ScreenToWorldPoint(pos), myBrush.CurrentDrawingLineParent);

        }
        else if (myBrush.CurrentDrawingLineParent != null)
        {
            myBrush.CurrentDrawingLineParent.GetComponent<Line>().LineDrawn = true;
        }

    }
}
