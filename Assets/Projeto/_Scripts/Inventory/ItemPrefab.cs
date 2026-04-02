using UnityEngine;

public class ItemPrefab : MonoBehaviour
{
    public Item item;

    public void Collect(){
        if(InventoryManager.manager.GenerateItem(item)){
            Destroy(gameObject);
        }
    }

}
