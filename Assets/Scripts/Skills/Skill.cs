using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill 
{
    
   

    private int skillID;
    private string skillName;
    private int minAtkR;
    private int maxAtkR;
    //private enum rangeType{ round, line}
    private int rangeArea;

    public int SkillID{get=>skillID;set=>skillID=value;}
    public string SkillName{get=>skillName;set=>skillName=value;}
    public int MinAtkR{get=>minAtkR;set=>minAtkR=value;}
    public int MaxAtkR{get=>maxAtkR;set=>maxAtkR=value;}
    public int RangeArea{get=>rangeArea;set=>rangeArea=value;}


    public virtual void calDamage(Unit attackUnit,Unit targetUnit)
    {

    }
    //public virtual Skill(int mokuninchi){skillID=mokuninchi;} //黙認値の設定。
                            //使い方はSkill skill = new Skill(3)みたいな感じ
     public virtual int[,] calAttackRange(int y, int x) //自分の位置、
   {    int[,] mapattack = new int[12,24];
         for(int xx=0; xx<24; xx++)
        {
            for(int yy=0; yy<12;yy++){
                mapattack[yy,xx]=-1;
            }
        }
       //for(int i=rangeMin;i<=rangeMax;i++)
       //{ n 
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
       //基本模型、１つのターゲットにダメージの範囲
       for(int rx=0;rx<=maxAtkR;rx++)
       {
           for(int ry=0;ry<=maxAtkR;ry++)
           {
               if(rx+ry<=maxAtkR&&rx+ry>= minAtkR)
               {
               if(y+ry<12&&x+rx<24) mapattack[y+ry,x+rx]=1;
               if(y+ry<12&&x-rx>=0) mapattack[y+ry,x-rx]=1;
               if(y-ry>=0&&x+rx<24) mapattack[y-ry,x+rx]=1;
               if(y-ry>=0&&x-rx>=0) mapattack[y-ry,x-rx]=1;
               }

           }
       }

       return mapattack;

   }

   





}
