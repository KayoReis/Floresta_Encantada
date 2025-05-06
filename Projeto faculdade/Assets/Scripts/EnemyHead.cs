using UnityEngine;

public class EnemyHead : MonoBehaviour
{
   [SerializeField] private Lumberjack parent;

   private void OnTriggerEnter2D(Collider2D other){
    if(other.CompareTag("Feet")){
        parent.animator.SetBool("Dying",true);
          Collider2D[] colliders = parent.GetComponents<Collider2D>();
        foreach (var col in colliders)
        {
            col.enabled = false;
        }
    }
   }

}
