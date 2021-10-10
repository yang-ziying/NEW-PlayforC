using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaManager : MonoBehaviour
{
    [SerializeField]
    GameObject charaPrefab;
     [SerializeField]
    Sprite[] images= new Sprite[6]; //臨時。将来はキャラ召喚し装備で変えるやつをどこかにつくる
    [SerializeField]
    Sprite[] MBimages= new Sprite[3];//臨時

    [SerializeField]
    private BattleSceneManager scenemanager;
    
    //何をしてるかというとUnitMoveから送られてくる位置データをQueueに保存してそれでキャラをゆっくりと動かす
    private Queue<Vector3> moveRoot = new Queue<Vector3>();
   
    private bool nextmove= true;

    private List<Vector3> unitnextposition= new List<Vector3>(); //これが０００なのでどこかでキャラの現在位置を呼び出しておく必要がある
    private Vector3 charaLastPosition = new Vector3();

    private float remdistance;
    

    private string inTurnCharaname;
    private GameObject inTurnChara;
    private int inTurnID;

    private bool CanCharaMove= false;


    // Update is called once per frame  
    
    void Start() 
    {
        
        
        for(int i=0;i<BattleSceneManager.allunitnumber;i++) //全キャラの生成
        {   var units = scenemanager.getUnit();
            string charaname= "Chara"+i;
            GameObject charara = Instantiate(charaPrefab);   
            
            if(units[i].Team=="player")
            {
               
                charara.GetComponent<SpriteRenderer>().sprite = images[units[i].JOB];
                
            }
            else if (units[i].Team == "enemy")
            {
                charara.GetComponent<SpriteRenderer>().sprite = MBimages[units[i].JOB];
            }
           Vector3 charascale= new Vector3(1,1,1);
            charara.transform.localScale=charascale; 
            charara.transform.position = units[i].GetPosition();
                unitnextposition.Add(units[i].GetPosition());
                charara.name= charaname;

            
        }
       

        
        
    }
    void Update()
    {
        if(CanCharaMove) CharaMove();
        　//キャラの座標を目標位置に向けてゆっくり動かす
        
            
    }
    private void CharaMove()
    {
        if (moveRoot.Count != 0 && nextmove == true) //Queueを排出して、新しい位置を提示
        {
            unitnextposition[inTurnID]= moveRoot.Dequeue();
            nextmove = false;
            
        } 
        inTurnChara.transform.position = Vector3.Lerp(inTurnChara.transform.position,unitnextposition[inTurnID],3f*Time.deltaTime); //新しい位置までゆっくり移動

        remdistance = Vector3.Distance(inTurnChara.transform.position,unitnextposition[inTurnID]); //新しい位置と現在位置の距離を求める
        Vector3 MoveVector=unitnextposition[inTurnID]-inTurnChara.transform.position;
        if (MoveVector.x>0)　inTurnChara.transform.localScale=new Vector3(-1,1,1); //キャラ画像方向転換
        else if(MoveVector.x<0)inTurnChara.transform.localScale=new Vector3(1,1,1);

        if (remdistance<=0.05f)//キャラが目標マスに近づいたら目標マスに置いて次の指示を待つ
        {
            inTurnChara.transform.position = unitnextposition[inTurnID];
            nextmove = true;
        }
        
       if(charaLastPosition==inTurnChara.transform.position)
             {
                 Debug.Log("charalastposi"+charaLastPosition);
                 Debug.Log("charaposi"+inTurnChara.transform.position);
                 CanCharaMove=false;
                 scenemanager.IsReadyToNextTurn=true;
                 nextmove=true; //nextmoveのリセット
             }
        

       
          

    }

    
    public void SetinTurnChara(int InTurnUnitIdx)
    {
        inTurnCharaname="Chara" + InTurnUnitIdx;
        inTurnChara = GameObject.Find(inTurnCharaname);
        MoveCamera.SetCamera(inTurnChara);
        inTurnID= InTurnUnitIdx;
        
    } 
    
    public void SetMoveRoot(Queue<Vector3> root,Vector3 lastPosition)
    {
        moveRoot=root;
        charaLastPosition=lastPosition;  
        CanCharaMove = true;
        
    }
}
