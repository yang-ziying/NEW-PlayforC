using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCal
{
    public static int CalmaxHP(int u_vit,int u_str){return u_vit*5+ u_str*5;}
    public static int CalAtk(int u_str,int u_dex){return u_str*3+u_dex;}
    public static int CalDef(int u_vit,int u_str){return u_vit*3+u_str;}
    public static int CalSpd(int u_agi,int u_dex){return u_agi*3+u_dex;}
}
