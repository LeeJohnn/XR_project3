using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class checkerboard : MonoBehaviour
{
    private GameObject [,,] board;
    private int boardSize = 7;
    public bool otherBoardActive = false;
    // Start is called before the first frame update
    void Start()
    {
        board = new GameObject[boardSize, boardSize, boardSize];
        // for(int i=0; i<boardSize; i++){
        //     board[0, i] = this.transform.GetChild(i).gameObject;
        // }
        // for(int i=0; i<boardSize; i++) {
        //     var child1 = this.transform.GetChild(i).gameObject;
        //     for(int j=0; j<boardSize; j++) {
        //         var child2 = child1.transform.GetChild(j).gameObject;
        //         for(int k=0; k<boardSize; k++) {
        //             board[i, j, k] = child2.transform.GetChild(k).gameObject;
        //             //board[i, j, k].transform.GetChild(0).gameObject.SetActive(false);
        //         }
        //     }
        // }
        // for(int i=0; i<boardSize; i++){
        //     board[0, i].transform.GetChild(0).gameObject.SetActive(false);
        // }
    }
    public void setotherBoardActiveTrue()
    {
        otherBoardActive = true;
    }
    public void setotherBoardActiveFalse()
    {
        otherBoardActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        // for(int i=0; i<boardSize; i++){
        //     if(board[0, i].GetComponent<boardStatus>().isEnter){
        //         bool active = true;
        //         for(int j=0; j<7; j++){
        //             if(board[0, j].GetComponent<boardStatus>().isEnter && i!=j)
        //                 active = false;
        //         }
        //         if(active)
        //             board[0, i].transform.GetChild(0).gameObject.SetActive(true);
        //     }
        //     else
        //         board[0, i].transform.GetChild(0).gameObject.SetActive(false);
        // }
        // for(int i=0; i<1; i++) {
        //     for(int j=0; j<boardSize; j++) {
        //         for(int k=0; k<boardSize; k++) {
        //             if(board[i, j, k].GetComponent<boardStatus>().isEnter) {
        //                 bool active = true;
        //                 for(int l=0; l<1; l++){
        //                     if(board[l, j, k].GetComponent<boardStatus>().isEnter && l!=i)
        //                         active = false;
        //                 }
        //                 for(int l=0; l<boardSize; l++){
        //                     if(board[i, l, k].GetComponent<boardStatus>().isEnter && l!=j)
        //                         active = false;
        //                 }
        //                 for(int l=0; l<boardSize; l++){
        //                     if(board[i, j, l].GetComponent<boardStatus>().isEnter && l!=k)
        //                         active = false;
        //                 }
        //                 if(active)
        //                     board[i,j,k].transform.GetChild(0).gameObject.SetActive(true);
        //             }
        //             else
        //                 board[i,j,k].transform.GetChild(0).gameObject.SetActive(false);
        //         }
        //     }
        // }
    }
}
