using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class intro : MonoBehaviour
{
    fade fade;
    void Start()
    {
        fade = FindObjectOfType<fade>();
        Invoke("stopvideo",70f);
    }

    void stopvideo()
    {
        fade.fadein();
        Invoke("loadmysore", 1f);
    }

    void loadmysore()
    {
        SceneManager.LoadSceneAsync("misore");
    }
}
