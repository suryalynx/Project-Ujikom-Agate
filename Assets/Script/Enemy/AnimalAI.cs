using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAI : MonoBehaviour
{
    public Transform checkpoint;
    public float speed = 3.0f;
    public int health = 100;
    public int score;
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (checkpoint != null)
        {
            MoveTowardsCheckpoint();
        }
        if (health <= 0)
        {
            SFXManager.instance.PlaySFX("DestroyAnimal");
            GameManager.instance.AddScore(score);
            Destroy(gameObject);
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
            animator.SetTrigger("IsWalk");
        }
        else{
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
    }
}
