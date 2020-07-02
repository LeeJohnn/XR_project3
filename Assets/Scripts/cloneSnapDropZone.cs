using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VRTK;

public class cloneSnapDropZone : MonoBehaviour
{
    public GameObject chess;
    public GameObject text;
    public GameObject snapzone;
    private int remain = 6;
    // Start is called before the first frame update
    void Start()
    {
        //this.GetComponent<VRTK_SnapDropZone>().ForceSnap(chess);
    }

    // Update is called once per frame
    void Update()
    {
        // remain = remain - snapzone.GetComponent<VRTK_SnapDropZone>().cnt;
        // text.transform.GetComponent<TextElement>().text = "remain: " + remain.ToString();
    }
}
