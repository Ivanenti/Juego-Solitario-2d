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

    private bool isFacingRight = true;

    public float attackCooldown = 0.5f;
    private float lastAttackTime = 0f;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    public float bounceForce = 10f;
    public float pushForce = 5f;
    public float knockbackCooldown = 0.5f;
    private float lastKnockbackTime = -10f;

    private Vector3 originalScale;  // Guarda la escala original
    private Transform currentPlatform = null;  // Guarda la plataforma actual

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;  // Guarda la escala inicial
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(move * speed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(move));

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (move < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            isFacingRight = false;
        }
        else if (move > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            isFacingRight = true;
        }

        if (Input.GetMouseButtonDown(0) && Time.time > lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D collider in hitEnemies)
        {
            if (collider.CompareTag("Enemy")) // Asegúrate que el enemigo tenga el Tag "Enemy"
            {
                EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(1); // o la cantidad que desees
                }
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent = collision.transform;
        }

        // Verifica si el jugador colisiona con un enemigo (para rebote)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (Time.time >= lastKnockbackTime + knockbackCooldown)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0); // Reseteamos la velocidad vertical
                rb.AddForce(new Vector2(0, bounceForce), ForceMode2D.Impulse); // Rebote vertical

                Vector2 pushDirection = (transform.position - collision.transform.position).normalized;
                rb.AddForce(new Vector2(pushDirection.x, 0) * pushForce, ForceMode2D.Impulse); // Empuje horizontal

                lastKnockbackTime = Time.time;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent = null;
        }
    }

}

