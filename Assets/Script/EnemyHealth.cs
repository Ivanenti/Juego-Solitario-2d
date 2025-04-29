using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(name + " recibi� " + damage + " de da�o.");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(name + " muri�.");
        Destroy(gameObject); // Destruye el enemigo
    }
}