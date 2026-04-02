using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Interface_Collect_Item : MonoBehaviour
{
    [SerializeField] private Image icone;
    [SerializeField] private TextMeshProUGUI nome_;
    [SerializeField] private TextMeshProUGUI qtd_;

    public void Style(Sprite icone, string nome, int qtd){
        this.icone.sprite = icone;
        nome_.text = nome;
        qtd_.text = qtd.ToString("00");
    }
}
