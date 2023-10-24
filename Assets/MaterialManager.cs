using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    [SerializeField] private Renderer[] allrd;
    private Material[] old_mat;
    [SerializeField] private GameObject[] keys;
    [SerializeField] private Material target;
    [SerializeField] private Camera maincam;

    // Start is called before the first frame update
    void Start()
    {
        old_mat = new Material[allrd.Length];
        for (int i = 0; i < allrd.Length; ++i)
            old_mat[i] = allrd[i].material;
        foreach (var k in keys)
            k.SetActive(false);
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Debug.Log("Shake it");
        Vector3 oldPos = maincam.transform.localPosition;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float xdiff = Random.Range(-.5f, .5f) * magnitude;
            float ydiff = Random.Range(-.5f, .5f) * magnitude;
            maincam.transform.localPosition = oldPos + new Vector3(xdiff, ydiff, 0f);
            elapsedTime += Time.deltaTime;
            Debug.Log(ydiff);
            Debug.Log(maincam.transform.localPosition);
            yield return null;
        }
        Debug.Log("End shake");
        maincam.transform.localPosition = oldPos;
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
        StartCoroutine(Shake(1, 10));
    }
    public void hide()
    {
        for (int i = 0; i < allrd.Length; ++i)
            allrd[i].material = old_mat[i];
        foreach (var k in keys)
            k.SetActive(false);
    }
}
