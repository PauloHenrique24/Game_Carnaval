using UnityEngine;

[CreateAssetMenu(fileName = "Item Inventory",menuName = "Inventario/Item")]
public class Item : ScriptableObject
{
    public string nome;
    public Sprite icone;
    
    [Header("O Item Acumula?")]
    public bool acumulativo;

    [Range(2,32)]
    public int max;
}
