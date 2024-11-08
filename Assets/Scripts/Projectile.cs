using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    private float damage;
    private Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    public void SetProjectileStats(TowerDetail detail)
    {
        damage = detail.damage;
        speed = detail.projectileSpeed;
    }

    public void SetAdditionalStats(TowerDetail detail)
    {
        damage += detail.damage;
        speed += detail.projectileSpeed;
    }

    private void FixedUpdate()
    {
        body.velocity = Vector3.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IEnemy>().Hit(damage);
            gameObject.SetActive(false);
        }

        if (other.CompareTag("End"))
        {
            gameObject.SetActive(false);
        }
    }
}
