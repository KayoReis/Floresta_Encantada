using UnityEngine;

public class Gens : MonoBehaviour
{

    public bool go = false;

    public Transform hud;

    [SerializeField] private int Score = 1;
    public GameController gameController;

    public Vector2 initialposition;
    void Start()
    {
        initialposition = transform.position;
    }
    void Update()
    {
        if(go){
            transform.position = Vector2.MoveTowards(transform.position, hud.position, 20* Time.deltaTime);

            if(Vector2.Distance(transform.position, hud.position)<0.01){
                 gameObject.SetActive(false);
                 gameController.AddPoints(Score);

            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
          

            go = true;

            
            
        }
    }
}
