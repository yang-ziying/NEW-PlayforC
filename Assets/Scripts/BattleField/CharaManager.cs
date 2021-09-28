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
    private BattleSceneManager scenemanager;
    
    //何をしてるかというとUnitMoveから送られてくる位置データをQueueに保存してそれでキャラをゆっくりと動かす
    private static Queue<Vector3> unitmoveto = new Queue<Vector3>();
    private GameObject chara;
    private bool nextmove= true;
    private Vector3 unitnextmove;

    private Vector3 unitnextposition= new Vector3(0,0,0);
    private Vector3 unitnowposition= new Vector3(-1,1,0);
    
    private float remdistance;
    // Update is called once per frame  
    
    void Start() 
    {
        
        
        for(int i=0;i<BattleSceneManager.partynumber;i++) //全キャラの生成
        {   var units = scenemanager.getUnit();
            string charaname= "Chara"+i;
            GameObject charara = Instantiate(charaPrefab);    
            charara.GetComponent<SpriteRenderer>().sprite = images[units[i].JOB];
            charara.transform.position = units[i].GetPosition();
            charara.name= charaname;
        }

        
        
    }
    void Update()
    {
        //臨時
        
        string InTurnCharaname="Chara" + BattleSceneManager.InTurnUnitIdx;
        chara = GameObject.Find(InTurnCharaname);

        if (unitmoveto.Count != 0 && nextmove == true) //Queueを排出して、新しい位置を提示
        {
            //Debug.Log("キャラ現在の位置"+chara.transform.position);
            unitnextmove =unitmoveto.Dequeue();
            //Debug.Log("Queue排出成功");
            unitnextposition= unitnextmove;
            //Debug.Log(unitnextposition);
            //Debug.Log("距離："+ remdistance);
            nextmove = false;

        }

        remdistance = Vector3.Distance(chara.transform.position,unitnextposition); //新しい位置と現在位置の距離を求める

        if (remdistance<=0.5f)//キャラが目標マスに近づいたら目標マスに置いて次の指示を待つ
        {
            
            chara.transform.position = unitnextposition;
           // unitnowposition = unitnextposition;
            nextmove = true;
        }
        
        chara.transform.position = Vector3.Lerp(chara.transform.position,unitnextposition,3f*Time.deltaTime);　//キャラの座標を目標位置に向けてゆっくり動かす
        
            
    }
      
    
    public static void addMovePos(int y,int x)
    {
        float floatx= (float)x;
        float floaty= (float)y;
        Vector3 posi = new Vector3(x,y,y);
        unitmoveto.Enqueue(posi);
    }
}
