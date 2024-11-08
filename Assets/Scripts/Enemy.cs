using UnityEngine;
using UnityEngine.Events;

public interface IEnemy
{
    void Hit(float damage);
}

public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] EnemyDetail enemyDetail;
    ParticleSystem hitEffect;

    private Rigidbody body;
    protected float currentHealth;
    private float timeTracker = 0f;
    private bool isMove = false;

    public UnityAction onDied;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        GameObject go = Instantiate(enemyDetail.hitFx, transform);
        hitEffect = go.GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        SetMaxHealth();
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
        hitEffect.Emit(Random.Range(50,100));

        if (currentHealth <= 0)
        {   
            onDied?.Invoke();
            GameplayManager.instance.Resources += 10;
            gameObject.SetActive(false);
        }
    }

    protected void SetMaxHealth()
    {
        currentHealth = enemyDetail.maxHealth;
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
