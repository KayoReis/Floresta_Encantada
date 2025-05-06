using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;

    public float minX = 3.76f;
    public float minY = -6.2f;

    public float maxY = 10.8f;

    public float maxX = 16.59f;


    private void Update()
    {
        transform.position = player.position + new Vector3(0,0,-10);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x,minX,maxX),Mathf.Clamp(transform.position.y,minY,maxY), transform.position.z);
    }


}
