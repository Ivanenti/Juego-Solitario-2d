using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(name + " recibió " + damage + " de daño.");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(name + " murió.");
        Destroy(gameObject); // Destruye el enemigo
    }
}