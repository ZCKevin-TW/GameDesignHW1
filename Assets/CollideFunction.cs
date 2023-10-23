using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideFunction : MonoBehaviour
{
    // Start is called before the first frame update
    private bool visited;
    [SerializeField] private MaterialManager toggler;
    void Start()
    {
        visited = false; 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == false) return;
        visited = !visited;
        if (visited)
            toggler.reveal();
        else
            toggler.hide();
    }

    // Update is called once per frame
}
