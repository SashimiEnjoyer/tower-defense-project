using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        body.velocity = Vector3.forward * speed * Time.deltaTime;
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IEnemy>().Hit();
            gameObject.SetActive(false);
        }
    }
}
