using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //現在ーー全体的にMoveの時に赤くするプログラム
    //StaticのUnitmoveのMapmove（マップの移動残りマス表示、ないときは-値）を取り出してきている
    //ついでに赤いときにマウスにクリックされたらその座標を返す
    int[,] array= new int[12,24];
    int thisx;
    int thisy;
    GameObject movetile;
    public static int clickx;
    public static int clicky;
    // Start is called before the first frame update
    void Start()
    {
        
        array = UnitMove.mapmove;
        thisx = (int)transform.position.x;
        thisy = (int)transform.position.y;
        movetile = transform.Find("hantoumei").gameObject;
        //Debug.Log(thisx +","+thisy);
        //Debug.Log(array[thisy,thisx]);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(array[thisy,thisx] <0 ) movetile.SetActive (false);
        else movetile.SetActive (true);

        
    }
     void OnMouseDown()
     {
        Debug.Log(array[thisy,thisx]);
        clickx= thisx;
        clicky= thisy;
    }   
}

