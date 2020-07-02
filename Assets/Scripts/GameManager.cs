using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using VRTK;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject[] playerarea;
    [Tooltip("Prefab- 玩家的角色")]
    public GameObject chessPrefab0;
    public GameObject chessPrefab1;
    public GameObject chessPrefab2;
    public GameObject chessboard;
    public GameObject[] smallboard;
    public Material[] m;
    public GameObject [] TimeText;
    public GameObject robot;
    public GameObject cam;
    public GameObject LeftControllerScriptAlias;
    public GameObject RightControllerScriptAlias;

    private AudioSource winAudio;
    private AudioSource loseAudio;
    private AudioSource BGM1, BGM2, BGM3, BGM4, BGM5;
    private AudioSource tiktokAudio;
    //private GameObject chess0, chess1, chess2;

    public int playerId = 0;
    private int chessNum = 0;
    private int lastchessNum = 0;

    private GameObject[] winChess = new GameObject[9];
    private GameObject[] loseChess = new GameObject[9];
    private float camRotationY;

    private GameObject b1;
    private GameObject b2;
    private Quaternion rotation;
    private float distance = 1f;
    private float diff;
    public float timer_f = 31.0f;
    public int timer_i = 31;


    int[,,] chessboard_surface = new int[6, 7, 7];
    int[,,] chessboard_depth = new int[6, 7, 7];

    public int win_player = -1;
    public bool isWin = false;
    public int turn_player;
    public bool isFinish = false;

    private GameObject pm;
    public GameObject pm_prefab;

    private GameObject sm;
    public GameObject sm_prefab;
    bool first = false;

    public bool isMyTurn = false;
    void Start()
    {
        turn_player = 0;
        camRotationY = cam.transform.localEulerAngles.y;
        BGM1 = GameObject.FindGameObjectWithTag("BGM1").GetComponent<AudioSource>();
        BGM2 = GameObject.FindGameObjectWithTag("BGM2").GetComponent<AudioSource>();
        BGM3 = GameObject.FindGameObjectWithTag("BGM3").GetComponent<AudioSource>();
        BGM4 = GameObject.FindGameObjectWithTag("BGM4").GetComponent<AudioSource>();
        BGM5 = GameObject.FindGameObjectWithTag("BGM5").GetComponent<AudioSource>();
        winAudio = GameObject.FindGameObjectWithTag("win").GetComponent<AudioSource>();
        loseAudio = GameObject.FindGameObjectWithTag("lose").GetComponent<AudioSource>();
        tiktokAudio = GameObject.FindGameObjectWithTag("tiktok").GetComponent<AudioSource>();
        BGM1.Play();
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                for (int k = 0; k < 7; k++)
                {
                    chessboard_surface[i, j, k] = -1;
                    chessboard_depth[i, j, k] = -1;
                }
            }
        }

        if (chessPrefab0 == null)
        {
            Debug.LogError("playerPrefab 遺失, 請在 Game Manager 重新設定",
                this);
        }
        else
        {
            // only master will generate the chess and other items
            if (PhotonNetwork.IsMasterClient) //player1
            {
                pm = PhotonNetwork.Instantiate(this.pm_prefab.name, new Vector3(-1.26425f, 0.5f, 0.2774169f), Quaternion.identity, 0);
                sm = PhotonNetwork.Instantiate(this.sm_prefab.name, new Vector3(-1.26425f, 0.5f, 0.2774169f), Quaternion.identity, 0);
           
                playerId = 0;
                sm.transform.tag = "sm" + playerId;
                var chess0 = PhotonNetwork.Instantiate(this.chessPrefab0.name, new Vector3(-1.26425f, 0.5f, 0.2774169f), Quaternion.identity, 0);
                chess0.transform.parent = playerarea[0].transform.GetChild(0).transform;
                chess0.transform.Translate(0, -100, 0);
                playerarea[0].transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<VRTK_SnapDropZone>().ForceSnap(chess0);
                playerarea[0].transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<VRTK_SnapDropZone>().isGenerate = true;

                var chess1 = PhotonNetwork.Instantiate(this.chessPrefab1.name, new Vector3(-1.26425f, 0.5f, 0.2774169f), Quaternion.identity, 0);
                chess1.transform.parent = playerarea[0].transform.GetChild(1).transform;
                chess1.transform.Translate(0, -100, 0);
                playerarea[0].transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).GetComponent<VRTK_SnapDropZone>().ForceSnap(chess1);
                playerarea[0].transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).GetComponent<VRTK_SnapDropZone>().isGenerate = true;

                var chess2 = PhotonNetwork.Instantiate(this.chessPrefab2.name, new Vector3(-1.26425f, 0.5f, 0.2774169f), Quaternion.identity, 0);
                chess2.transform.parent = playerarea[0].transform.GetChild(2).transform;
                chess2.transform.Translate(0, -100, 0);
                playerarea[0].transform.GetChild(2).transform.GetChild(0).transform.GetChild(2).GetComponent<VRTK_SnapDropZone>().ForceSnap(chess2);
                playerarea[0].transform.GetChild(2).transform.GetChild(0).transform.GetChild(2).GetComponent<VRTK_SnapDropZone>().isGenerate = true;

                //Generate win chess
                for (int i = 0; i < 9; i++)
                {

                    var c = PhotonNetwork.Instantiate(this.chessPrefab0.name, new Vector3(999f, 999f, 999f), Quaternion.identity, 0);
                    winChess[i] = c;
                    winChess[i].SetActive(false);
                    var l = PhotonNetwork.Instantiate(this.chessPrefab0.name, new Vector3(999f, 999f, 999f), Quaternion.identity, 0);
                    loseChess[i] = l;
                    loseChess[i].SetActive(false);
                }

                //Generate robot
                b1 = PhotonNetwork.Instantiate(this.robot.name, new Vector3(0, 0, 0), Quaternion.identity, 0);
                rotation = b1.transform.rotation;
            }
            else
            { //player2
                sm = PhotonNetwork.Instantiate(this.sm_prefab.name, new Vector3(-1.26425f, 0.5f, 0.2774169f), Quaternion.identity, 0);

                playerId = 1;
                sm.tag = "sm" + playerId;
                var chess0 = PhotonNetwork.Instantiate(this.chessPrefab0.name, new Vector3(-1.26425f, 0.5f, 0.2774169f), Quaternion.identity, 0);
                chess0.transform.parent = playerarea[1].transform.GetChild(0).transform;
                chess0.transform.Translate(0, -100, 0);
                playerarea[1].transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<VRTK_SnapDropZone>().ForceSnap(chess0);
                playerarea[1].transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<VRTK_SnapDropZone>().isGenerate = true;

                var chess1 = PhotonNetwork.Instantiate(this.chessPrefab1.name, new Vector3(-1.26425f, 0.5f, 0.2774169f), Quaternion.identity, 0);
                chess1.transform.parent = playerarea[1].transform.GetChild(1).transform;
                chess1.transform.Translate(0, -100, 0);
                playerarea[1].transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).GetComponent<VRTK_SnapDropZone>().ForceSnap(chess1);
                playerarea[1].transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).GetComponent<VRTK_SnapDropZone>().isGenerate = true;

                var chess2 = PhotonNetwork.Instantiate(this.chessPrefab2.name, new Vector3(-1.26425f, 0.5f, 0.2774169f), Quaternion.identity, 0);
                chess2.transform.parent = playerarea[1].transform.GetChild(2).transform;
                chess2.transform.Translate(0, -100, 0);
                playerarea[1].transform.GetChild(2).transform.GetChild(0).transform.GetChild(2).GetComponent<VRTK_SnapDropZone>().ForceSnap(chess2);
                playerarea[1].transform.GetChild(2).transform.GetChild(0).transform.GetChild(2).GetComponent<VRTK_SnapDropZone>().isGenerate = true;

                //Generate robot
                b2 = PhotonNetwork.Instantiate(this.robot.name, new Vector3(0, 0, 0), Quaternion.identity, 0);
                rotation = b2.transform.rotation;
            }

        }
    }
    private void LateUpdate()
    {
        if (PhotonNetwork.IsMasterClient) //player1
        {
            b1.transform.Rotate(0, diff, 0);
            //b1.transform.position = new Vector3(b1.transform.localPosition.x, -0.007f, b1.transform.localPosition.z);
        }
        else
        {
            b2.transform.Rotate(0, diff, 0);
            //b2.transform.position = new Vector3(b2.transform.localPosition.x, -0.007f, b2.transform.localPosition.z);
        }
        

    }
    private void changeChessState()
    {
        if (isMyTurn)
        {
            LeftControllerScriptAlias.GetComponent<VRTK_InteractGrab>().enabled = true;
            RightControllerScriptAlias.GetComponent<VRTK_InteractGrab>().enabled = true;
        }
        else
        {
            LeftControllerScriptAlias.GetComponent<VRTK_InteractGrab>().enabled = false;
            RightControllerScriptAlias.GetComponent<VRTK_InteractGrab>().enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("player num" + PhotonNetwork.PlayerList.Length);
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.PlayerList.Length < 2)
            {
                LeftControllerScriptAlias.GetComponent<VRTK_InteractGrab>().enabled = false;
                RightControllerScriptAlias.GetComponent<VRTK_InteractGrab>().enabled = false;
                timer_f = 31.0f;
            }
            else
            {
                if(turn_player == 0)
                {
                    LeftControllerScriptAlias.GetComponent<VRTK_InteractGrab>().enabled = true;
                    RightControllerScriptAlias.GetComponent<VRTK_InteractGrab>().enabled = true;
                }
            }
        }
            Debug.Log("turn_player:" + turn_player);
        if (PhotonNetwork.IsMasterClient) //player1
        {
            b1.transform.position = cam.transform.position - cam.transform.forward * 0.3f - new Vector3(0,1.8f,0);
            diff = cam.transform.localEulerAngles.y - camRotationY;
            camRotationY = cam.transform.localEulerAngles.y;
        }
        else
        {
            b2.transform.position = cam.transform.position - cam.transform.forward * 0.3f - new Vector3(0, 1.8f, 0);
            diff = cam.transform.localEulerAngles.y - camRotationY;
            camRotationY = cam.transform.localEulerAngles.y;
        }
        
        if (!isFinish)
        {
            //cleanSurface();
            chessNum = 0;
            //check every position status
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        var currPosition = chessboard.transform.GetChild(i).transform.GetChild(j).transform.GetChild(k);
                        if (currPosition.GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject() != null)
                        {
                            chessNum++;
                            update_chessboard(currPosition.GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject(), i, j, k);
          
                            //Debug.Log("x+: "+ chessboard_surface[0,i,k].ToString());
                            // Debug.Log("x-: " + chessboard_surface[1, i, k].ToString());
                            //Debug.Log("y+: " + chessboard_surface[2, j, k].ToString());
                            //Debug.Log("y-: " + chessboard_surface[3, j, k].ToString());
                            //Debug.Log("z+: " + chessboard_surface[4, i, j].ToString());
                            //Debug.Log("z-: " + chessboard_surface[5, i, j].ToString());
                        }

                    }
                }
            }
            
            if (PhotonNetwork.IsMasterClient)
            {
                if (chessNum > lastchessNum || timer_f < 0.0f)
                {
                    turn_player++;
                    turn_player %= 2;
                    timer_f = 31.0f;
                }
                lastchessNum = chessNum;
                if (turn_player == 0)
                {
                    isMyTurn = true;
                }
                else
                {
                    isMyTurn = false;
                }
            }
            else
            {
                if (turn_player == 0)
                {
                    isMyTurn = false;
                }
                else
                {
                    isMyTurn = true;
                }
            }
            
            if (chessNum > 5 && chessNum <= 10 && !BGM2.isPlaying)
            {
                
                StartCoroutine(FadeAudioSource.StartFade(BGM1, 3, 0));
                BGM2.PlayDelayed(3);
                //BGM1.Pause();
            }
            else if (chessNum > 10 && chessNum <= 15 && !BGM3.isPlaying)
            {
                
                StartCoroutine(FadeAudioSource.StartFade(BGM2, 3, 0));
                BGM3.PlayDelayed(3);
                //BGM2.Pause();
            }
            else if (chessNum > 15 && chessNum <= 20 && !BGM4.isPlaying)
            {
               
                StartCoroutine(FadeAudioSource.StartFade(BGM3, 3, 0));
                BGM4.PlayDelayed(3);
                //BGM3.Pause();
            }
            else if (chessNum > 20 && !BGM5.isPlaying)
            {
                StartCoroutine(FadeAudioSource.StartFade(BGM4, 3, 0));
                BGM5.PlayDelayed(3);
                //BGM4.Pause();
            }
            updateSmallBoard();

            Debug.Log("isWin:" + isWin);
            if (isWin && !first)
            {
                
                first = true;
               
                cleanSurface();
                cleanBoard();
                updateSmallBoard();
               
                if (win_player == 0)
                {
                    //WinText.GetComponent<TextMesh>().text = "Player 1 Win!";
                    if (PhotonNetwork.IsMasterClient)
                    {
                        showSmile(0);
                        showCry(0);
                    }
                    if (playerId == 0)
                    {
                        winAudio.Play();
                    }
                    else
                    {
                        loseAudio.Play();
                    }
                }


                else if (win_player == 1)
                {
                    
                    if (PhotonNetwork.IsMasterClient)
                    {
                        showSmile(1);
                        showCry(1);
                    }

                    //WinText.GetComponent<TextMesh>().text = "Player 2 Win!";
                    if (playerId == 0)
                    {
                        loseAudio.Play();
                    }
                    else
                    {
                        winAudio.Play();
                    }
                }
            }

        }
        changeChessState();
        // set timer
        if (PhotonNetwork.IsMasterClient)
        {
            timer_f -= Time.deltaTime;
            timer_i = (int)timer_f;
        }
        if(timer_i <= 10)
        {
            //BGM1.Pause();
            //BGM1.Play();
            TimeText[0].GetComponent<TextMesh>().color = Color.red;
            //TimeText[1].GetComponent<TextMesh>().color = Color.red;
        }
        else
        {
            tiktokAudio.Play();
            TimeText[0].GetComponent<TextMesh>().color = Color.white;
            //TimeText[1].GetComponent<TextMesh>().color = Color.white;
            //tiktokAudio.loop = true;
            //tiktokAudio.Stop();
        }
        
        TimeText[0].GetComponent<TextMesh>().text = timer_i.ToString();
        if (PhotonNetwork.IsMasterClient)
        {
            if(turn_player == 0)
            {
                TimeText[1].GetComponent<TextMesh>().text = "My turn";
            }
            else
            {
                TimeText[1].GetComponent<TextMesh>().text = "Enemy's turn";
            }
        }
        else
        {
            if (turn_player == 0)
            {
                TimeText[1].GetComponent<TextMesh>().text = "Enemy's turn";
            }
            else
            {
                TimeText[1].GetComponent<TextMesh>().text = "My turn";
            }
        }
        
        Debug.Log("TIME: " + timer_i);
    }


public void updateSmallBoard()
    {

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                for (int k = 0; k < 7; k++)
                {
                    if (chessboard_surface[i, j, k] >= 0)
                    {
                        if (chessboard_surface[i, j, k] == 0)
                        {
                            smallboard[0].transform.GetChild(i).GetChild(j).GetChild(k).GetComponent<MeshRenderer>().material = m[0];
                            smallboard[1].transform.GetChild(i).GetChild(j).GetChild(k).GetComponent<MeshRenderer>().material = m[0];
                        }

                        else if (chessboard_surface[i, j, k] == 1)
                        {
                            smallboard[0].transform.GetChild(i).GetChild(j).GetChild(k).GetComponent<MeshRenderer>().material = m[1];
                            smallboard[1].transform.GetChild(i).GetChild(j).GetChild(k).GetComponent<MeshRenderer>().material = m[1];
                        }

                        else if (chessboard_surface[i, j, k] == 2)
                        {
                            smallboard[0].transform.GetChild(i).GetChild(j).GetChild(k).GetComponent<MeshRenderer>().material = m[2];
                            smallboard[1].transform.GetChild(i).GetChild(j).GetChild(k).GetComponent<MeshRenderer>().material = m[2];
                        }

                    }
                    else
                    {
                        smallboard[0].transform.GetChild(i).GetChild(j).GetChild(k).GetComponent<MeshRenderer>().material = m[3];
                        smallboard[1].transform.GetChild(i).GetChild(j).GetChild(k).GetComponent<MeshRenderer>().material = m[3];
                    }

                }
            }
        }
    }

    public void update_chessboard(GameObject c, int i, int j, int k)
    {

        //update x+
        if (chessboard_depth[0, i, k] < j)
        {
            chessboard_surface[0, i, k] = ColorToInt(c.GetComponent<GetChessColor>().GetColorXplus());
            //Debug.Log(chessboard_surface[0, i, k]);
            chessboard_depth[0, i, k] = j;

            PrintWinText(IsWin(0, i, k), chessboard_surface[0, i, k]);
        }

        //update x-
        if ((chessboard_depth[1, i, k] > j) || (chessboard_depth[1, i, k] == -1))
        {
            chessboard_surface[1, i, k] = ColorToInt(c.GetComponent<GetChessColor>().GetColorXminus());
            chessboard_depth[1, i, k] = j;
            PrintWinText(IsWin(1, i, k), chessboard_surface[1, i, k]);


        }
        //update y+
        if (chessboard_depth[2, j, k] < i)
        {
            chessboard_surface[2, j, k] = ColorToInt(c.GetComponent<GetChessColor>().GetColorYplus());
            chessboard_depth[2, j, k] = i;
            IsWin(2, j, k);
            PrintWinText(IsWin(2, j, k), chessboard_surface[2, j, k]);
        }

        //update y-
        if ((chessboard_depth[3, j, k] > i) || (chessboard_depth[3, j, k] == -1))
        {
            chessboard_surface[3, j, k] = ColorToInt(c.GetComponent<GetChessColor>().GetColorYminus());
            chessboard_depth[3, j, k] = i;
            PrintWinText(IsWin(3, j, k), chessboard_surface[3, j, k]);
        }
        //update z+
        if (chessboard_depth[4, i, j] < k)
        {
            chessboard_surface[4, i, j] = ColorToInt(c.GetComponent<GetChessColor>().GetColorZplus());
            chessboard_depth[4, i, j] = k;
            PrintWinText(IsWin(4, i, j), chessboard_surface[4, i, j]);
        }
        //update z-
        if ((chessboard_depth[5, i, j] > k) || (chessboard_depth[5, i, j] == -1))
        {
            chessboard_surface[5, i, j] = ColorToInt(c.GetComponent<GetChessColor>().GetColorZminus());
            chessboard_depth[5, i, j] = k;
            PrintWinText(IsWin(5, i, j), chessboard_surface[5, i, j]);
        }
    }

    public void PrintWinText(bool win, int color)
    {
        if (win)
        {
            if (PhotonNetwork.IsMasterClient) //player1
            {
                isWin = true;
                
                isFinish = true;
                win_player = color;
            }
         
            //else if (color == 2)
            //    WinText.GetComponent<TextMesh>().text = "Draw";
        }
        else
        {
            //WinText.GetComponent<TextMesh>().text = "No one Win!";
        }
    }

    public void cleanSurface()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                for (int k = 0; k < 7; k++)
                {
                    chessboard_surface[i, j, k] = -1;
                }
            }
        }
    }

    public void cleanBoard()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                for (int k = 0; k < 7; k++)
                {
                    var currPosition = chessboard.transform.GetChild(i).transform.GetChild(j).transform.GetChild(k);
                    if (currPosition.GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject() != null)
                    {
                        var obj = currPosition.GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject();
                        currPosition.GetComponent<VRTK_SnapDropZone>().ForceUnsnap();
                        obj.transform.position = new Vector3(0.999f, -999f, 0.999f);
                        //currPosition.GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject().transform.position = new Vector3(0.999f, -999f, 0.999f);
                        //currPosition.GetComponent<VRTK_SnapDropZone>().ForceUnsnap();
                        //currPosition.GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject().GetComponent<MeshRenderer>().enabled = false;
                        //for (int ii=0; ii<6; ii++)
                        //currPosition.GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject().transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;

                        //Debug.Log("x+: "+ chessboard_surface[0,i,k].ToString());
                        // Debug.Log("x-: " + chessboard_surface[1, i, k].ToString());
                        //Debug.Log("y+: " + chessboard_surface[2, j, k].ToString());
                        //Debug.Log("y-: " + chessboard_surface[3, j, k].ToString());
                        //Debug.Log("z+: " + chessboard_surface[4, i, j].ToString());
                        //Debug.Log("z-: " + chessboard_surface[5, i, j].ToString());
                    }

                }
            }
        }

    }

    public void showCry(int winner)
    {
        int idx;
        if (winner == 0)
        {
            idx = 0;
        }
        else
        {
            idx = 6;
        }
        for (int i = 0; i < 9; i++)
        {
            loseChess[i].SetActive(true);
        }
        chessboard.transform.GetChild(1).transform.GetChild(2).transform.GetChild(idx).GetComponent<VRTK_SnapDropZone>().ForceSnap(loseChess[0]);
        //update_chessboard(chessboard.transform.GetChild(1).transform.GetChild(2).transform.GetChild(6).GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject(), 1,2,6);
        chessboard.transform.GetChild(1).transform.GetChild(3).transform.GetChild(idx).GetComponent<VRTK_SnapDropZone>().ForceSnap(loseChess[1]);
        //update_chessboard(chessboard.transform.GetChild(1).transform.GetChild(2).transform.GetChild(6).GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject(), 1, 3, 6);
        chessboard.transform.GetChild(1).transform.GetChild(4).transform.GetChild(idx).GetComponent<VRTK_SnapDropZone>().ForceSnap(loseChess[2]);
        //update_chessboard(chessboard.transform.GetChild(1).transform.GetChild(2).transform.GetChild(6).GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject(), 1, 4, 6);
        chessboard.transform.GetChild(0).transform.GetChild(1).transform.GetChild(idx).GetComponent<VRTK_SnapDropZone>().ForceSnap(loseChess[3]);
        //update_chessboard(chessboard.transform.GetChild(1).transform.GetChild(2).transform.GetChild(6).GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject(), 2, 1, 6);
        chessboard.transform.GetChild(0).transform.GetChild(5).transform.GetChild(idx).GetComponent<VRTK_SnapDropZone>().ForceSnap(loseChess[4]);
        //update_chessboard(chessboard.transform.GetChild(1).transform.GetChild(2).transform.GetChild(6).GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject(), 2, 5, 6);
        chessboard.transform.GetChild(4).transform.GetChild(2).transform.GetChild(idx).GetComponent<VRTK_SnapDropZone>().ForceSnap(loseChess[5]);
        //update_chessboard(chessboard.transform.GetChild(1).transform.GetChild(2).transform.GetChild(6).GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject(), 4, 2, 6);
        chessboard.transform.GetChild(4).transform.GetChild(4).transform.GetChild(idx).GetComponent<VRTK_SnapDropZone>().ForceSnap(loseChess[6]);
        //update_chessboard(chessboard.transform.GetChild(1).transform.GetChild(2).transform.GetChild(6).GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject(), 4, 4, 6);
        chessboard.transform.GetChild(5).transform.GetChild(2).transform.GetChild(idx).GetComponent<VRTK_SnapDropZone>().ForceSnap(loseChess[7]);
        //update_chessboard(chessboard.transform.GetChild(1).transform.GetChild(2).transform.GetChild(6).GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject(), 5, 2, 6);
        chessboard.transform.GetChild(5).transform.GetChild(4).transform.GetChild(idx).GetComponent<VRTK_SnapDropZone>().ForceSnap(loseChess[8]);
    }

    public void showSmile(int winner)
    {
        int idx;
        if (winner == 0)
        {
            idx = 6;
        }
        else
        {
            idx = 0;
        }
        for (int i = 0; i < 9; i++)
        {
            winChess[i].SetActive(true);
        }
        chessboard.transform.GetChild(1).transform.GetChild(2).transform.GetChild(idx).GetComponent<VRTK_SnapDropZone>().ForceSnap(winChess[0]);
        //update_chessboard(chessboard.transform.GetChild(1).transform.GetChild(2).transform.GetChild(6).GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject(), 1,2,6);
        chessboard.transform.GetChild(1).transform.GetChild(3).transform.GetChild(idx).GetComponent<VRTK_SnapDropZone>().ForceSnap(winChess[1]);
        //update_chessboard(chessboard.transform.GetChild(1).transform.GetChild(2).transform.GetChild(6).GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject(), 1, 3, 6);
        chessboard.transform.GetChild(1).transform.GetChild(4).transform.GetChild(idx).GetComponent<VRTK_SnapDropZone>().ForceSnap(winChess[2]);
        //update_chessboard(chessboard.transform.GetChild(1).transform.GetChild(2).transform.GetChild(6).GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject(), 1, 4, 6);
        chessboard.transform.GetChild(2).transform.GetChild(1).transform.GetChild(idx).GetComponent<VRTK_SnapDropZone>().ForceSnap(winChess[3]);
        //update_chessboard(chessboard.transform.GetChild(1).transform.GetChild(2).transform.GetChild(6).GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject(), 2, 1, 6);
        chessboard.transform.GetChild(2).transform.GetChild(5).transform.GetChild(idx).GetComponent<VRTK_SnapDropZone>().ForceSnap(winChess[4]);
        //update_chessboard(chessboard.transform.GetChild(1).transform.GetChild(2).transform.GetChild(6).GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject(), 2, 5, 6);
        chessboard.transform.GetChild(4).transform.GetChild(2).transform.GetChild(idx).GetComponent<VRTK_SnapDropZone>().ForceSnap(winChess[5]);
        //update_chessboard(chessboard.transform.GetChild(1).transform.GetChild(2).transform.GetChild(6).GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject(), 4, 2, 6);
        chessboard.transform.GetChild(4).transform.GetChild(4).transform.GetChild(idx).GetComponent<VRTK_SnapDropZone>().ForceSnap(winChess[6]);
        //update_chessboard(chessboard.transform.GetChild(1).transform.GetChild(2).transform.GetChild(6).GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject(), 4, 4, 6);
        chessboard.transform.GetChild(5).transform.GetChild(2).transform.GetChild(idx).GetComponent<VRTK_SnapDropZone>().ForceSnap(winChess[7]);
        //update_chessboard(chessboard.transform.GetChild(1).transform.GetChild(2).transform.GetChild(6).GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject(), 5, 2, 6);
        chessboard.transform.GetChild(5).transform.GetChild(4).transform.GetChild(idx).GetComponent<VRTK_SnapDropZone>().ForceSnap(winChess[8]);
        //update_chessboard(chessboard.transform.GetChild(1).transform.GetChild(2).transform.GetChild(6).GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject(), 5, 4, 6);
    }

    public int ColorToInt(string c)
    {
        if (c == "Yellow") return 0;
        if (c == "Blue") return 1;
        if (c == "Purple") return 2;

        return -1;
    }

    public bool IsWin(int idx, int x, int y)
    {
        int currColor = chessboard_surface[idx, x, y];
        int cnt = 1;

        for (int i = 1; i < 4; i++)
        {
            if (x + i < 7)
            {
                if (chessboard_surface[idx, x + i, y] == currColor)
                {
                    cnt++;

                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

        for (int i = 1; i < 4; i++)
        {
            if (x - i >= 0)
            {
                if (chessboard_surface[idx, x - i, y] == currColor)
                {
                    cnt++;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        if (cnt >= 4) return true;
        cnt = 1;

        for (int i = 1; i < 4; i++)
        {
            if (y + i < 7)
            {
                if (chessboard_surface[idx, x, y + i] == currColor)
                {
                    cnt++;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }

        }

        for (int i = 1; i < 4; i++)
        {
            if (y - i >= 0)
            {
                if (chessboard_surface[idx, x, y - i] == currColor)
                {
                    cnt++;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }

        }

        if (cnt >= 4) return true;
        cnt = 1;

        for (int i = 1; i < 4; i++)
        {
            if (x + i < 7 && y + i < 7)
            {
                if (chessboard_surface[idx, x + i, y + i] == currColor)
                {
                    cnt++;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

        for (int i = 1; i < 4; i++)
        {
            if (x - i >= 0 && y - i >= 0)
            {
                if (chessboard_surface[idx, x - i, y - i] == currColor)
                {
                    cnt++;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }

        }

        if (cnt >= 4) return true;
        cnt = 1;

        for (int i = 1; i < 4; i++)
        {
            if (x + i < 7 && y - i >= 0)
            {
                if (chessboard_surface[idx, x + i, y - i] == currColor)
                {
                    cnt++;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

        for (int i = 1; i < 4; i++)
        {
            if (x - i >= 0 && y + i < 7)
            {
                if (chessboard_surface[idx, x - i, y + i] == currColor)
                {
                    cnt++;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

        if (cnt >= 4) return true;
        cnt = 1;

        return false;
    }
    public static class FadeAudioSource
    {

        public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
        {
            float currentTime = 0;
            float start = audioSource.volume;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }
            audioSource.Stop();
        }
    }

}
