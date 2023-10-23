using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchToMainScene : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    public void ToMain()
    {
        Debug.Log("Button is pressed");
        SceneManager.LoadScene("MainPlay");
    }
}
