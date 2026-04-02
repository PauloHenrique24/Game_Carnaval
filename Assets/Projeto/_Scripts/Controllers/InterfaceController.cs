using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    public static InterfaceController ui;

    [Header("Open Itens Interface")]
    [SerializeField] private GameObject barra_estamina;
    [SerializeField] private GameObject volume_estamina;
    

    [Header("Controllers Componentes")]
    [SerializeField] private Image estamina_bar;

    void Awake()
    {
        ui = (ui == null) ? this : ui;
    }

    public void Open_BarraEstamina(){
        barra_estamina.SetActive(true);
    }

    public void Canseira_Open(){
        volume_estamina.SetActive(true);
    }

    public void Close_BarraEstamina(){
        StartCoroutine(CloseAnimatorEstamina());
    }

    IEnumerator CloseAnimatorEstamina(){
        if(volume_estamina.activeSelf){
            volume_estamina.GetComponent<Animator>().SetTrigger("close");
        }
        barra_estamina.GetComponent<Animator>().SetTrigger("close");
        yield return new WaitForSeconds(1f);
        barra_estamina.SetActive(false);
        volume_estamina.SetActive(false);
    }


    public void BarEstamina(ref float estamina){
        estamina_bar.fillAmount = estamina / 100;
    }
}
