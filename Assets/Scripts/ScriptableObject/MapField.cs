using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MapField", menuName = "PlayforC/MapField", order = 0)]
public class MapField : ScriptableObject {
    [SerializeField]
    private List<MapTile> Maptiles= new List<MapTile>();
    [SerializeField]
    private string Mapname;
    //[SerializeField]
    //private List <Monster> monsters = new List<Monster>();


    
    private MapTile[,] Field = new MapTile[12,24];

    private int[,] lines = new int[12,24]
    {
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,2,2,2,2,0,0,0,2,0,0,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {1,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,1},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
    };

    public MapTile[,] GetMaptiles(){
    for(int x=0;x<24;x++)
    { for(int y=0; y<12; y++)
        { 
            int n = lines[y,x]; //タイルを埋めて返す
            Field[y,x]=Maptiles[n];
         }
    }
    return Field;
    }
    public int[,] GetMovementArray()//移動だけを送るぜ
    { 
        int[,] movementArray= new int[12,24];
        for(int x=0;x<24;x++)
        { 
            for(int y=0; y<12; y++)
            { 
            int n = lines[y,x]; //タイルを埋めて返す
            movementArray[y,x]=Maptiles[n].movement;
            }
        }
        return movementArray;

    }
    

    public string mapName {get=>Mapname;}



}