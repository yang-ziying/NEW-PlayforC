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
        Debug.Log(unitID+"'s turnmax="+turnmax);

        
    }
    public void MoveTo(int y, int x)
    {
        unit_x= x;
        unit_y= y;
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
    public int Unit_x{get=>unit_x;}
    public int Unit_y{get=>unit_y;}
    public int MOV {get=>mov;}
    
    public void Damage(int damage) 
    {
       if(def<damage) hp-=(damage-def);
       if(hp<=0) hp=0;
    }
   
}


public class BattleSceneManager : MonoBehaviour
{
   
    private Unit[] units = new Unit[6];
     //unitposi管理をSceneManegerで行う　画像はCharaManager
   

    private GameObject unitmoveobj;
    private UnitMove unitmove;
    private UnitInfo allunitinfo;
    private CharaManager charamanager;


    int InTurnUnitID;


    // Start is called before the first frame update
    void Start()
    {
        unitmoveobj= GameObject.Find("UnitMove");      //这里这里******************************
        unitmove=unitmoveobj.GetComponent<UnitMove>();

        charamanager = GameObject.Find("CharaManager").GetComponent<CharaManager>();

        allunitinfo = UnitInfo.CreateFromSaveData();
        

        for(int i=0;i<6;i++)
        {
            units[i]= new Unit();
            units[i].SetStatus(allunitinfo,i);
            units[i].MoveTo(i,i);
            
        }
         //Array.Sort(units, (a, b) => b.SPD - a.SPD);
         while(true)
         {
            int tick=TurnTick();
            if(tick>=0)
            {
                InTurnUnitID=tick;
                Debug.Log("InTurnUnitID="+InTurnUnitID);
                break;
            }

         }
        
         unitmove.calMoveRange(units[InTurnUnitID].Unit_y,units[InTurnUnitID].Unit_x,units[InTurnUnitID].MOV);
        

        
        
         
    }
    int TurnTick()
    {   int id=-1;
        for(int i=0;i<6;i++)
            {
                if(units[i].IsMyTurn()) id=units[i].UnitID; 
            } 
        return id;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
