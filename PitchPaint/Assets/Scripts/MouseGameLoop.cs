﻿using UnityEngine;
using System.Collections;

public class MouseGameLoop : MonoBehaviour
{
    
    public BaseBrush myBrush;
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
            myBrush.UpdateDraw(Input.mousePosition, myBrush.CurrentDrawingLineParent);

        }
        else if (myBrush.CurrentDrawingLineParent != null)
        {
            myBrush.CurrentDrawingLineParent.GetComponent<Line>().LineDrawn = true;
        }

    }
}
