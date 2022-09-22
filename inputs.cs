using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputs : MonoBehaviour
{
    [Header("Keyboard")]
    public string movx;//as
    public string movz;//ws
    public string arrows; //upanddownarrows
    public string jump; //space
    public string interact; //f
    public string attack; //j
    public string pause; //esc
    public string run; //shift

    [Header("Controller")]
    public string c_movx;//leftx
    public string c_movz;//righty
    public string c_arrows; //right
    public string c_jump; //circle
    public string c_interact; //triangle
    public string c_attack; //x
    public string c_pause; //pause
    public string c_run; //square
}
