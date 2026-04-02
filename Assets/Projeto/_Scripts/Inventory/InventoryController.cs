using System.Collections;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController instance;

    [Header("Inventory Animations Start")]
    [SerializeField] private GameObject container_inventory;

    private bool enabled_inventory;

    private bool inAction;

    void Awake()
    {
        instance = (instance == null) ? this : instance;
    }

    public void InventoryControll(){
        if(!inAction){
            enabled_inventory = !enabled_inventory;

            if(enabled_inventory) StartCoroutine(AnimatorOpenInventory());
            else StartCoroutine(AnimatorCloseInventory());
                
        }
    }

    IEnumerator AnimatorOpenInventory(){
        inAction = true;
        container_inventory.SetActive(enabled_inventory);
        yield return new WaitForSeconds(1f);
        inAction = false;
    }

    IEnumerator AnimatorCloseInventory(){
        inAction = true;
        container_inventory.GetComponent<Animator>().SetTrigger("close");
        yield return new WaitForSeconds(1f);
        container_inventory.SetActive(false);
        inAction = false;
    }
}
