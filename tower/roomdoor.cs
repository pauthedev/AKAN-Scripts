using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class roomdoor : MonoBehaviour
{
    public AddRoom thisroom;
    public bool closed;
    public Animator anim;
    public CinemachineVirtualCamera cam;
    AudioSource aud;
    public AudioClip door;
    void Start()
    {
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        cam = GameObject.FindWithTag("shake").GetComponent<CinemachineVirtualCamera>();
        thisroom = gameObject.GetComponentInParent(typeof(AddRoom)) as AddRoom;
    }

    void Update()
    {
        if (thisroom.isRoom && thisroom.fight && !closed)
        {

            Invoke("checkin", 0.2f);

        }

        if (thisroom.isRoom && !thisroom.fight && closed)
        {//battle start shake
            closed = false;
            aud.PlayOneShot(door);
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.7f;
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1f;
            Invoke("stopshake", 2f);
            anim.Play("door_out");
        }


    }

    void stopshake()
    {
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
    }


    void checkin()
    {
        if (thisroom.isRoom)
        {//battle end shake
            closed = true;
            aud.PlayOneShot(door);
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.7f;
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1f;
            Invoke("stopshake", 3f);
            anim.Play("door_in");
        }
        else
        {
            closed = false;
        }
    }

    void checkout()
    {

    }
}
