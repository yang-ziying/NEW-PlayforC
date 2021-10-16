using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
     private static int[,] mapattack = new int[12,24]; //アタックのデフォ値も-1にしておいた


    public void PrepareStart()
    {
        ResetAttackRange();
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

   public void CalAttackRange(int y, int x, int rangeMin, int rangeMax)
   {
       //for(int i=rangeMin;i<=rangeMax;i++)
       //{
                //X型だった
               //if(y+i<12&&x+i<24) mapattack[y+i,x+i]=1;
               //if(y+i<12&&x-i>=0) mapattack[y+i,x-i]=1;
               //if(y-i>=0&&x+i<24) mapattack[y-i,x+i]=1;
               //if(y-i>=0&&x-i>=0) mapattack[y-i,x-i]=1;

            //十字型だった
           //if(y+i<12) mapattack[y+i,x]=1;
           //if(x-i>=0) mapattack[y,x-i]=1;
           //if(y-i>=0) mapattack[y-i,x]=1;
          // if(x+i<24) mapattack[y,x+i]=1;
       //}
       //合計値制限しなくて、int rx=ranggeMinからやると四角が４つ
       for(int rx=0;rx<=rangeMax;rx++)
       {
           for(int ry=0;ry<=rangeMax;ry++)
           {
               if(rx+ry<=rangeMax&&rx+ry>=rangeMin)
               {
               if(y+ry<12&&x+rx<24) mapattack[y+ry,x+rx]=1;
               if(y+ry<12&&x-rx>=0) mapattack[y+ry,x-rx]=1;
               if(y-ry>=0&&x+rx<24) mapattack[y-ry,x+rx]=1;
               if(y-ry>=0&&x-rx>=0) mapattack[y-ry,x-rx]=1;
               }

           }
       }

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

    public static int[,] GetMapAttack{get=>mapattack;}
}
