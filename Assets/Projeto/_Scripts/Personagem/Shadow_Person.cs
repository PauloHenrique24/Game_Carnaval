using UnityEngine;

public class Shadow_Person : MonoBehaviour
{
    private bool isGround;
    private Vector3 floor;

    void Update()
    {
        if(isGround){
            var pos = transform.localPosition;
            pos.y = floor.y;
            transform.position = pos;
        }

        var player = FindFirstObjectByType<Player_Controller>();
        var posX = transform.position;
        posX.x = player.transform.position.x;
        transform.position = posX;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("floor")){
            isGround = true;
            floor = collision.transform.position;
        }else{
            isGround = false;
            floor = Vector3.zero;
        }
    }
}
