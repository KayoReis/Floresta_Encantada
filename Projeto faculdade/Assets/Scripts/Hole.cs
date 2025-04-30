using UnityEngine;

public class Hole : MonoBehaviour
{
   public GameController gameController;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            gameController.DeathReset();
        }
    }
}
