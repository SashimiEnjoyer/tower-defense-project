using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerDetail", menuName = "ScriptableObject/TowerDetail")]
public class TowerDetail : ScriptableObject
{
    public string towerName;
    public float maxHealth;
    public float costToUse;
    public float intervalShoot;
    public GameObject towerPrefab;
    public GameObject projectilePrefab;
    public Sprite towerImage;
}
