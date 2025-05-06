using UnityEngine;
using UnityEngine.SceneManagement;

public class Select : MonoBehaviour
{
   
     public void SelectLevel(string cena){

            SceneManager.LoadScene(cena);

   }
}
