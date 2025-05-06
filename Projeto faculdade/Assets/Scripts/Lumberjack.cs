using System;
using UnityEngine;
using UnityEngine.AI;

public class Lumberjack : MonoBehaviour
{

    public Personagem personagem;


    [SerializeField] private Transform head;
    public Transform A;
    public Transform B;

    public float tolerancia = 0.2f;

    public NavMeshAgent agent;

    private Transform finalDestination;

    public bool playerDead = false;

    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(gameObject != null){
        agent = GetComponent<NavMeshAgent>();
          agent.updateRotation = false;
        finalDestination = B;
        agent.SetDestination(finalDestination.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject != null){

            if(playerDead){
                agent.isStopped = true;
                return;
            }
          transform.rotation = Quaternion.identity;
        if(!agent.pathPending && agent.remainingDistance <= tolerancia){
            finalDestination = (finalDestination == A) ? B:A;
            agent.SetDestination(finalDestination.position);
            flipDirection();

        
        }
        }
    }

    void flipDirection(){
        Vector3 scale = transform.localScale;

        scale.x = (finalDestination == A)? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        transform.localScale = scale; 

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       if (gameObject != null){
        if(collision.gameObject.CompareTag("Player")){
            playerDead = true;
            personagem.animator.SetBool("Deathing", true);
            personagem.canJump = false;
            personagem.canMove = false;
            personagem.canGlideAgain = false;
            personagem.isGliding = false;
            personagem.rb.constraints = RigidbodyConstraints2D.FreezeAll;

        }
    }
    }

   public void Die()
    {
        if(gameObject != null){
       
        
            gameObject.SetActive(false);
            
            
        }
    }


}
