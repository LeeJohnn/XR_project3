using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class boardStatus : MonoBehaviour
{
    public bool isEnter = false;
    private Vector3 boardAngle = Vector3.zero;
    private VRTK_InteractableObject obj;
    public GameObject che;
    private Vector3 [] facePosition;
    private int maxXindex = -1;
    private int maxYindex = -1;
    private float maxX = -Mathf.Infinity;
    private float maxY = -Mathf.Infinity;
    private Vector3 previousAngle = Vector3.zero;
    private bool isSnap = false;

    // Start is called before the first frame update
    void Start()
    {
        obj = new VRTK_InteractableObject();
        facePosition = new Vector3[6];
    }

    public void setisEnterTrue()
    {
        isEnter = true;
    }
    public void setisEnterFalse()
    {
        isEnter = false;
    }
    public void roateBoard()
    {
        isSnap = true;
    }
    
    private void getFacePosition(VRTK_InteractableObject objj)
    {
        for(int i=0; i<6; i++) {
            facePosition[i] = objj.transform.GetChild(i).transform.position;
        }
        maxX = -Mathf.Infinity;
        maxY = -Mathf.Infinity;
        maxXindex = -1;
        maxYindex = -1;
        for (int i=0; i<6; i++) {
            if(facePosition[i].x >= maxX && Mathf.Abs(facePosition[i].x - maxX) >= 0.01f)
            {
                maxX = facePosition[i].x;
                maxXindex = i;
            }
            if(facePosition[i].y >= maxY && Mathf.Abs(facePosition[i].y - maxY) >= 0.01f)
            {
                maxY = facePosition[i].y;
                maxYindex = i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<VRTK_SnapDropZone>().ValidSnappableObjectIsHovering()){
            obj = this.GetComponent<VRTK_SnapDropZone>().GetHoveringInteractableObjects()[0];
            getFacePosition(obj);
        }
        if (isSnap)
        {
            //Debug.Log("obj: " + obj.name);
            if (maxYindex == 0)
            {
                if (maxXindex == 1) //(0,0,0)
                    obj.transform.Rotate(0, 0, 0);
                else if (maxXindex == 2) //(0,270,0)
                    obj.transform.Rotate(0, 270, 0);
                else if (maxXindex == 3) //(0,180,0)
                    obj.transform.Rotate(0, 180, 0);
                else if (maxXindex == 4) //(0,90,0)
                    obj.transform.Rotate(0, 90, 0);
            }
            else if (maxYindex == 1)
            {
                if (maxXindex == 5) //(0,0,90)
                    obj.transform.Rotate(0, 0, 90);
                else if (maxXindex == 4) //(0,90,90)
                    obj.transform.Rotate(0, 90, 90);
                else if (maxXindex == 0) //(0,180,90)
                    obj.transform.Rotate(0, 180, 90);
                else if (maxXindex == 2) //(0,270,90)
                    obj.transform.Rotate(0, 270, 90);

            }
            else if (maxYindex == 2)
            {
                if (maxXindex == 1) //(90,0,0)
                    obj.transform.Rotate(90, 0, 0);
                else if (maxXindex == 0) //(90,90,0)
                    obj.transform.Rotate(90, 90, 0);
                else if (maxXindex == 3) //(90,180,0)
                    obj.transform.Rotate(90, 180, 0);
                else if (maxXindex == 5) //(90,270,0)
                    obj.transform.Rotate(90, 270, 0);
            }
            else if (maxYindex == 3)
            {
                if (maxXindex == 0) //(0,0,-90)
                    obj.transform.Rotate(0, 0, -90);
                else if (maxXindex == 4) //(0,90,-90)
                    obj.transform.Rotate(0, 90, -90);
                else if (maxXindex == 5) //(0,180,-90)
                    obj.transform.Rotate(0, 180, -90);
                else if (maxXindex == 2) //(0,270,-90)
                    obj.transform.Rotate(0, 270, -90);
            }
            else if (maxYindex == 4)
            {
                if (maxXindex == 5) //(270,0,90)
                    obj.transform.Rotate(270, 0, 90);
                else if (maxXindex == 3) //(270,90,90)
                    obj.transform.Rotate(270, 90, 90);
                else if (maxXindex == 0) //(270,180,90)
                    obj.transform.Rotate(270, 180, 90);
                else if (maxXindex == 1) //(270,270,90)
                    obj.transform.Rotate(270, 270, 90);
            }
            else if (maxYindex == 5)
            {
                if (maxXindex == 3) //(0,0,180)
                    obj.transform.Rotate(0, 0, 180);
                else if (maxXindex == 4) //(0,90,180)
                    obj.transform.Rotate(0, 90, 180);
                else if (maxXindex == 1) //(0,180,180)
                    obj.transform.Rotate(0, 180, 180);
                else if (maxXindex == 2) //(0,270,180)
                    obj.transform.Rotate(0, 270, 180);
            }
            isSnap = false;
            maxX = -Mathf.Infinity;
            maxY = -Mathf.Infinity;
            maxXindex = -1;
            maxYindex = -1;
            obj = null;
        }
    }
}
