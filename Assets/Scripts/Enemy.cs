using UnityEngine;
using UnityEngine.Events;

public interface IEnemy
{
    void Hit();
}

public class Enemy : MonoBehaviour, IEnemy
{
    //[SerializeField] float speed = 15f;
    //[SerializeField] float maxHealth = 5f;
    //[SerializeField] float intervalAttack = 1f;
    [SerializeField] EnemyDetail enemyDetail;
    Rigidbody body;

    float currentHealth;
    float timeTracker = 0f;
    bool isMove = false;

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
            body.velocity = Vector3.forward * -1 * enemyDetail.speed * Time.deltaTime;
        else
            body.velocity = Vector3.zero;
    }

    public void Hit()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {   
            onDied?.Invoke();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
            isMove = false;

        if (other.CompareTag("End"))
        {
            GameplayManager.instance.ChangeToDay();
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
                other.GetComponent<ITower>().Hit();
                timeTracker = 0f;
            }
        }
    }
}
