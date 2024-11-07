using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerDetail", menuName = "ScriptableObject/TowerDetail")]
public class TowerDetail : ScriptableObject
{
    public string towerName;
    public float maxHealth;
    public float projectileSpeed;
    public float damage;
    public float costToUse;
    public float intervalShoot;
    [TextArea] public string description;
    public GameObject towerPrefab;
    public GameObject projectilePrefab;
    public Sprite towerImage;
}
