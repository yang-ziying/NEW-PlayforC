using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //現在ーー全体的にMoveの時に赤くするプログラム
    //StaticのUnitmoveのMapmove（マップの移動残りマス表示、ないときは-値）を取り出してきている
    //ついでに赤いときにマウスにクリックされたらその座標を返す
    int[,] mapmovearray= new int[12,24];
    public int thisx;
    public int thisy;
    GameObject movetile;
    GameObject attacktile;
    GameObject roundtile;
    public static int clickx;
    public static int clicky;
    private BattleSceneManager scenemanager;

    // Start is called before the first frame update
    void Start()
    {
        
        mapmovearray = UnitMove.GetMapmve;　//すごく・・・参照型です・・・
       
        movetile = transform.Find("MoveArea").gameObject;
        attacktile = transform.Find("AttackArea").gameObject;
        roundtile = transform.Find("RoundArea").gameObject;
        scenemanager=GameObject.Find("SceneManager").GetComponent<BattleSceneManager>();
        Debug.Log("testtest"+thisx +","+thisy);
        //Debug.Log(mapmovearray[thisy,thisx]);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(mapmovearray[thisy,thisx] <0 || scenemanager.IsOtherUnitHere(thisy,thisx) ) movetile.SetActive (false); //mapmoveが-の時とmapunitにInturnキャラ以外の人がいる時そこは行けない
        else movetile.SetActive (true);


        
        if(UnitAttack.mapattack[thisy,thisx]<0) attacktile.SetActive(false);
        else attacktile.SetActive(true);

        if(UnitAttack.mapattackRound[thisy,thisx]<0) roundtile.SetActive(false);
        else roundtile.SetActive(true);

        
    }
     void OnMouseDown()
     {
        Debug.Log(mapmovearray[thisy,thisx]);
        clickx= thisx;
        clicky= thisy;
    }   
    private void OnMouseEnter() {
        if(UnitAttack.mapattack[thisy,thisx]>=0)
        {
            
            UnitAttack.ChangeAttackRound(thisy,thisx);
        }
        
        
    }
}

