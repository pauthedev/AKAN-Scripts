using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class graphmanager : MonoBehaviour
{
    Resolution[] res;
    public Dropdown drop;
    void Awake()
    {
        res = Screen.resolutions;//get all device resolution options
        drop.ClearOptions();

        List<string> options = new List<string>();

        int currentres = 0;

        for (int i=0; i <res.Length; i++)
        {
            string option = res[i].width + " x " + res[i].height;
            options.Add(option);//creates options for all resolutions in the dropdown

            if (res[i].width == Screen.currentResolution.width && res[i].height == Screen.currentResolution.height)
            {
                currentres = i;
            }
        }

        drop.AddOptions(options);
        drop.value = currentres;
        drop.RefreshShownValue();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetResolution(int resindex)
    {
        Resolution resolution = res[resindex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
