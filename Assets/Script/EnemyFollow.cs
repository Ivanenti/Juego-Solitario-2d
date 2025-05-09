using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRange = 10f;
    public Transform player;
    public LayerMask playerLayer;

    private Vector3 originalScale;
    private Animator animator;  // 👈 Añadido

    private void Start()
    {
        originalScale = transform.localScale;
        animator = GetComponent<Animator>();  // 👈 Obtenemos el Animator
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

                // Volteo del sprite
                if (direction.x > 0)
                    transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
                else if (direction.x < 0)
                    transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);

                // 👇 Aquí actualizas la animación de caminar
                animator.SetFloat("Speed", Mathf.Abs(direction.x));
            }
            else
            {
                // 👇 Si el jugador está fuera de rango, se queda quieto
                animator.SetFloat("Speed", 0f);
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
