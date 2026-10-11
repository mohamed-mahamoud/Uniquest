using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    [Header("Déplacement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Animation (optionnel)")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private Vector2 input;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;                       
        rb.freezeRotation = true;                   
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void Update()
    {
        // Lecture des touches ZQSD (clavier AZERTY)
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.W)) y += 1f;   // Z
        if (Input.GetKey(KeyCode.S)) y -= 1f;   // S
        if (Input.GetKey(KeyCode.D)) x += 1f;   // D
        if (Input.GetKey(KeyCode.A)) x -= 1f;   // Q

        // Normalisation : évite d'aller plus vite en diagonale
        input = new Vector2(x, y).normalized;

        // Retourner le sprite selon la direction horizontale
        if (spriteRenderer != null && x != 0f)
            spriteRenderer.flipX = x < 0f;

        // Animation (paramètres à créer dans l'Animator)
        if (animator != null)
        {
            animator.SetFloat("Speed", input.sqrMagnitude);
            animator.SetFloat("Horizontal", input.x);
            animator.SetFloat("Vertical", input.y);
        }
    }

    private void FixedUpdate()
    {
        // Déplacement via la physique pour respecter les collisions
        rb.linearVelocity = input * moveSpeed;    
    }
}