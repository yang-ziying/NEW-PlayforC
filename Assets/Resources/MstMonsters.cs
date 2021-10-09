using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class MstMonsters : ScriptableObject
{
	public List<MstMonstersEntity> Entities; // Replace 'EntityType' to an actual type that is serializable.
}
