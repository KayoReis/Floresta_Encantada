using UnityEngine;




public class Secret2 : MonoBehaviour
{

    public GameObject ForestHeart;

    public ForestHeart forestHeartClass;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

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

            if (ForestHeart.gameObject != null)
            {
                if(forestHeartClass.Gotcha) return;
                ForestHeart.gameObject.SetActive(false);
            }
        }
    }
}
