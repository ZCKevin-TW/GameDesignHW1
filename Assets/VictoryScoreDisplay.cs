using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class VictoryScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    private TMP_Text Display;
    [SerializeField] DataHolder data;
    void Start()
    {
        Display = GetComponent<TMP_Text>();
        Display.SetText("Final Score: " + data.Score.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
