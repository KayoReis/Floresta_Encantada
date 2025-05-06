
using UnityEngine;

public class ForestHeart : MonoBehaviour
{
    public GameController gameController;
    public bool Gotcha = false;

public Transform Hud;
  

    public Vector2 InitialPosition;

    void Start()
    {
        InitialPosition = transform.position;
        gameObject.SetActive(false);

    }

    void Update()
    {
        if(Gotcha){
             transform.position = Vector2.MoveTowards(transform.position, Hud.position, 20* Time.deltaTime);
              if(Vector2.Distance(transform.position, Hud.position)<0.01){
                 gameObject.SetActive(false);
                 gameController.CatchForestHeart();

            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
 

            Gotcha = true;




        }
    }
}
