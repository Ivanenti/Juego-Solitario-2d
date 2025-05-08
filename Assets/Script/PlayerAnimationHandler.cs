using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    public Animator animator;

    public void SetSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    public void PlayAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void PlayHurt()
    {
        animator.SetTrigger("Hurt");
    }

    public void PlayDeath()
    {
        animator.SetBool("IsDead", true);
        animator.SetTrigger("Die");
    }
}
