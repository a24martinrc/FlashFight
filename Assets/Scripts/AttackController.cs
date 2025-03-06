using UnityEngine;

public class AttackController : MonoBehaviour
{
    public float attackDamage = 10f; // Daño que causa el ataque.
    private BoxCollider2D attackCollider; // Collider que representa el área de ataque.

    void Start()
    {
        attackCollider = GetComponent<BoxCollider2D>();
        attackCollider.enabled = false; // Desactivar el collider al inicio.
    }

    public void PerformAttack()
    {
        Debug.Log("¡Ataque realizado!");

        // Activar el collider del ataque.
        attackCollider.enabled = true;

        // Desactivar el collider después de un breve tiempo.
        Invoke("DisableAttackCollider", 0.5f); // Ajusta el tiempo según la duración del ataque.
    }

    void DisableAttackCollider()
    {
        attackCollider.enabled = false;
        GetComponentInParent<PlayerController>().ResetPunch(); // Resetear el estado de golpe

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el collider del ataque golpea al enemigo.
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            Debug.Log("¡Golpe conectado!");
            collision.GetComponent<PlayerController>().TakeDamage(attackDamage); // Aplicar daño.
        }
    }
}