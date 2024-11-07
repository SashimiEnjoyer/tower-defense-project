using UnityEngine;
using UnityEngine.Events;

public interface IEnemy
{
    void Hit(float damage);
}

public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] EnemyDetail enemyDetail;
    Rigidbody body;

    float currentHealth;
    float timeTracker = 0f;
    public bool isMove = false;

    public UnityAction onDied;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        currentHealth = enemyDetail.maxHealth;
    }

    private void OnEnable()
    {
        isMove = true;
    }

    private void FixedUpdate()
    {
        if (isMove)
            body.velocity = -1 * enemyDetail.speed * Time.deltaTime * Vector3.forward;
        else
            body.velocity = Vector3.zero;
    }

    public void Hit(float damage)
    {
        currentHealth-= damage;

        if (currentHealth <= 0)
        {   
            onDied?.Invoke();
            GameplayManager.instance.Resources += 10;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
            isMove = false;

        if (other.CompareTag("End"))
        {
            GameplayManager.instance.Lost();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            timeTracker += Time.deltaTime;

            if (timeTracker >= enemyDetail.intervalAttack)
            {
                other.GetComponent<ITower>().Hit(enemyDetail.damage);
                timeTracker = 0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        isMove = true;
    }
}
