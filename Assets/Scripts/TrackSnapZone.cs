using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using VRTK;

public class TrackSnapZone : MonoBehaviourPunCallbacks,IPunObservable
{
    private string snap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.GetComponent<VRTK_InteractableObject>().IsInSnapDropZone())
        {
            if(this.gameObject.GetComponent<VRTK_InteractableObject>().GetStoredSnapDropZone().tag != "OriginalSnapZone")
                snap = this.gameObject.GetComponent<VRTK_InteractableObject>().GetStoredSnapDropZone().name;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream,
    PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 為玩家本人的狀態, 將 IsFiring 的狀態更新給其他玩家
            stream.SendNext(snap);
        }
        else
        {
            // 非為玩家本人的狀態, 單純接收更新的資料
            this.snap = (string)stream.ReceiveNext();
            if(snap != null)
                GameObject.Find(snap).GetComponent<VRTK_SnapDropZone>().ForceSnap(this.gameObject);
        }
    }
}
