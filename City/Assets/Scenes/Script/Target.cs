
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    [SerializeField] private Animator m_animator;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        m_animator.SetTrigger("Dead");
    }
}
