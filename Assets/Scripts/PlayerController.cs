using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 moveInput;

    private string playerTag;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float crouchHeight = 0.5f; // Altura reducida al agacharse
    private bool isCrouching = false;
    private bool isJumping = false;

    private BoxCollider2D boxCollider;
    private Vector2 originalSize; // Tamaño original del collider
    private Vector2 originalOffset; // Offset original del collider

    [SerializeField] private float maxHealth = 100f; // Vida máxima
    private float currentHealth;    // Vida actual
    [SerializeField] private Image healthBar; // Asigna esta variable en el Inspector
    [SerializeField] private Gradient healthGradient; // Gradiente para el color de la barra
    [SerializeField] private AttackController attackManager; // Asigna el AttackManager en el Inspector
    private Collider2D otherPlayerCollider; // Collider del otro jugador
    private bool isBlocking = false; // Estado de bloqueo
    private bool isPunching = false; // Variable para controlar si el jugador está golpeando

    private Animator animator; // Referencia al Animator

    [SerializeField] private AudioSource audioSource; // Fuente de audio
    [SerializeField] private AudioClip punchSound; // Clip de sonido para el golpe


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTag = gameObject.tag;

        // Obtener el BoxCollider2D del jugador
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            originalSize = boxCollider.size; // Guardar el tamaño original
            originalOffset = boxCollider.offset; // Guardar el offset original
        }
        currentHealth = maxHealth; // Inicializar la vida al máximo

        GameObject otherPlayer = GameObject.FindGameObjectWithTag(playerTag == "Player1" ? "Player2" : "Player1");
        if (otherPlayer != null)
        {
            otherPlayerCollider = otherPlayer.GetComponent<Collider2D>();
        }

        animator = GetComponent<Animator>(); // Obtener el Animator
    }

    void Update()
    {
        if (playerTag == "Player1")
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal_P1"), 0);

            if (Input.GetKeyDown(KeyCode.W) && !isJumping)
            {
                Jump();
            }
            animator.SetBool("IsJumping", !isJumping);

            if (Input.GetKey(KeyCode.S))
            {
                isCrouching = true;
                Crouch();
            }
            else
            {
                isCrouching = false;
                ResetHeight();
            }

            animator.SetBool("IsCrouching", isCrouching);

            if (Input.GetKey(KeyCode.K))
            {
                isBlocking = true;
            }
            else
            {
                isBlocking = false;
            }

            animator.SetBool("IsBlocking", isBlocking);

            animator.SetBool("IsJumping", isJumping);
            animator.SetBool("Punch", isPunching);
        }
        else if (playerTag == "Player2")
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal_P2"), 0);

            if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping)
            {
                Jump();
            }
            animator.SetBool("IsJumping", !isJumping);

            if (Input.GetKey(KeyCode.DownArrow))
            {
                isCrouching = true;
                Crouch();
            }
            else
            {
                isCrouching = false;
                ResetHeight();
            }

            animator.SetBool("IsCrouching", isCrouching);

            if (Input.GetKey(KeyCode.X))
            {
                isBlocking = true;
            }
            else
            {
                isBlocking = false;
            }

            animator.SetBool("IsBlocking", isBlocking);

            animator.SetBool("IsJumping", isJumping);
            animator.SetBool("Punch", isPunching);
        }

        if (playerTag == "Player1" && Input.GetKeyDown(KeyCode.J))
        {
            isPunching = true;
            animator.SetTrigger("Punch"); // Activar la animación de golpe
            attackManager.PerformAttack(); // Llamar al gestor de ataques
            audioSource.PlayOneShot(punchSound); // Reproducir sonido de golpe
        }
        else if (playerTag == "Player2" && Input.GetKeyDown(KeyCode.Z))
        {
            isPunching = true;
            animator.SetTrigger("Punch"); // Activar la animación de golpe
            attackManager.PerformAttack(); // Llamar al gestor de ataques
            audioSource.PlayOneShot(punchSound); // Reproducir sonido de golpe
        }
        else{
            isPunching = false;
        }

        // Ignorar colisiones entre jugadores si uno está en el aire
        if (isJumping && otherPlayerCollider != null)
        {
            Physics2D.IgnoreCollision(boxCollider, otherPlayerCollider, true);
        }

        animator.SetFloat("Speed", Mathf.Abs(moveInput.x));
    }

    void FixedUpdate()
    {
        // Movimiento horizontal
        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(!isJumping){    
            if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
            {
                // Obtener dirección relativa
                Vector2 direction = (collision.transform.position - transform.position).normalized;

                // Si el jugador intenta moverse hacia el otro, reducir velocidad
                if (Vector2.Dot(direction, rb.velocity) > 0)
                {
                    rb.velocity *= 0.3f; // Reduce la velocidad un 70%
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Field"))
        {
            isJumping = false;

            // Restaurar colisiones entre jugadores cuando tocan el suelo
            if (otherPlayerCollider != null)
            {
                Physics2D.IgnoreCollision(boxCollider, otherPlayerCollider, false);
            }
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        isJumping = true;
    }

    void Crouch()
    {
        if (boxCollider != null)
        {
            // Reducir la altura del collider
            boxCollider.size = new Vector2(originalSize.x, crouchHeight);

            // Ajustar el offset para que el collider esté alineado con el suelo
            float offsetY = (crouchHeight - originalSize.y) / 2;
            boxCollider.offset = new Vector2(originalOffset.x, originalOffset.y + offsetY);
        }
    }

    void ResetHeight()
    {
        if (boxCollider != null)
        {
            // Restaurar el tamaño y el offset original del collider
            boxCollider.size = originalSize;
            boxCollider.offset = originalOffset;
        }
    }

    void UpdateHealthBar()
    {
        // Actualizar el Fill Amount de la barra de vida
        healthBar.fillAmount = currentHealth / maxHealth;

        // Cambiar el color de la barra según la vida
        healthBar.color = healthGradient.Evaluate(healthBar.fillAmount);
    }

    public void TakeDamage(float damage)
    {
        if (isBlocking)
        {
            return;
        }

        currentHealth -= damage; // Reducir la vida
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegurar que no sea menor a 0

        UpdateHealthBar(); // Actualizar la barra de vida

        if (currentHealth <= 0)
        {
            Die(); // Llamar a un método para manejar la muerte del jugador
        }
    }

    void Die()
    {
        Debug.Log("¡Jugador derrotado!");

        SceneManager.LoadScene("FinalScene");
    }

    public void ResetPunch()
    {
        animator.ResetTrigger("Punch");
    }
}
