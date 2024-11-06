using UnityEngine;

public interface ITower
{
    void Hit();
}

public class Tower : MonoBehaviour, ITower
{
    [SerializeField] TowerDetail towerDetail;
    [SerializeField] Transform projectileOutput;
    [SerializeField] Transform enemyDetection;
    [SerializeField] LayerMask enemyLayer;

    float currentHealth;
    float counter;
    bool canShoot = false;
    bool isBattleState = false;
    RaycastHit hit;

    private void Awake()
    {
        GameplayManager.instance.onStateChange += CheckState;
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

        if (Physics.Raycast(enemyDetection.position, enemyDetection.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, enemyLayer))
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

    void ShootProjectile()
    {
        GameObject go = Instantiate(towerDetail.projectilePrefab, projectileOutput.position, Quaternion.identity, null);
        Destroy(go, 20f);
    }

    public void Hit()
    {
        Debug.Log("Tower Hit");
        currentHealth--;
        if(currentHealth < 0)
            gameObject.SetActive(false);
    }

    void CheckState(GameplayState state) 
    {
        if (state == GameplayState.Night)
            isBattleState = true;
        else
            isBattleState = false;
    }
}
