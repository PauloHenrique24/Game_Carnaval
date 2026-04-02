using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager manager;

    public List<Slot> slots = new List<Slot>();
    public Transform target_slot;

    [Header("Item no Inventario")]
    [SerializeField] private GameObject item_prefab;

    void Awake()
    {
        for(int i = 0; i < target_slot.childCount;i++){
            if(target_slot.GetChild(i).GetComponent<Slot>()){
                slots.Add(target_slot.GetChild(i).GetComponent<Slot>());
            }
        }

        manager = (manager == null) ? this : manager;
    }

    public bool GenerateItem(Item item){
        if(item.acumulativo){
            //Pode existir outros no inventario para agrupar com ele
            foreach(Slot sl in slots){
                if(sl.full && sl.item.item == item && sl.item.qtd < item.max){
                    AddItem(sl);
                    return true;
                }
            }

            return CreateItem(item);
        }else{
            return CreateItem(item);
        }
    }

    void AddItem(Slot slot){
        slot.item.AddQuant();
    }
    
    bool CreateItem(Item item){
        foreach(Slot sl in slots){
            if(!sl.full){
                //Cria um novo item no slot vazio
                ItemSlot itemSlot = Instantiate(item_prefab,sl.transform).GetComponent<ItemSlot>();
                
                itemSlot.StyleItem(item.nome,item.icone);
                itemSlot.qtd = 1;
                itemSlot.item = item;

                sl.full = true;
                sl.item = itemSlot;
                return true;
            }
        }
        return false;
    }   
}
