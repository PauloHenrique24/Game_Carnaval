using Entities;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Controller : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] private float speedWalk;
    [SerializeField] private float speedRun;
    [SerializeField] private float speedJump;

    private float speed;

    private Animator anim;
    private Animator anim_shadow;

    private Rigidbody2D rb;

    [HideInInspector] public int animations;

    [Header("Flip")]
    private bool isFacing;

    [Header("Jump")]
    [SerializeField] private float range;
    private bool isGround;

    private float estamina = 100f;

    private bool estamina_close = true;
    private bool not_run;

    [Header("Collect Itens")]
    [SerializeField] private float rangeCollect;
    [SerializeField] private LayerMask layerItens;

    [Header("Canvas_Player")]
    [SerializeField] private GameObject canvas_Player;
    [SerializeField] private GameObject CollectItem_Interface;
 
    void Start()
    {
        anim = GetComponent<Animator>();
        anim_shadow = transform.GetChild(0).GetComponent<Animator>();
        
        rb = GetComponent<Rigidbody2D>();

        speed = speedWalk;
        estamina = 100f;
    }

    void LateUpdate()
    {
        Movimentacao();
        IsGround();

        Keyboards_Cliked();
        CollectItens();
    }

    public void Keyboards_Cliked(){
        if(Input.GetKeyDown(KeyCode.Space) && isGround){
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.Tab)){
            InventoryController.instance.InventoryControll();
        }
    }

    void Movimentacao(){
        Movimento movimento = new Movimento(speed);
        transform.position += movimento.Movimentar();

        if(movimento.Movimentar().x < 0 && !isFacing){
            Flip();
        }else if(movimento.Movimentar().x > 0 && isFacing){
            Flip();
        }

        // Animações
        if(movimento.Movimentar() != Vector3.zero){
            if(Input.GetKey(KeyCode.LeftShift) && !not_run){
                speed = speedRun;
                animations = 2;

                if(estamina > 0){
                    estamina -= Time.deltaTime * 35;
                }else{
                    not_run = true;
                    InterfaceController.ui.Canseira_Open();
                }

                InterfaceController.ui.Open_BarraEstamina();
            }else if(!Input.GetKey(KeyCode.LeftShift) || not_run){
                animations = 1;
                speed = speedWalk;

                if(estamina < 100){
                    estamina += Time.deltaTime * 10;
                    estamina_close = false;
                }else{
                    if(!estamina_close){
                        InterfaceController.ui.Close_BarraEstamina();
                        not_run = false;
                        estamina_close = true;
                    }
                }
            }
        }
        else{ 
            if(estamina < 100){
                estamina += Time.deltaTime * 15;
                estamina_close = false;
            }else{
                if(!estamina_close){
                    InterfaceController.ui.Close_BarraEstamina();
                    estamina_close = true;
                    not_run = false;
                }
            }
            animations = 0;
        }

        anim.SetBool("isGround",isGround);
        anim_shadow.SetBool("isGround",isGround);

        anim.SetInteger("transition",animations);
        anim_shadow.SetInteger("transition",animations);

        InterfaceController.ui.BarEstamina(ref estamina);
        estamina = Mathf.Clamp(estamina,0,100);
    }

    void Flip(){
        isFacing = !isFacing;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        var scaleCanva = canvas_Player.transform.localScale;
        scaleCanva.x *= -1;
        canvas_Player.transform.localScale = scaleCanva;
    }

    void Jump(){
        anim.SetTrigger("jump");
        anim_shadow.SetTrigger("jump");
        rb.AddForce(Vector2.up * speedJump,ForceMode2D.Impulse);
        isGround = false;
    }

    void IsGround(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.down,range);

        if(hit.collider && !hit.collider.CompareTag("Player")){
            isGround = true;
        }
        
        if(!hit.collider){
            isGround = false;
        }
    }

    void CollectItens(){
        var hit2D = Physics2D.OverlapCircleAll(transform.position,rangeCollect,layerItens);

        if(hit2D.Length > 0){
            foreach(var i in hit2D){
                if(i.GetComponent<ItemPrefab>() != null){
                    Sprite icone = i.GetComponent<ItemPrefab>().item.icone;
                    string nome = i.GetComponent<ItemPrefab>().item.nome;
                    int qtd = 1;
                    CollectItem_Interface.GetComponent<Interface_Collect_Item>().Style(icone,nome,qtd);
                    CollectItem_Interface.SetActive(true);

                    if(Input.GetKeyDown(KeyCode.E)){
                        i.GetComponent<ItemPrefab>().Collect();
                    }
                    break;
                }
            }

            
        }else{
            CollectItem_Interface.SetActive(false);
        }

        // CollectItem_Interface.SetActive(false);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 normalizedDirection = Vector2.down.normalized;
        Vector3 endPoint = transform.position + normalizedDirection * range;
        Gizmos.DrawLine(transform.position,endPoint);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangeCollect);
    }
}
