using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitInfo
{
    
    public bool[] isUsing= new bool[100];
    public int[] id = new int[100];
    public string[] u_name= new string[100];
    public int[] u_hp= new int[100];
    public int[] u_mp= new int[100];
    public int[] u_exp = new int [100];
    public int[] u_job = new int [100];
    public int[] u_vit = new int [100];
    public int[] u_str = new int [100];
    public int[] u_dex = new int [100];
    public int[] u_agi = new int [100];
    public int[] u_luk = new int [100];
    public int[] u_int = new int [100];
    public int[] u_wis = new int [100];
    public int[] u_men = new int [100];
    public int[] u_mov = new int [100];
    public int[] u_gender = new int [100];
    //public int[] u_hair = new int [100];
    //public int[] u_skin = new int [100];
    //public int[] equip_head = new int [100];
    //public int[] equip_body = new int [100];
    //public int[] equip_weapon1 = new int [100];
    //public int[] equip_weapon2 = new int [100];
    //public int[] equip_mantle = new int [100]; 
    //public int[] skill_persona = new int [100];
    //  public int[] skill_talent1 = new int[100];
    // public int[] skill_talent2 = new int[100];
    //public int[] skill_job1 = new int [100];
    //public int[] skill_job2 = new int [100];
   
    //public static Unitinfo CreateFromJSON_atPlayerPrefs(string keyname)
    //{
    //    return CreateFromJSON(PlayerPrefs.GetString(keyname));
    //}
    public static UnitInfo CreateFromSaveData()
    {
        //return JsonUtility.FromJson<UnitInfo>(jsonString);
        return JsonUtility.FromJson<UnitInfo>(PlayerPrefs.GetString("AllUnitInfos"));
        //今のところこれしかキー使わないし上書き保存する気満々だからこれ一個で行く
    }
    
    public int[] CreateNewUnit_Randomly_ReturnIDs(int createamount)
    {
        int remnant= createamount;
        int [] newIDs = new int[createamount];
    
        for(int i=0;i<100;i++)
        {
            if(!isUsing[i]){
                
                isUsing[i]= true;
                id[i]=i;
                u_name[i]="Chara"+i.ToString(); //Stringのいちいち変え方がまだよくわからない
                u_hp[i]= 100; //ここにHPMAX計算式設置
                u_mp[i]= 100; //ここにMPMAX計算式設置
                u_exp[i] = 0; 
                u_job[i] = Random.Range(0,6);

                int a =0;
                while (a!=40)
                {
                u_mov[i] = Random.Range(3,6);
                u_vit[i] = Random.Range(1,11); 
                u_str[i] = Random.Range(1,11);
                u_dex[i] = Random.Range(1,11); 
                u_agi[i] = Random.Range(1,11); 
                u_luk[i] = Random.Range(1,11); 
                u_int[i] = Random.Range(1,11);
                u_wis[i] = Random.Range(1,11);
                u_men[i] = Random.Range(1,11);
                a= (u_mov[i]-4)*2+u_vit[i]+u_str[i]+u_dex[i]+ u_agi[i]+u_luk[i]+u_int[i]+u_wis[i]+u_men[i];
                }
                
                u_gender[i] = Random.Range(0,2); //ランダムで０か１

                //ここに画面に表示させる方法をのせる
                newIDs[(createamount-remnant)] = i; //どこに新しいキャラを作成したかを記録
                remnant-=1;
            }
            if(remnant == 0) break;
        }
        return newIDs; //新しいキャラの作成位置を全てReturn
    }

    
}
public class CharasSaveAndLoad : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {
       SaveUnits();
        
    }
    public void SaveUnits()
    {
        UnitInfo allunitinfo= new UnitInfo();
        if(!PlayerPrefs.HasKey("HasUnitSaveData")){
            Debug.Log("新規セーブです");
            PlayerPrefs.SetString("HasUnitSaveData","yes");
                 
        } 
        else
        {
            Debug.Log("二回目以降のセーブです");        
            allunitinfo = UnitInfo.CreateFromSaveData();
           　
        }
         int[] newids= allunitinfo.CreateNewUnit_Randomly_ReturnIDs(3);  //どれが新規か一応取っとく
         string json_allunitinfo = JsonUtility.ToJson(allunitinfo);
         PlayerPrefs.SetString("AllUnitInfos",json_allunitinfo);
            
         PlayerPrefs.Save(); 
         Debug.Log(PlayerPrefs.GetString("AllUnitInfos")); 

    }
    public void ResetSaveData()
    {
        UnitInfo allunitinfo= new UnitInfo();
        int[] newids= allunitinfo.CreateNewUnit_Randomly_ReturnIDs(1);
        string json_allunitinfo = JsonUtility.ToJson(allunitinfo);
        PlayerPrefs.SetString("AllUnitInfos",json_allunitinfo);
        PlayerPrefs.DeleteKey("HasUnitSaveData");
        PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetString("AllUnitInfos"));


    }
   

}
