using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class UnitMove : MonoBehaviour
{
   
    private static int[,] mapmove = new int[12,24]; //☆こいつに座標の移動ステータスが含まれている STATIC


    
    [SerializeField]
    private MapField mapfields;//ここにフィールドの情報入れておいた　今回の戦闘フィールドの　タイルごとの情報が入った二次元配列
    [SerializeField]
    BattleSceneManager scenemanager;
   

    private int[,] mapMovementInfo= new int[12,24];　//マップフィールドの移動情報だけを受け取る

    private int lengthy; //mapMovementInfoの長さを獲得、出ないように注意
    private int lengthx; //test
    
    private Stack<int> posistackx = new Stack<int>();
    private Stack<int> posistacky = new Stack<int>();


    

    // Start is called before the first frame update
   
    public void PrepareStart() // スタート時に起動
    {
       lengthy = mapMovementInfo.GetLength(0);
       lengthx = mapMovementInfo.GetLength(1); 
       mapMovementInfo=mapfields.GetMovementArray();
       resetMapmove();
    }

    public static int[,] GetMapmve{get=>mapmove;}

 
    private void resetMapmove() //全てのMAPMOVEを-1に戻す
    {
        for(int x=0; x<24; x++){
            for(int y=0; y<12;y++){
                mapmove[y,x]=-1;
                //Debug.Log(mapmove[y,x]);
            }
        }
    }
    

    public void calMoveRange(int y,int x,int moverem)//現在座標y、x、プレイヤー移動残り
    {
       
        

        
      int sabunUp = 0;
      int sabunRight = 0;
      int sabunDown = 0;
      int sabunLeft = 0;
       

       mapmove[y,x] = moverem;
        
      
      
      if(y+1<lengthy) //←Array内かどうか判断
      {
            sabunUp = moverem-mapMovementInfo[y+1,x];　//目標マップに移動するにあたる移動消費(mapMovementInfo[目標座標]はこのタイルの消費量が書いてある)の差を計算
            if(mapmove[y+1,x]< moverem && sabunUp>=0) calMoveRange(y+1,x,sabunUp);
             //解説：mapmove[目標座標]には予測されたそこへ移動した際の残り移動量が書いてある。
             //もしもっといい路線が構築された場合それを新しい値で置き換える。そのため、mapmoveがmoveremより小さい場合は置き換える権利と周りの再計算の権利を得る。
             //sabunUpはさっき計算した移動消費後の残り量の予測値。もしこれが0を下回った場合動けるマスではないので直接飛ばす。以下同
      }
      if(x+1<lengthx) 
      {
            sabunRight = moverem-mapMovementInfo[y,x+1];
            if(mapmove[y,x+1]< moverem && sabunRight>=0) calMoveRange(y,x+1,sabunRight);
      }
      
      if(y-1>=0)
      {
          sabunDown = moverem-mapMovementInfo[y-1,x];
          if(mapmove[y-1,x]< moverem && sabunDown>=0) calMoveRange(y-1,x,sabunDown);
      } 
      
      if(x-1>=0) 
      {
          sabunLeft = moverem-mapMovementInfo[y,x-1];
         if(mapmove[y,x-1]< moverem && sabunLeft>=0) calMoveRange(y,x-1,sabunLeft);
      }
        
        
      
        
    }

    public void calMoveRootandMove(int[,] mapunit,int unitidx)　//OverLoad 全てのキャラ、現在のキャラのUnitsでの位置
    {
         
          int mapunitClickIdx=mapunit[Tile.clicky,Tile.clickx];
          if (mapunitClickIdx != unitidx && mapunitClickIdx >=0) return; //そこに自分自身じゃないキャラがいたらReturn

        int clicklocation=mapmove[Tile.clicky,Tile.clickx];
        if (clicklocation<0) {
            return;
        } //そこに移動できるかを判断
        //移動できなかった場合何も発生させない
        calMoveRootandMove(Tile.clicky, Tile.clickx);
        
    }
    public void calMoveRootandMove(int y, int x)//クリック場所の座標y,x 全ユニット情報
    {
        int thisstep=mapmove[y,x];
        //本題。周り4マスをまず移動出来るか判断。もし端っこだった場合(lengthが飛び出てた場合)、移動できないマスとして-1を代入
        //移動できるマスならmapmove[目標マス]の情報を拾ってくる。0から始まって増えていくやつや
        int upstep;
        int rightstep;
        int downstep;
        int leftstep;

        //Debug.Log("I'm here:"+x+","+y);
        
        if(y+1<lengthy) upstep= mapmove[y+1,x];
        else upstep= -1;
        if(x+1<lengthx) rightstep = mapmove[y,x+1];
        else rightstep = -1;
        if(y-1>=0) downstep = mapmove[y-1,x];
        else downstep = -1;
        if(x-1>=0) leftstep = mapmove[y,x-1];
        else leftstep = -1;

        //そんで一番大きいやつを判断。（＝回り移動可マスは４，３、２，１，０と下げて行ったが、遡りは大きければ大きいほど有意義なステップになる）

        int maxvalue = Math.Max(upstep,Math.Max(rightstep,Math.Max(downstep,leftstep)));
        if( maxvalue > thisstep )//有意義なステップが依然存在するかを判断　上にしか上らない。同じ数字もなし。スタックにｘｙの遡りルート座標をため込む。
                                 //そして一番大きいやつの場所に移動して次同じことを繰り返す。もし周りにおおきいやつが存在しない場合そこがスタート地点になるので終了。
        {
            posistackx.Push(x);
            posistacky.Push(y);

        if (upstep == maxvalue ) {
            //Debug.Log("maxisup");
            calMoveRootandMove(y+1,x);
            
            return;
            
            }
        if (rightstep == maxvalue ) {
            //Debug.Log("maxisright");
            calMoveRootandMove(y,x+1);
            
            return;
            }
        if (downstep == maxvalue ) {
            //Debug.Log("maxisdown");
            calMoveRootandMove(y-1,x);
            
            return;
            }
        if (leftstep == maxvalue ) {
             //Debug.Log("maxisleft");
            calMoveRootandMove(y,x-1);
           
            return;
            }
        }
        else
        {
            scenemanager.SetPosiStack(posistacky,posistackx);
                
            resetMapmove();//pop終わったらＭａｐｍｏｖｅは仕事終了なので何もないころに戻す
            
            
        }

    }


   
}
