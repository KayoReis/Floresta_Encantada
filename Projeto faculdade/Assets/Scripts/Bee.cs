
using UnityEngine;

public class Bee : MonoBehaviour
{
    private Rigidbody2D rb;
   public Animator animator;
   public SpriteRenderer spriteRenderer;

public Personagem personagem;
   public Vector3 initialposition;

   public Vector3 Lastposition;

   public Vector3 direction;

    void Start()
    {
        if (gameObject != null){
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialposition = transform.position;
        Lastposition = transform.position;
        }
    }

       void Update()
    {
        if (gameObject != null){
        transform.position = Vector2.MoveTowards(transform.position,personagem.transform.position, 1.5f* Time.deltaTime); 

        direction = transform.position - Lastposition;

        if(direction.x > 0){
            spriteRenderer.flipX = true;
        }else if(direction.x < 0){
            spriteRenderer.flipX = false;
        }

        Lastposition = transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject != null){
        if(collision.CompareTag("Player")){
            
            personagem.animator.SetBool("Deathing", true);
            personagem.canJump = false;
            personagem.canMove = false;
            personagem.canGlideAgain = false;
            personagem.isGliding = false;
            personagem.rb.constraints = RigidbodyConstraints2D.FreezeAll;

        }
    }
    }
}
