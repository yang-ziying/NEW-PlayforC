using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

        
    }
    public int SPD {get=>spd;}
    public int UnitID{get=>unitID;}
    public void Damage(int damage) 
    {
       if(def<damage) hp-=(damage-def);
       if(hp<=0) hp=0;
    }
   
}


public class BattleSceneManager : MonoBehaviour
{
   
    Unit[] units = new Unit[6];

    UnitInfo allunitinfo;
    


    // Start is called before the first frame update
    void Start()
    {
        allunitinfo = UnitInfo.CreateFromSaveData();
        for(int i=0;i<6;i++)
        {
            units[i]= new Unit();
            units[i].SetStatus(allunitinfo,i);
        }
        IEnumerable<Unit> unitTurnOrder = units.OrderBy(unit=>-unit.SPD);
        foreach (Unit unit in unitTurnOrder)//独自の機能を追加した自作クラスをforeachで使用したい場合は、IEnumerableが必須となります。
        {
             Debug.Log("spd:"+ unit.SPD + " id:"+unit.UnitID);
        }
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
