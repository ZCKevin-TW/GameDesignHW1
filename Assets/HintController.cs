using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintController : MonoBehaviour
{
    private bool show = false;
    [SerializeField] private GameObject hintobject;

    // Update is called once per frame
    void Update()
    {
        if (hintobject != null)
            hintobject.active = show;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            show = false;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            show = true;
    }
}
