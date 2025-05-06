using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 5;
    public bool platformX , platformY;
    public bool moveRigth = true;

    public bool moveUp = true;

    // Update is called once per frame
    void Update()
    {
        if(platformX){
            if(transform.position.x > 5 ){
                moveRigth =false;
            }else if(transform.position.x < -5){
                moveRigth = true;
            }

            if(moveRigth){
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }else{
                transform.Translate(Vector2.right * -moveSpeed * Time.deltaTime);
            }
        }
        if(platformY){
            if(transform.position.y > 4 ){
                moveUp =false;
            }else if(transform.position.y < -4){
                moveUp = true;
            }

            if(moveUp){
                transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
            }else{
                transform.Translate(Vector2.up * -moveSpeed * Time.deltaTime);
            }
        }
    }
}
