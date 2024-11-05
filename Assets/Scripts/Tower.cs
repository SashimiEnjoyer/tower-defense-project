using UnityEngine;

public interface ITower
{
    void Hit();
}

public class Tower : MonoBehaviour, ITower
{
    [SerializeField] float maxHealth;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform projectileOutput;

    float currentHealth;

    private void Start()
    {
        InvokeRepeating(nameof(ShootProjectile), 2, 5);
    }

    void ShootProjectile()
    {
        GameObject go = Instantiate(projectile, projectileOutput.position, Quaternion.identity, null);
        Destroy(go, 5f);
    }

    public void Hit()
    {
        currentHealth--;
        if(currentHealth < 0)
            gameObject.SetActive(false);
    }
}
