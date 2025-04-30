//CONLUÍDO
using UnityEngine;
using System.Collections;
public class Personagem : MonoBehaviour
{
    public GameController gameController;

    public Rigidbody2D rb;
    [SerializeField] private int velocidade = 6;

    [SerializeField] private Transform Feet;
    [SerializeField] private LayerMask floorLayer;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private int turningleftHash = Animator.StringToHash("TurningLeft");
    private int walkingHash = Animator.StringToHash("Walking");
    private int jumpstartHash = Animator.StringToHash("JumpStart");
    private int jumploopHash = Animator.StringToHash("JumpLoop");
    private int jumppeakHash = Animator.StringToHash("JumpPeak");
    private int turningrigthHash = Animator.StringToHash("TurningRigth");
    
    private int falling1StartgHash = Animator.StringToHash("Fall1Start");

    private int RunningHash = Animator.StringToHash("Runing");

    private bool inFlorr;

    public bool facingright = true;

    private bool Isturning;

    public bool isjumping;
    public float jumpspeed = 8f;
    public float turningDuration = 0.2f;

    public float turningTimer;
    public float counterJump = 0.4f;



    public bool isRunning = false;
    public bool Ismoving = false;

    public bool canJump = true;

    public bool canGlideAgain = true;

    public bool isGliding = false;

    private int glideHash = Animator.StringToHash("Gliding");

    public bool canMove = true;

    public Vector2 InitialPosition;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        InitialPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        isRunning = VerificarCorrida();
        // Inputs


        /* Virar para direita */
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && facingright == false && canMove)
        {
            facingright = true;
            spriteRenderer.flipX = false;
            if (inFlorr && !Ismoving && !isRunning)

            {
                Isturning = true;
                animator.SetBool(turningrigthHash, true);
                turningTimer = turningDuration;
                StartCoroutine(Nudge(Vector3.right, 0.5f, 0.31f));
            }
            else if (isRunning && !Ismoving)
            {
                StartCoroutine(Nudge(Vector3.right, 0.1f, 0.05f));
            }
        }
        /* Andar para direita enquanto a animação de virar não está ativa*/
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !Isturning && !isRunning && canMove)
        {
            transform.position += new Vector3(1 * velocidade * Time.deltaTime, 0, 0);

            if (inFlorr)
            {
                animator.SetBool(walkingHash, true);
                Ismoving = true;

            }else{animator.SetBool(walkingHash, false);}
        }
        /* Para de andar e parar com a animação */
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (inFlorr)
            {
                if (Ismoving)
                {
                    animator.CrossFade("Idle", 0.05f);
                }
                animator.SetBool(walkingHash, false);
            }
        }
        //Mesa coisa porém para esquerda
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) && canMove)
        {

            if (facingright == true)
            {
                if (inFlorr && !Ismoving && !isRunning)
                {
                    Isturning = true;
                    animator.SetBool(turningleftHash, true);
                    turningTimer = turningDuration;
                    StartCoroutine(Nudge(Vector3.left, 0.5f, 0.31f));
                }
                else
                {
                    spriteRenderer.flipX = true;
                    if (isRunning && !Ismoving)
                    {
                        StartCoroutine(Nudge(Vector3.left, 0.1f, 0.05f));
                    }
                }

                facingright = false;
            }

        }
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !Isturning && !isRunning && canMove)
        {
            transform.position -= new Vector3(1 * velocidade * Time.deltaTime, 0, 0);

            if (inFlorr)

            {
                animator.SetBool(walkingHash, true);
                Ismoving = true;
            }else{animator.SetBool(walkingHash, false);}
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (inFlorr)
            {
                if (Ismoving)
                {
                    animator.CrossFade("Idle", 0.05f);
                }
                animator.SetBool(walkingHash, false);
            }
        }
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            animator.SetBool(walkingHash, false);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            StartCoroutine(EsperarParar());
        }
        // Pulo



        /* Verificar se o personagem está no chão*/
        inFlorr = Physics2D.OverlapCircle(Feet.position, 0.2f, floorLayer);
        /* Iniciar o Pulo */
        if (Input.GetKeyDown(KeyCode.Space) && inFlorr && canJump)
        {
            isRunning = false;
            Ismoving = false;
            canJump = false;
            isjumping = true;
            animator.SetBool("Iniciopulo", true);
            animator.SetBool(jumploopHash, false);
            animator.SetBool(jumppeakHash, false);

            // Inicia troca após pequeno delay (duração da JumpStart)
            StartCoroutine(AtivarJumpLoop());


        }
        /*Controlar a altura do pulo segurando o butão */
        if (Input.GetKey(KeyCode.Space) && isjumping)
        {

            counterJump -= Time.deltaTime;
            if (counterJump > 0)
            {
                animator.SetBool(jumploopHash, true);

            }
            else
            {
                isjumping = false;
                animator.SetBool(jumploopHash, false);
                animator.SetBool(jumppeakHash, true);
            }
        }
        /* Encerrar o Pulo*/
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isjumping = false;
            counterJump = 0.4f;
            animator.SetBool("Iniciopulo", false);
            animator.SetBool(jumploopHash, false);
            animator.SetBool(jumppeakHash, true);
        }

        if (inFlorr && !isjumping)
        {
            animator.SetBool(jumppeakHash, false);
            animator.SetBool(jumploopHash, false);
            animator.SetBool("Iniciopulo", false);

        }

        bool isFalling = !isjumping && !inFlorr;
        bool Grounded = inFlorr && !isjumping;

        if (Grounded)
        {
            animator.SetBool("JumpLoop", false);
        }


        animator.SetBool(falling1StartgHash, isFalling);
        animator.SetBool("Infloor", Grounded);



        //CORRER

        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && canMove)
        {
            transform.position += new Vector3(1.3f * velocidade * Time.deltaTime, 0, 0);
            Debug.Log(Ismoving);

            animator.SetBool(walkingHash, false);
            animator.SetBool(RunningHash, true);

        }

        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && canMove)
        {
            transform.position -= new Vector3(1.3f * velocidade * Time.deltaTime, 0, 0);



            animator.SetBool(walkingHash, false);
            animator.SetBool(RunningHash, true);
        }

        if (!Input.GetKey(KeyCode.LeftShift) || (!TemInputHorizontal()))
        {



            StartCoroutine(VerificarCorridaAposDelay());

        }

        //Glide

        if (isFalling && Input.GetKey(KeyCode.Space) && !isGliding && canGlideAgain)
        {

            isGliding = true;
            animator.SetBool(glideHash, true);

        }
        else if (!Input.GetKey(KeyCode.Space) && isGliding)
        {
            EndGlide();
            animator.SetBool(glideHash, false);
        }

        //Timer para animação de trocar de lado
        if (Isturning)
        {
            turningTimer -= Time.deltaTime;
            if (turningTimer <= 0f)
            {
                Isturning = false;
                animator.SetBool(turningrigthHash, false);
                animator.SetBool(turningleftHash, false);

                if (!facingright)
                {

                    spriteRenderer.flipX = true;

                }
                else
                {

                }

            }
        }

        //Troca das animações para Pula, Queda e Aterrisagem

        IEnumerator Nudge(Vector3 direction, float distance, float duration)
        {
            Vector3 start = transform.position;
            Vector3 target = start + direction * distance;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                transform.position = Vector3.Lerp(start, target, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = target;
        }

        IEnumerator EsperarParar()
        {
            yield return new WaitForSeconds(0.1f);

            //Em movimento
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                Ismoving = false;

            }

        }
        IEnumerator AtivarJumpLoop()
        {
            yield return new WaitForSeconds(0.5f); // tempo da animação JumpStart

            if (isjumping)
            {
                animator.SetBool("Iniciopulo", false);
                animator.SetBool(jumploopHash, true);
            }
        }

        if (isRunning)
        {
            animator.SetBool(walkingHash, false);
            Ismoving = false;
        }

        bool VerificarCorrida()
        {
            return Input.GetKey(KeyCode.LeftShift) &&
                   (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ||
                    Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow));


        }

        bool TemInputHorizontal()
        {
            return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ||
                   Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        }
        IEnumerator VerificarCorridaAposDelay()
        {
            yield return new WaitForSeconds(0.3f); // aproximadamente 2 frames (a 60fps)

            if (!Input.GetKey(KeyCode.LeftShift) || !TemInputHorizontal())
            {
                animator.SetBool(RunningHash, false);
            }
        }
    }

    public void BloquearMovimento()
    {

        canMove = false;
    }

    public void DesbloquearMovimento()
    {

        canMove = true;
    }

    public void OnLand()
    {
        canJump = true;
        isjumping = false;
        canGlideAgain = true;
        if (inFlorr && !isjumping && !canJump)
        {
            Debug.Log("Fallback liberou pulo (possível falha do evento)");
            canJump = true;
            canGlideAgain = true;
        }
    }

    public void EndGlide()
    {
        isGliding = false;
        canGlideAgain = false;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

    }

    public void OnStartFalling()
    {

        animator.SetBool(walkingHash, false);
        animator.SetBool(RunningHash, false);
    }
    public void onDeath (){

        gameController.DeathReset();

    }
    void FixedUpdate()
    {
        if (animator.GetBool(jumploopHash))
        {
            if (counterJump > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpspeed);

            }
        }

        if (isGliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -0.8f);
        }
    }
}
