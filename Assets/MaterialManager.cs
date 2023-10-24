using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Platformer.Gameplay;
//using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Mechanics;


public class MaterialManager : MonoBehaviour
{
    [SerializeField] private Renderer[] allrd;
    private Material[] old_mat;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private Sprite newBackground;
    private Sprite oldbackground;
    [SerializeField] private GameObject[] keys;
    [SerializeField] private Material target;
    [SerializeField] private Camera maincam;
    private CinemachineCameraShaker sk;
    private PlayerController player;
    public Transform tp;
    // Start is called before the first frame update
    void Start()
    {
        sk = GetComponent<CinemachineCameraShaker>();
        player = GameObject.FindObjectOfType<PlayerController>();
        old_mat = new Material[allrd.Length];
        for (int i = 0; i < allrd.Length; ++i)
            old_mat[i] = allrd[i].material;
        foreach (var k in keys)
            k.SetActive(false);
        oldbackground = background.sprite;
    }
    // Update is called once per frame
    public void reveal()
    {
        foreach (var rd in allrd)
        {
            rd.material = target; 
        }

        foreach (var k in keys)
            k.SetActive(true);

        background.sprite = newBackground;
        sk.ShakeCamera(1);
        player.Teleport(tp.position);
    }
    public void hide()
    {
        for (int i = 0; i < allrd.Length; ++i)
            allrd[i].material = old_mat[i];
        foreach (var k in keys)
            k.SetActive(false);
        background.sprite = oldbackground;
        sk.ShakeCamera(1);
        player.Teleport(tp.position);
    }
}
