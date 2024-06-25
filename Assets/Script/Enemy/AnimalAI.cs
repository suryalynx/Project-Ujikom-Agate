using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAI : MonoBehaviour
{
    public static AnimalAI Instance;
    public Transform checkpoint;
    public float speed = 3.0f;
    public float health = 100f;
    public int score;
    public GameObject deathVFXPrefab;
    public Animator animator;
    public HealthBarEnemy healthBar;


    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (healthBar != null)
        {
            healthBar.Initialize(health);
        }
    }

    void Update()
    {
        if (checkpoint != null)
        {
            MoveTowardsCheckpoint();
        }
        if (health <= 0)
        {
            HandleDeath();
        }
    }

    void MoveTowardsCheckpoint()
    {
        Vector3 direction = (checkpoint.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, checkpoint.position);

        if (distance > 0.1f)
        {
            transform.position += direction * speed * Time.deltaTime;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Health after damage: " + health);
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(health);
        }
    }
    private void HandleDeath()
    {
        SFXManager.instance.PlaySFX("DestroyAnimal");

        if (deathVFXPrefab != null)
        {
            GameObject vfx = Instantiate(deathVFXPrefab, transform.position, transform.rotation);
            Destroy(vfx, 3f);
        }

        GameManager.instance.AddScore(score);

        if (checkpoint != null)
        {
            Destroy(checkpoint.gameObject);
        }

        Destroy(gameObject);
    }

}
