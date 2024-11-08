using System.Collections.Generic;
using UnityEngine;

public interface ITower
{
    void Hit(float damage);
}

public class Tower : MonoBehaviour, ITower
{
    [SerializeField] TowerDetail towerDetail;
    [SerializeField] Transform projectileOutput;
    [SerializeField] Transform enemyDetection;
    [SerializeField] LayerMask enemyLayer;

    private List<GameObject> projectilePool = new();
    private int projectileCount = 0;
    private float currentHealth;
    private float counter;
    private bool isBattleState = false;

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
        currentHealth = towerDetail.maxHealth;
    }

    private void Update()
    {
        if(!isBattleState)
            return;

        if (Physics.Raycast(enemyDetection.position, enemyDetection.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, enemyLayer))
        {

            counter += Time.deltaTime;

            if (counter > towerDetail.intervalShoot)
            {
                ShootProjectile();
                counter = 0;
            }

        }else
            counter = 0; ;
    }

    void InitializeProjectile()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(towerDetail.projectilePrefab, projectileOutput.position, Quaternion.identity, transform);
            go.SetActive(false);
            projectilePool.Add(go);
        }
    }

    void ShootProjectile()
    {
        projectilePool[projectileCount].transform.position = projectileOutput.position;
        projectilePool[projectileCount].GetComponent<Projectile>().SetProjectileStats(towerDetail);
        projectilePool[projectileCount].SetActive(true);
        projectileCount++;
        projectileCount %= 10;
    }

    public void Hit(float damage)
    {
        currentHealth-= damage;
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
