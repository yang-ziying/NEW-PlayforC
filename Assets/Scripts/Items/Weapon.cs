using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equip
{
   private Skill WeaponSkill;
   private string WaponSkillManual;

   public Skill GetWeaponSkillforTest()//テスト用です
   {
       WeaponSkill= new Skill();
       WeaponSkill.SkillID=1;
       WeaponSkill.MinAtkR=3;
       WeaponSkill.MaxAtkR=5;
       WeaponSkill.RangeArea=3;
       return WeaponSkill;
   }

   
}
