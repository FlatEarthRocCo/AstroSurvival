using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable object/Enemy", order = 1)]
public class EnemyData : ScriptableObject
{
 public int hp;
 public int damage;
 public float speed;
}
