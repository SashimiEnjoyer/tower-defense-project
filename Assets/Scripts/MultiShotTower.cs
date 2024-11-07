using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class MultiShotTower : MonoBehaviour, ITower
{
    [SerializeField] TowerDetail detail;
    [SerializeField] Transform[] projectileOutput;

    List<GameObject> projectilePool = new();
    int projectileCount = 0;
    float currentHealth;
    float counter;
    bool isBattleState = false;

    private void Awake()
    {
        GameplayManager.instance.onStateChange += CheckState;
        InitializeProjectile();
    }

    private void OnDestroy()
    {
        GameplayManager.instance.onStateChange -= CheckState;
    }

    private void Start()
    {
        currentHealth = detail.maxHealth;
    }

    private void Update()
    {
        if (!isBattleState)
            return;

        counter += Time.deltaTime;

        if (counter > detail.intervalShoot)
        {
            ShootProjectile();
            counter = 0;
        }
    }

    void InitializeProjectile()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject go = Instantiate(detail.projectilePrefab, transform.position, Quaternion.identity, transform);
            go.SetActive(false);
            projectilePool.Add(go);
        }
    }

    void ShootProjectile()
    {
        for (int i = 0; i < projectileOutput.Length; i++)
        {
            int temp = i;
            projectilePool[projectileCount].transform.position = projectileOutput[temp].position;
            projectilePool[projectileCount].GetComponent<Projectile>().SetProjectileStats(detail);
            projectilePool[projectileCount].SetActive(true);
            projectileCount++;
            projectileCount %= 30;
        }
    }

    public void Hit(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            transform.Translate(new Vector3(transform.position.x, transform.position.y - 5, transform.position.z));
            Destroy(gameObject, 2);
        }
    }

    void CheckState(GameplayState state)
    {
        if (state == GameplayState.Night)
            isBattleState = true;
        else
            isBattleState = false;
    }
}
