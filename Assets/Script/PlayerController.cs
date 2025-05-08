using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator animator;

    private bool isFacingRight = true;  // Para controlar la dirección en la que el personaje está mirando

    public float attackCooldown = 0.5f;  // Tiempo de espera entre ataques
    private float lastAttackTime = 0f;  // Para controlar el cooldown del ataque

    public Transform attackPoint;  // El punto de ataque
    public float attackRange = 0.5f;  // El rango del ataque
    public LayerMask enemyLayer;  // La capa de los enemigos

    public float bounceForce = 10f;  // Fuerza de rebote
    public float pushForce = 5f;  // Fuerza de empuje
    public float knockbackCooldown = 0.5f;  // Cooldown para el empuje
    private float lastKnockbackTime = -10f;  // Tiempo de la última vez que se aplicó un empuje

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        // Movimiento
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        // Animación de caminar
        animator.SetFloat("Speed", Mathf.Abs(move));

        // Verifica si el personaje está en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Volteo del sprite
        if (move < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);  // Voltea el sprite a la izquierda
            isFacingRight = false;  // El personaje ahora está mirando a la izquierda
        }
        else if (move > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);  // Voltea el sprite a la derecha
            isFacingRight = true;  // El personaje ahora está mirando a la derecha
        }

        // Ataque
        if (Input.GetMouseButtonDown(0) && Time.time > lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(1); // o el valor que tú definas como daño
            }
        }
    }

    // Método para aplicar rebote y empuje cuando colisiona con un enemigo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si el jugador colisiona con un enemigo
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            // Solo aplica el empuje si ha pasado el cooldown
            if (Time.time >= lastKnockbackTime + knockbackCooldown)
            {
                // Calcula la dirección del rebote (hacia arriba)
                Vector2 bounceDirection = (collision.transform.position - transform.position).normalized;
                rb.velocity = new Vector2(rb.velocity.x, 0); // Reseteamos la velocidad vertical antes de aplicar el rebote
                rb.AddForce(new Vector2(0, bounceForce), ForceMode2D.Impulse); // Rebote hacia arriba

                // Calcula la dirección del empuje (hacia atrás en el eje X)
                Vector2 pushDirection = (transform.position - collision.transform.position).normalized;
                rb.AddForce(new Vector2(pushDirection.x, 0) * pushForce, ForceMode2D.Impulse); // Empuje solo en el eje X

                // Registra el tiempo de la última aplicación de empuje
                lastKnockbackTime = Time.time;
            }
        }
    }
}