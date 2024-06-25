using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    public int damage = 25;

    private void OnTriggerEnter(Collider other)
    {
        AnimalAI enemyAI = other.GetComponent<AnimalAI>();

        if (enemyAI != null && (other.gameObject.CompareTag("Anjing") || other.gameObject.CompareTag("Sapi") || other.gameObject.CompareTag("Kuda") || other.gameObject.CompareTag("Rusa")))
        {
            enemyAI.TakeDamage(damage);
            SFXManager.instance.PlaySFX("SFXEat");
            Destroy(gameObject);
        }
    }
}
