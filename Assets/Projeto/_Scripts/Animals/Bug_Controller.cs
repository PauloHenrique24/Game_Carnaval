using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug_Controller : MonoBehaviour
{
    [Header("Instanciar Bugs")]
    [SerializeField] private float min_timer;
    [SerializeField] private float max_timer;

    [Space]
    [SerializeField] private Transform min_target;
    [SerializeField] private Transform max_target;

    [Space]
    [SerializeField] private List<GameObject> bugs;

    private float timer;
    private float y;
    private int bug;

    private bool GenerateBug;

    public bool right;

    void Start()
    {
        RandomBug();
    }

    void Update()
    {
        if(GenerateBug){
            RandomBug();
        }
    }

    IEnumerator InstantBug(float timer, Vector3 pos,int bug){
        GenerateBug = false;
        yield return new WaitForSeconds(timer);
        var bug_ = Instantiate(bugs[bug],pos,Quaternion.identity);

        bug_.GetComponent<Bug>().right = right;
        GenerateBug = true;
    }

    void RandomBug(){
        timer = Random.Range(min_timer,max_timer);
        y = Random.Range(min_target.position.y,max_target.position.y);
        bug = Random.Range(0,bugs.Count);

        Vector3 pos = new Vector3(min_target.position.x,y);

        StartCoroutine(InstantBug(timer,pos,bug));
    }
}
