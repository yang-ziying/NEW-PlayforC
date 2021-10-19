using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    public static int[,] mapattack = new int[12,24]; //アタックのデフォ値も-1にしておいた
    public static int[,] mapattackRound= new int[12,24];
    private static int roundscale;

    public void PrepareStart()
    {
        ResetAttackRange();
        ResetAttackRangeRound();
        
    }
    public void ResetAttackRange()
    {
       
        for(int x=0; x<24; x++)
        {
            for(int y=0; y<12;y++){
                mapattack[y,x]=-1;
            }
        }

    }
    public void ResetAttackRangeRound()
    {
       
        for(int x=0; x<24; x++)
        {
            for(int y=0; y<12;y++){
                mapattackRound[y,x]=-1;
            }
        }

    }

    public static void ChangeAttackRound(int y,int x)//これ実をいうと画面の変更だけ
    {
       
          for(int xx=0; xx<24; xx++)
        {
            for(int yy=0; yy<12;yy++){
               mapattackRound[yy,xx]=-1;
            }
        }
      
       for(int rx=0;rx<=roundscale;rx++)
       {
           for(int ry=0;ry<=roundscale;ry++)
           {
               if(rx+ry<=roundscale)
               {
               if(y+ry<12&&x+rx<24) mapattackRound[y+ry,x+rx]=1;
               if(y+ry<12&&x-rx>=0) mapattackRound[y+ry,x-rx]=1;
               if(y-ry>=0&&x+rx<24) mapattackRound[y-ry,x+rx]=1;
               if(y-ry>=0&&x-rx>=0) mapattackRound[y-ry,x-rx]=1;
               }

           }
       }
       


    }

   public void CalAttackRange(int y, int x,Skill normalattackskill) //RangeMinとAttackはスキルから、ここをスキル名に変更する
   {
       
      mapattack=normalattackskill.calAttackRange(y,x);
      roundscale=normalattackskill.RangeArea;
     
   }
   public void GetUnitAttacked(int[,] mapunit,string team)
   {
       int mapunitClickIdx=mapunit[Tile.clicky,Tile.clickx];
       if(team =="player"　&& mapunitClickIdx < BattleSceneManager.partynumber) return; //クリック場所に敵キャラがいなかった場合なしidxは前は味方、後ろは敵
       if(team =="enemy"　&& mapunitClickIdx >= BattleSceneManager.partynumber) return; //クリック場所に味方キャラがいなかった場合なし
        int clicklocation=mapattack[Tile.clicky,Tile.clickx];
        if (clicklocation<0) return;    //クリック場所が範囲外だった場合なし
        Debug.Log("攻撃！");
        //スキルに攻撃されたmapunitIDxをマネジャーに返す
        //それを受け取ったマネジャーはスキルから一般攻撃スキル継承クラスを呼び出し、攻撃Unitと被攻撃UnitのUnitクラスをそのまんま渡す
        //受け取ったスキルがUnitのHPと攻撃力と防御力とその他諸々を元にダメージを計算して、ダメージをマネジャーに返す（Unitのスキルリストからカウンタースキル有無の判断とかも）
        //マネジャー元でその分の体力減らす。同時にキャラマネジャーに減らした動画と攻撃動画再生させる（カウンタースキルがある場合は2週目？）
        //キャラマネジャーが終わった事をマネジャーに伝える。ターンエンド


   }

 
}
