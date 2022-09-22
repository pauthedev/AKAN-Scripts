using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class lookatcontroller : MonoBehaviour
{
    public Transform lookaim;
    public Transform target;
    public GameObject baseposition;
    public bool follow;
    public MultiAimConstraint multiAim;

    void Update()
    {
        float weight = (target == null) ? 0 : 1f;
        Vector3 pos = (target == null) ? transform.position + transform.forward + Vector3.up : target.position + Vector3.up;
//smooth follow of the character with the head
        multiAim.weight = Mathf.Lerp(multiAim.weight, weight, .05f);
        lookaim.position = Vector3.Lerp(target.position, pos, .3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
            follow = true;
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;
            follow = false;
        }
            
    }
}
