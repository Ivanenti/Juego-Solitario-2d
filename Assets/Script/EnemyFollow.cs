using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRange = 10f;
    public Transform player;
    public LayerMask playerLayer;

    private Vector3 originalScale;  // Variable para almacenar la escala original

    private void Start()
    {
        // Guardar la escala original para evitar cualquier cambio no deseado
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance < detectionRange)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                transform.position += (Vector3)(direction * speed * Time.deltaTime);

                // Volteamos el sprite según la dirección, pero sin cambiar la escala en Y
                if (direction.x > 0)
                    transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z); // Mira a la derecha
                else if (direction.x < 0)
                    transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z); // Mira a la izquierda
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage();
            }
        }
    }

}