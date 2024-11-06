using UnityEngine;

public interface ITower
{
    void Hit();
}

public class Tower : MonoBehaviour, ITower
{
    [SerializeField] TowerDetail towerDetail;
    [SerializeField] Transform projectileOutput;

    float currentHealth;
    bool canShoot = false;

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

    void ShootProjectile()
    {
        GameObject go = Instantiate(towerDetail.projectilePrefab, projectileOutput.position, Quaternion.identity, null);
        Destroy(go, 20f);
    }

    public void Hit()
    {
        currentHealth--;
        if(currentHealth < 0)
            gameObject.SetActive(false);
    }

    void CheckState(GameplayState state) 
    {
        if (state == GameplayState.Night)
            InvokeRepeating(nameof(ShootProjectile), 0, 2);
        else
            CancelInvoke(nameof(ShootProjectile));
    }
}
