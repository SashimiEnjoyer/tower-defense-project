using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancerTower : MonoBehaviour, ITower
{
    [SerializeField] TowerDetail detail;
    private float currentHealth;

    private void Start()
    {
        currentHealth = detail.maxHealth;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            other.GetComponent<Projectile>().SetAdditionalStats(detail);
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


}
