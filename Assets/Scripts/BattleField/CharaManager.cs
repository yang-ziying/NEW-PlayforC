using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaManager : MonoBehaviour
{
    //何をしてるかというとUnitMoveから送られてくる位置データをQueueに保存してそれでキャラをゆっくりと動かす
    private static Queue<float> unitmovetox = new Queue<float>();
    private static Queue<float> unitmovetoy = new Queue<float>();
    private GameObject chara;
    private bool nextmove= true;
    private float unitnextmovex;
    private float unitnextmovey;

    private Vector3 unitnextposition= new Vector3(0,0,0);
    private Vector3 unitnowposition= new Vector3(-1,1,0);
    
    private float remdistance;
    // Update is called once per frame  
    
    void Start() 
    {
        chara = GameObject.Find("Chara");
    }
    void Update()
    {
        
        
        

        if (unitmovetox.Count != 0 && nextmove == true) //Queueを排出して、新しい位置を提示
        {
            //Debug.Log("キャラ現在の位置"+chara.transform.position);
            unitnextmovex =unitmovetox.Dequeue();
            unitnextmovey =unitmovetoy.Dequeue();
            //Debug.Log("Queue排出成功");
            unitnextposition=new Vector3 (unitnextmovex,unitnextmovey,0f);
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
        unitmovetox.Enqueue(floatx);
        unitmovetoy.Enqueue(floaty);
    }
}
