using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip 
{
   //まあ大体を言うとEquipも EquipControlクラスがあってキャラにセーブと共に（セーブデータのやつと統一してよろし）
   //クラスＥｑｕｉｐを保存する機能を持たせる
   //こっからMstEquips読み込み。
   //とどのつまりこいつはUniInfoといっしょや　UnitinfoのEquipはこいつのIDunmberや
   //マスターデータをいま管理するがないから今は全省略
    protected int EquipID;
    protected string EquipName;
    protected int EquipQuality;
    protected int EquipLevel;
    protected enum EquipKind
    {
        Hat,
        Weapon,
        SubWeapon,
        Armor,
        Mantle
    }
    protected string EquipManual;
    protected string EquipSpriteName;

    




}


