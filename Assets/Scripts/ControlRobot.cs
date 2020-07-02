using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRobot : MonoBehaviour
{
    private GameObject cam;
    private Quaternion rotation;
    private float distance = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
        rotation = this.transform.rotation;
        this.transform.Rotate(0, 180, 0);
    }
    private void LateUpdate()
    {
        this.transform.rotation = rotation;
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.position = cam.transform.position + cam.transform.forward * distance + new Vector3(0, -0.5f, 0);
    }
}
