using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void Hit();
}

public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] float speed = 15f;
    [SerializeField] float maxHealth = 5f;

    float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * -1 * speed * Time.deltaTime);
    }

    public void Hit()
    {
        currentHealth--;

        if(currentHealth <= 0)
            gameObject.SetActive(false);
    }
}
