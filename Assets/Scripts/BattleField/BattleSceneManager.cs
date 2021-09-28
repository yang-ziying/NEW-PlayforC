using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit 
 {
    
    private int unitID;
    private int maxHP;
    private int hp;
    private int atk;
    private int def;
    private int spd;
    private int mov;
    private int job;
    private int unit_x;
    private int unit_y;
    private float turnmax;
    private float turn;
    

    public void SetStatus(UnitInfo unitinfo,int idx)
    {
        unitID=unitinfo.id[idx];
        maxHP=unitinfo.u_vit[idx]*10+unitinfo.u_str[idx]*10;
        hp=maxHP;
        atk=unitinfo.u_str[idx]*5+unitinfo.u_dex[idx]*3;
        def=unitinfo.u_vit[idx]*5+unitinfo.u_str[idx]*3;
        spd=unitinfo.u_agi[idx]*3+unitinfo.u_dex[idx]*1;
        mov=unitinfo.u_mov[idx];
        job=unitinfo.u_job[idx];
        turnmax= 100f/((float)Math.Sqrt((double)spd));
        //Debug.Log(unitID+"'s turnmax="+turnmax);

        
    }
    public void MoveTo(int y, int x)
    {
        unit_x= x;
        unit_y= y;
    }
    public Vector3 GetPosition()
    {
        Vector3 position=new Vector3(unit_x,unit_y,unit_y); //zを故意に指定している。重複問題に関わる。
        return position;
    }


    public bool IsMyTurn()
    {
        turn+=0.01f;
        if(turn>=turnmax)
        {
            turn = 0f;
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public int UnitID{get=>unitID;}
    public int SPD {get=>spd;}
    public int MOV {get=>mov;}
    public int JOB {get=>job;}
    public int Unit_x{get=>unit_x;}
    public int Unit_y{get=>unit_y;}
    
    public void Damage(int damage) 
    {
       if(def<damage) hp-=(damage-def);
       if(hp<=0) hp=0;
    }
   
}


public class BattleSceneManager : MonoBehaviour
{
    public static int partynumber = 6; //臨時　パーティーのキャラ数
    private Unit[] units = new Unit[partynumber];
     //unitposi管理をSceneManegerで行う　画像はCharaManager

    
   
   

    private GameObject unitmoveobj;
    private UnitMove unitmove;
    private UnitInfo allunitinfo;
    private CharaManager charamanager;


    public static int InTurnUnitIdx; //現在のターンのUnitのINDEXNUMBER

    


   
    void Awake() //Startだと他のManagerにUnits[]渡すの間に合わない。Unitの定義のみAwakeが必要
    {
        unitmoveobj= GameObject.Find("UnitMove");      
        unitmove=unitmoveobj.GetComponent<UnitMove>();

        charamanager = GameObject.Find("CharaManager").GetComponent<CharaManager>();

        allunitinfo = UnitInfo.CreateFromSaveData();
        

        for(int i=0;i<partynumber;i++)
        {
            units[i]= new Unit();
            units[i].SetStatus(allunitinfo,i);
            units[i].MoveTo(i*i/5+1,i*2+1);
            
        }
         //Array.Sort(units, (a, b) => b.SPD - a.SPD);
         
     
         
    }
    private void Start() 
    {
        while(true)
         {
            int tick=TurnTick();
            if(tick>=0)
            {
                InTurnUnitIdx=tick;
                //Debug.Log("InTurnUnitIdx="+InTurnUnitIdx);
                break;
            }

         }
        
         unitmove.calMoveRange(units[InTurnUnitIdx].Unit_y,units[InTurnUnitIdx].Unit_x,units[InTurnUnitIdx].MOV);
    }
     // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) //ここら辺はクリックしたときにクリックした時の座標で
        {
            unitmove.calMoveRootandMove();
        }
    }


    private int TurnTick()
    {   int idx=-1;
        for(int i=0;i<partynumber;i++)
            {
                if(units[i].IsMyTurn()) idx=i; 
            } 
        return idx;
    }

   

    public Unit[] getUnit()
    {
        return units;
    }

    public void changeUnitposi(int y,int x) //ゲームコードPosiも画像Posiも変えて行く必要があり
    {
        units[InTurnUnitIdx].MoveTo(y,x);
        CharaManager.addMovePos(y,x);
    }

    
    
    
}
