using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapTile", menuName = "PlayforC/MapTile", order = 0)]
public class MapTile : ScriptableObject {
    
    [SerializeField]
    private int Movement;   //基本Movement消費

    [SerializeField]
    private int MovementAir; //基本

    [SerializeField]
    private int MovementHorse;

    [SerializeField]
    private int MovementShip;

    [SerializeField]
    private string TileName;

    [SerializeField]
    private Sprite TileImage;

    
    public int movement{get=>Movement;}
    public int movementAir{get=>MovementAir;}
    public int movementHorse{get=>MovementHorse;}
    public int movementShip{get=>MovementShip;}
    public string tileName{get=>TileName;}
    public Sprite tileImage {get=>TileImage;}



}
