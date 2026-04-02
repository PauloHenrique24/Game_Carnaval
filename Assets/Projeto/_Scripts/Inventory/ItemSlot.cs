using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public int qtd;
    public Item item;

    public Image icone;
    public TextMeshProUGUI txt_qtd;

    public void AddQuant(){
        qtd++;
        txt_qtd.text = qtd.ToString("00");
    }

    public void StyleItem(string nome, Sprite icone){
        this.icone.sprite = icone;
        this.txt_qtd.text = "01";
    }
}
