using UnityEngine;
using UnityEngine.Tilemaps;



public class Secret : MonoBehaviour
{

    public GameObject ForestHeart;

    public ForestHeart forestHeartClass;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<TilemapRenderer>().enabled = false;

            if (forestHeartClass.Gotcha == false)
            {
                ForestHeart.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            GetComponent<TilemapRenderer>().enabled = true;

            if (ForestHeart.gameObject != null)
            {
                if(forestHeartClass.Gotcha) return;
                ForestHeart.gameObject.SetActive(false);
            }
        }
    }
}
