using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using VRTK;

public class SoundSync : MonoBehaviourPunCallbacks, IPunObservable
{
    public int playSoundID = -1;
    public AudioSource audioPlayer;
    public AudioClip[] clips;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }

    public void OnPhotonSerializeView(PhotonStream stream,
    PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {
            // 為玩家本人的狀態, 將 IsFiring 的狀態更新給其他玩家
            stream.SendNext(playSoundID);
           
        }
        else
        {

            // 非為玩家本人的狀態, 單純接收更新的資料   
            this.playSoundID = (int)stream.ReceiveNext();
            if(this.playSoundID != -1)
            {
                audioPlayer.clip = clips[this.playSoundID];
                audioPlayer.PlayOneShot(audioPlayer.clip);
            }
          
        }
    }
}
