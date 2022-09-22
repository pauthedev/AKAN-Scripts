using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class congrats : MonoBehaviour
{
    dialogueui ui;

    bool animstart;
    bool gotext;
    public CanvasGroup canv;
    public fade faded;
    public AudioMixer audiomix;
    void Start()
    {
        audiomix.SetFloat("Master", PlayerPrefs.GetFloat("masterVolume"));
        audiomix.SetFloat("Music", PlayerPrefs.GetFloat("musicVolume"));
        audiomix.SetFloat("FX", PlayerPrefs.GetFloat("fxVolume"));

        ui = FindObjectOfType<dialogueui>();
        faded = GetComponent<fade>();
        faded.fadeout();
        Invoke("startdialogue", 4f);
    }

    void Update()
    {
        if (animstart && !ui.inDialogue)
        {
            if (canv.alpha < 1)
            {
                canv.alpha += Time.deltaTime*0.4f;
            }
            else
            {
                if (!gotext)
                {
                    Invoke("endanim", 10f);
                    gotext = true;
                }
            }
        }
    }

    void startdialogue()
    {
	//restart the dialogue system
        ui.ClearText();
        ui.SetCharNameAndColor();
        Invoke("enabler", 1f);
    }

    void enabler()
    {
        animstart = true;
    }

    void loadtitle()
    {
        SceneManager.LoadSceneAsync("title");
    }

    public void endanim()
    {
        faded.fadein();
        Invoke("loadtitle", 1f);
    }
}
