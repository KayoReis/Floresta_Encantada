using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Levelmage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float zoomScale = 1.1f;
    public Vector3 originalscale;
 
     void Start()
    {
        originalscale = transform.localScale;
    }

   public void OnPointerEnter(PointerEventData eventData){
        transform.localScale = originalscale * zoomScale;
   }

   public void OnPointerExit(PointerEventData eventData){
        transform.localScale = originalscale;
   }
}
