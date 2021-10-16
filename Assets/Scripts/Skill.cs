using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill 
{
    private Unit attackUnit;
    private List<Unit> targetUnit = new List<Unit>();

    private int skillID;
    private string skillName;
    private int minAtkR = 1 ;
    private int maxAtkR = 1 ;
    //private enum rangeType{ round, line}
    //private int rangeArea = 1;

    public virtual void calDamage(Unit attackUnit)
    {

    }






}
