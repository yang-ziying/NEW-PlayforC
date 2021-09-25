using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class UnitMove : MonoBehaviour
{
    private int mov=5; //テスト用のプレイヤーのMOV
    private int unitposix; //unitposiは内でルートを踏んで動くが動画の方はSeneManegerに任せる
    private int unitposiy; //そのためunitposiが示しているのは実際の位置（動画は故意にラグる）

    private bool isPositionSelecting = false;　//もしTrueだった場合目標位置をクリック可

   
    public static int[,] mapmove = new int[12,24]; //☆こいつに座標の移動ステータスが含まれている STATIC


    
    [SerializeField]
    private MapField mapfields;//ここにフィールドの情報入れておいた　今回の戦闘フィールドの　タイルごとの情報が入った二次元配列

    private MapTile[,] maptiles= new MapTile[12,24];　//マップフィールドの情報をそのまんま受け取る為の器

    private int lengthy; //maptileの長さを獲得、出ないように注意
    private int lengthx; //test
    
    private Stack<int> posistackx = new Stack<int>();
    private Stack<int> posistacky = new Stack<int>();


    

    // Start is called before the first frame update
    void Start()
    {
       lengthy = maptiles.GetLength(0);
       lengthx = maptiles.GetLength(1); 
       resetMapmove();
       maptiles= mapfields.GetMaptiles();//こいつで今回のフィールドの情報を全てGET

       changeUnitposi(5,5);　//changeUnitposi(y,x)でフィールドの情報更新、そしてCharaManagerに座標の目標位置まで動く命令
    }

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetMouseButtonDown(0)) //ここら辺はクリックしたときにクリックした時の座標で
        {
            

            if(isPositionSelecting == false) 
            {
                calMoveRange(unitposiy,unitposix,mov);　//移動できる全ての座標をＭａｐｍｏｖｅに書き込み赤くする,現在座標y、x、プレイヤー移動残り
                
            }
            else 
            {
                calMoveRootandMove(Tile.clicky,Tile.clickx); //Tileがクリックされたｘｙを元に動けるか判断して動ける場合は動いてＭａｐｍｏｖｅ終了、動けない場合はなにもせず終了 
               
            }
        }
    

    }
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
        isPositionSelecting = true;　//ポジションセレクト開始
      int sabunUp = 0;
      int sabunRight = 0;
      int sabunDown = 0;
      int sabunLeft = 0;
       

       mapmove[y,x] = moverem;
        
      
      
      if(y+1<lengthy) //←Array内かどうか判断
      {
            sabunUp = moverem-maptiles[y+1,x].movement;　//目標マップに移動するにあたる移動消費(maptiles[目標座標].movementはこのタイルの消費量が書いてある)の差を計算
            if(mapmove[y+1,x]< moverem && sabunUp>=0) calMoveRange(y+1,x,sabunUp);
             //解説：mapmove[目標座標]には予測されたそこへ移動した際の残り移動量が書いてある。
             //もしもっといい路線が構築された場合それを新しい値で置き換える。そのため、mapmoveがmoveremより小さい場合は置き換える権利と周りの再計算の権利を得る。
             //sabunUpはさっき計算した移動消費後の残り量の予測値。もしこれが0を下回った場合動けるマスではないので直接飛ばす。以下同
      }
      if(x+1<lengthx) 
      {
            sabunRight = moverem-maptiles[y,x+1].movement;
            if(mapmove[y,x+1]< moverem && sabunRight>=0) calMoveRange(y,x+1,sabunRight);
      }
      
      if(y-1>=0)
      {
          sabunDown = moverem-maptiles[y-1,x].movement;
          if(mapmove[y-1,x]< moverem && sabunDown>=0) calMoveRange(y-1,x,sabunDown);
      } 
      
      if(x-1>=0) 
      {
          sabunLeft = moverem-maptiles[y,x-1].movement;
         if(mapmove[y,x-1]< moverem && sabunLeft>=0) calMoveRange(y,x-1,sabunLeft);
      }
        
        
      
        
    }

    public void calMoveRootandMove(int y,int x)//クリック場所の座標y,x
    {
        
        //そこに移動できるかを判断
        //移動できなかった場合即座に終了
        int thisstep=mapmove[y,x];
        if (thisstep<0) {
            return;
        }


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
            Debug.Log("maxisup");
            calMoveRootandMove(y+1,x);
            
            return;
            
            }
        if (rightstep == maxvalue ) {
            Debug.Log("maxisright");
            calMoveRootandMove(y,x+1);
            
            return;
            }
        if (downstep == maxvalue ) {
            Debug.Log("maxisdown");
            calMoveRootandMove(y-1,x);
            
            return;
            }
        if (leftstep == maxvalue ) {
             Debug.Log("maxisleft");
            calMoveRootandMove(y,x-1);
           
            return;
            }
        }
        else
        {
            for( int i= posistackx.Count ; i>0; i--)  //さっきＳｔａｃｋした全部をＰｏｐしていく
                {
                    int xx= posistackx.Pop();
                    int yy= posistacky.Pop();
                   changeUnitposi(yy,xx);
                }
            resetMapmove();//pop終わったらＭａｐｍｏｖｅは仕事終了なので何もないころに戻す
             isPositionSelecting = false; //positionselect終了
        }

    }

    void changeUnitposi(int y,int x) //ゲームコードPosiも画像Posiも変えて行く必要があり
    {
        unitposix = x;
        unitposiy = y;
        CharaManager.addMovePos(y,x);
    }
}
