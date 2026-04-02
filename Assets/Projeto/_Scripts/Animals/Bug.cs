using UnityEngine;

public class Bug : MonoBehaviour
{
    [Header("Movimentação")]
    private float speed;
    private float speedWalk;
    private float speedRun;

    public bool right;
    
    private Transform player_;

    void Start()
    {
        player_ = FindFirstObjectByType<Player_Controller>().transform;

        speedWalk = Random.Range(.5f,2f);
        speedRun = 4;
        if(transform.position.y < player_.position.y){
            GetComponent<SpriteRenderer>().sortingOrder = 3;
        }else{
            GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
    }

    void Update()
    {
        if(Vector3.Distance(transform.position,player_.position) < 2f){
            if(player_.gameObject.GetComponent<Player_Controller>().animations > 0){
                speed = speedRun;
                if(player_.position.x > transform.position.x){
                    right = false;
                }else{
                    right = true;
                }
            }
        }else{
            speed = speedWalk;
        }

        if(right){
            transform.position += transform.right * speed * Time.deltaTime;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else{
            transform.position += -transform.right * speed * Time.deltaTime;
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(right){
            if(collision.CompareTag("EndBug_Right")){
                Destroy(gameObject);
            }
        }else{
            if(collision.CompareTag("EndBug_Left")){
                Destroy(gameObject);
            }
        }
    }
}
