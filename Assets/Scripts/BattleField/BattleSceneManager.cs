using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit 
 {
    
    protected int unitID;
    protected int maxHP;
    protected int hp;
    protected int atk;
    protected int def;
    protected int spd;
    protected int mov;
    protected int job;
    protected int unit_x;
    protected int unit_y;
    protected float turnmax;
    protected float turn;
    protected string team;
    

    public void SetStatus(UnitInfo unitinfo,int idx)
    {
        unitID=unitinfo.id[idx];
        maxHP=StatusCal.CalmaxHP(unitinfo.u_vit[idx],unitinfo.u_str[idx]);
        hp=maxHP;
        atk=StatusCal.CalAtk(unitinfo.u_str[idx],unitinfo.u_dex[idx]);
        def=StatusCal.CalDef(unitinfo.u_vit[idx],unitinfo.u_str[idx]);
        spd=StatusCal.CalSpd(unitinfo.u_agi[idx],unitinfo.u_dex[idx]);
        mov=unitinfo.u_mov[idx];
        job=unitinfo.u_job[idx];
        team = "player";
        turnmax= 50f/((float)Math.Sqrt((double)spd));
        Debug.Log(unitID+"'s turnmax="+turnmax);

        
    }

    public void SetMonsterStatus(List<MstMonstersEntity> unitinfo,int idx)
    {
        unitID=unitinfo[idx].id;
        maxHP=StatusCal.CalmaxHP(unitinfo[idx].u_vit,unitinfo[idx].u_str);
        hp=maxHP;
        atk=StatusCal.CalAtk(unitinfo[idx].u_str,unitinfo[idx].u_dex);
        def=StatusCal.CalDef(unitinfo[idx].u_vit,unitinfo[idx].u_str);
        spd=StatusCal.CalSpd(unitinfo[idx].u_agi,unitinfo[idx].u_dex);
        mov=unitinfo[idx].u_mov;

        job=idx;

         
        team = "enemy";
        turnmax= 50f/((float)Math.Sqrt((double)spd));
        Debug.Log(unitID+"'s turnmax="+turnmax);

        
    }



    public void MoveTo(int y, int x)
    {
        unit_x= x;
        unit_y= y;
    }
    public Vector3 GetPosition()
    {
        Vector3 position=new Vector3(((float)unit_x)/2.5f,((float)unit_y)/2.5f,unit_y); //*32zを故意に指定している。重複問題に関わる。
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
    public string Team{get=>team;}
    
    public void Damage(int damage) 
    {
       if(def<damage) hp-=(damage-def);
       if(hp<=0) hp=0;
    }

    
   
}

//-------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------

public class BattleSceneManager : MonoBehaviour
{
   [SerializeField]
    MstMonsters mstmonsters; //モンスター管理データ
    
    public static int partynumber = 6; //臨時　パーティーのキャラ数
    public static int monsternumber = 3;//臨時　モンスターのキャラ数 今は図鑑からＩＤを直接取り出してる。どこかでモンスター複製しとかないと話が進まない（やるべき）
    public static int allunitnumber;　

    private int[,] mapunitID = new int[12,24]; //map上のunit位置情報 中にある数字はunits[]のindex number　前partynumberの量だけ味方、（数字が大きめの）後ろが敵　現在Mapunitは毎ターンの開始時に1度だけリセットされ更新されている
    private string[,] mapunitTeam = new string[12,24];

    private Unit[] units = new Unit[partynumber+monsternumber];
     //unitposi管理をSceneManegerで行う　画像はCharaManager

   
    private UnitMove unitmove;
    private UnitAttack unitattack;
    private UnitInfo allunitinfo;
    private CharaManager charamanager;
    


    public static int InTurnUnitIdx; //現在のターンのUnitのINDEXNUMBER
    
    public bool IsReadyToNextTurn= true;
    public bool IsReadyToAttack= false;
    
    private Queue<Vector3> unitmoveRoot = new Queue<Vector3>();

    


   
    void Awake() //Startだと他のManagerにUnits[]渡すの間に合わない。Unitの定義のみAwakeが必要 ☆これでバグが起きそうだからいつか全呼び出しをここで行うようにしないといけないかも
    {
        
        
         
     
         
    }
    private void Start() 
    {  
        unitmove=GameObject.Find("UnitMove").GetComponent<UnitMove>();
        unitattack=GameObject.Find("UnitAttack").GetComponent<UnitAttack>();
        charamanager = GameObject.Find("CharaManager").GetComponent<CharaManager>();

        allunitinfo = UnitInfo.CreateFromSaveData();
        allunitnumber = partynumber+monsternumber;

        
        

        for(int i=0;i<partynumber;i++)
        {
            units[i]= new Unit();
            units[i].SetStatus(allunitinfo,i);
            units[i].MoveTo(i*i/5+1,i*2+1);
            
        }
        for(int i=0; i<monsternumber;i++)
        {
            units[i+partynumber]= new Monster();
            units[i+partynumber].SetMonsterStatus(mstmonsters.Entities,i);
            units[i+partynumber].MoveTo(i+4,i+6);
        }
         //Array.Sort(units, (a, b) => b.SPD - a.SPD);

        
        unitmove.PrepareStart();
        charamanager.PrepareStart();
        unitattack.PrepareStart();
       
    }

     // Update is called once per frame
    private void Update()
    {
        if(IsReadyToNextTurn)
        {
             IsReadyToNextTurn=false;
             TurnTick();
             UpdateMapunit();
             unitmove.calMoveRange(units[InTurnUnitIdx].Unit_y,units[InTurnUnitIdx].Unit_x,units[InTurnUnitIdx].MOV);
             
        }
       
        if(Input.GetMouseButtonDown(0)) //ここら辺はクリックしたときにクリックした時の座標で
        {
            unitmove.calMoveRootandMove(mapunitID,InTurnUnitIdx);
            unitattack.GetUnitAttacked(mapunitID,units[InTurnUnitIdx].Team);

        }

        if(IsReadyToAttack)
        {
            IsReadyToAttack = false;

            Weapon testweapon = new Weapon();//for test、もともとはUnitに入れるデータ
            Skill testskill = new Skill();//for test、もともとはUnitに入れるデータ
            testskill=testweapon.GetWeaponSkillforTest();//for test、もともとはUnitに入れるデータ

            unitattack.CalAttackRange(units[InTurnUnitIdx].Unit_y,units[InTurnUnitIdx].Unit_x,testskill);
           
        }
    }

    


    private void TurnTick() //誰かのターンになるまでターンを進める。最終的に横にバーでこれを表示
    {   int idx=-1;
        for(int i=0;i<allunitnumber;i++)
            {
                if(units[i].IsMyTurn()) idx=i; 
            } 
        if(idx<0)
        {
            TurnTick();
            return;
        }
         else
         {
             InTurnUnitIdx=idx;
             charamanager.SetinTurnChara(idx);
         }  
    }

    public void UpdateMapunit()
    {
        //Reset MapArray
       for(int x=0; x<24; x++){
            for(int y=0; y<12;y++){
                mapunitID[y,x]=-1;
                mapunitTeam[y,x]="";
            }
        }    
        //Change MapArray
        for(int i=0;i<allunitnumber;i++)
            {
               mapunitID[units[i].Unit_y,units[i].Unit_x]=i;
               mapunitTeam[units[i].Unit_y,units[i].Unit_x]=units[i].Team;
            } 
    }
    public bool IsOtherUnitHere(int y, int x)
    {
        bool HasSomeoneHere = false;
        
        if(mapunitID[y,x]>=0 && mapunitID[y,x]!=InTurnUnitIdx) HasSomeoneHere = true;
       
        return HasSomeoneHere;
    }

   

    public Unit[] getUnit()
    {
        return units;
    }

    public void SetPosiStack(Stack<int> stacky,Stack<int> stackx) //ゲームコードPosiも画像Posiも変えて行く必要があり
    {
        unitmoveRoot.Clear(); //ここで消す理由：毎回受け取る前に消す。参照型なので渡した後すぐ消すとCharamanagerの方も一緒に消える
        Vector3 lastPosition = new Vector3(0,0,0);
        if(stacky.Count ==0 ) lastPosition = units[InTurnUnitIdx].GetPosition(); //その場をタップした場合動かないからスタックがない。
        for( int i= stackx.Count ; i>0; i--)  //さっきＳｔａｃｋした全部をＰｏｐしていく
        {
            
            int x= stackx.Pop();
            int y= stacky.Pop();
             if (i==1) //ラストポジションは覚えておく
            {
                lastPosition = new Vector3(((float)x)/2.5f,((float)y)/2.5f,y);//*1f
                Debug.Log(lastPosition);
            }
            units[InTurnUnitIdx].MoveTo(y,x);
            float floatx= (float)x;
            float floaty= (float)y;

            Vector3 posi = new Vector3(floatx/2.5f,floaty/2.5f,floaty);//*1f
            unitmoveRoot.Enqueue(posi);

           
            
        }
       charamanager.SetMoveRoot(unitmoveRoot,lastPosition);
       

    }
    

    
    
    
}
