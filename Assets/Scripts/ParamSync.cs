using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using VRTK;

public class ParamSync : MonoBehaviourPunCallbacks, IPunObservable
{
    public bool isWin = false;
    public bool isFinish = false;
    public int win_player = -1;
    public int turn_player = -1;
    public int time_i = -1;
    GameObject gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        gm = GameObject.Find("GameManager");
        if (photonView.IsMine)
        {
            
            this.isWin = gm.GetComponent<GameManager>().isWin;
            this.isFinish = gm.GetComponent<GameManager>().isFinish;
            this.win_player = gm.GetComponent<GameManager>().win_player;
            this.turn_player = gm.GetComponent<GameManager>().turn_player;
            this.time_i = gm.GetComponent<GameManager>().timer_i;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream,
    PhotonMessageInfo info)
    {
        
        if (stream.IsWriting)
        {
            // 為玩家本人的狀態, 將 IsFiring 的狀態更新給其他玩家
            stream.SendNext(isWin);
            stream.SendNext(isFinish);
            stream.SendNext(win_player);
            stream.SendNext(turn_player);
            stream.SendNext(time_i);
        }
        else
        {
                
            // 非為玩家本人的狀態, 單純接收更新的資料   
            this.isWin = (bool)stream.ReceiveNext();
            this.isFinish = (bool)stream.ReceiveNext();
            this.win_player = (int)stream.ReceiveNext();
            this.turn_player = (int)stream.ReceiveNext();
            this.time_i = (int)stream.ReceiveNext();
            if (gm != null)
            {
                gm.GetComponent<GameManager>().isWin = this.isWin;
                gm.GetComponent<GameManager>().isFinish = this.isFinish;
                gm.GetComponent<GameManager>().win_player = this.win_player;
                gm.GetComponent<GameManager>().turn_player = this.turn_player;
                gm.GetComponent<GameManager>().timer_i = this.time_i;
                Debug.Log(this.turn_player);
            }
        }
    }
}
