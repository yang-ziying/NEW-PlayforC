using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party
{
    public static int[] GetPartyMembersID(int charanum)
    {
        UnitInfo allunitinfo= UnitInfo.CreateFromSaveData();
        int[] menbersID= new int[charanum];
        for(int i=0;i<charanum; i++)// ＊＊＊＊＊臨時＊＊＊＊
        {
            menbersID[i]=allunitinfo.id[i]; // ＊＊＊＊＊臨時＊＊＊＊
        }
        

        return menbersID;
    }

}

public class BattleSceneManager : MonoBehaviour
{
    private int enemyNum = 10; //number of enemy  <---!!should be in FieldData!!
    private static int charaNum=6;
    List<int> IDforTurn = new List<int>();
    
    //CharaのリストをPartyから引っ張ってくる
    int[] partyIDs= Party.GetPartyMembersID(charaNum); //number of chara = 6 now


    





    // Start is called before the first frame update
    void Start()
    {
        //test
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
