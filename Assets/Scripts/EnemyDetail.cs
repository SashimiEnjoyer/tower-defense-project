using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyDetail", menuName = "ScriptableObject/EnemyDetail")]

public class EnemyDetail : ScriptableObject
{
    public float maxHealth;
    public float speed;
    public float intervalAttack;
}
