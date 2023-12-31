using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintController : MonoBehaviour
{
    private bool show = false;
    //[SerializeField] private GameObject hintobject = this;

    Renderer[] rd;
    private void Awake()
    {
        rd = GetComponentsInChildren<Renderer>(); 
    }
    // Update is called once per frame
    void Update()
    {
        foreach(Renderer i in rd)
        { 
            i.enabled = show;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            show = false;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Hi");
        if (col.CompareTag("Player"))
            show = true;
    }
}
