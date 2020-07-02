using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GetChessColor : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3[] facePosition;
    private int maxXindex = -1;
    private int maxYindex = -1;
    private float maxX = -Mathf.Infinity;
    private float maxY = -Mathf.Infinity;
    private int[] indexList;

    void Start()
    {
        facePosition = new Vector3[6];
        indexList = new int[6] { 0, 1, 2, 3, 4, 5 };
    }
    public string GetColorYplus()
    {
        return this.transform.GetChild(indexList[0]).tag;
    }
    public string GetColorXplus()
    {
        return this.transform.GetChild(indexList[1]).tag;
    }
    public string GetColorZplus()
    {
        return this.transform.GetChild(indexList[4]).tag;
    }
    public string GetColorYminus()
    {
        return this.transform.GetChild(indexList[5]).tag;
    }
    public string GetColorXminus()
    {
        return this.transform.GetChild(indexList[3]).tag;
    }
    public string GetColorZminus()
    {
        return this.transform.GetChild(indexList[2]).tag;
    }
    private void getFacePosition(GameObject objj)
    {
        for (int i = 0; i < 6; i++)
        {
            facePosition[i] = objj.transform.GetChild(i).transform.position;
        }
        maxX = -Mathf.Infinity;
        maxY = -Mathf.Infinity;
        maxXindex = -1;
        maxYindex = -1;
        for (int i = 0; i < 6; i++)
        {
            if (facePosition[i].x >= maxX && Mathf.Abs(facePosition[i].x - maxX) >= 0.01f)
            {
                maxX = facePosition[i].x;
                maxXindex = i;
            }
            if (facePosition[i].y >= maxY && Mathf.Abs(facePosition[i].y - maxY) >= 0.01f)
            {
                maxY = facePosition[i].y;
                maxYindex = i;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        getFacePosition(this.gameObject);
        if (maxYindex == 0)
        {
            if (maxXindex == 1) //(0,0,0)
                indexList = new int[6] { 0, 1, 2, 3, 4, 5 };
            else if (maxXindex == 2) //(0,270,0)
                indexList = new int[6] { 0, 2, 3, 4, 1, 5 };
            else if (maxXindex == 3) //(0,180,0)
                indexList = new int[6] { 0, 3, 4, 1, 2, 5 };
            else if (maxXindex == 4) //(0,90,0)
                indexList = new int[6] { 0, 4, 1, 2, 3, 5 };
        }
        else if (maxYindex == 1)
        {
            if (maxXindex == 5) //(0,0,90)
                indexList = new int[6] { 1, 5, 2, 0, 4, 3 };
            else if (maxXindex == 4) //(0,90,90)
                indexList = new int[6] { 1, 4, 5, 2, 0, 3 };
            else if (maxXindex == 0) //(0,180,90)
                indexList = new int[6] { 1, 0, 4, 5, 2, 3 };
            else if (maxXindex == 2) //(0,270,90)
                indexList = new int[6] { 1, 2, 0, 4, 5, 3 };

        }
        else if (maxYindex == 2)
        {
            if (maxXindex == 1) //(90,0,0)
                indexList = new int[6] { 2, 1, 5, 3, 0, 4 };
            else if (maxXindex == 0) //(90,90,0)
                indexList = new int[6] { 2, 0, 1, 5, 3, 4 };
            else if (maxXindex == 3) //(90,180,0)
                indexList = new int[6] { 2, 3, 0, 1, 5, 4 };
            else if (maxXindex == 5) //(90,270,0)
                indexList = new int[6] { 2, 5, 3, 0, 1, 4 };
        }
        else if (maxYindex == 3)
        {
            if (maxXindex == 0) //(0,0,-90)
                indexList = new int[6] { 3, 0, 2, 5, 4, 1 };
            else if (maxXindex == 4) //(0,90,-90)
                indexList = new int[6] { 3, 4, 0, 2, 5, 1 };
            else if (maxXindex == 5) //(0,180,-90)
                indexList = new int[6] { 3, 5, 4, 0, 2, 1 };
            else if (maxXindex == 2) //(0,270,-90)
                indexList = new int[6] { 3, 2, 5, 4, 0, 1 };
        }
        else if (maxYindex == 4)
        {
            if (maxXindex == 5) //(270,0,90)
                indexList = new int[6] { 4, 5, 1, 0, 3, 2 };
            else if (maxXindex == 3) //(270,90,90)
                indexList = new int[6] { 4, 3, 5, 1, 0, 2 };
            else if (maxXindex == 0) //(270,180,90)
                indexList = new int[6] { 4, 0, 3, 5, 1, 2 };
            else if (maxXindex == 1) //(270,270,90)
                indexList = new int[6] { 4, 1, 0, 3, 5, 2 };
        }
        else if (maxYindex == 5)
        {
            if (maxXindex == 3) //(0,0,180)
                indexList = new int[6] { 5, 3, 2, 1, 4, 0 };
            else if (maxXindex == 4) //(0,90,180)
                indexList = new int[6] { 5, 4, 3, 2, 1, 0 };
            else if (maxXindex == 1) //(0,180,180)
                indexList = new int[6] { 5, 1, 4, 3, 2, 0 };
            else if (maxXindex == 2) //(0,270,180)
                indexList = new int[6] { 5, 2, 1, 4, 3, 0 };
        }
        maxX = -Mathf.Infinity;
        maxY = -Mathf.Infinity;
        maxXindex = -1;
        maxYindex = -1;
    }
}
