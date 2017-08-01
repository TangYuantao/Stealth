using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour {
    /// <summary>
    /// 动画Hash集合
    /// </summary>
    public static int Speed;
    public static int Sneak;
    public static int Shout;
    public static int DoorOpen;

    void Awake()
    {
        Speed = Animator.StringToHash("Speed");
        Sneak = Animator.StringToHash("Sneak");
        Shout = Animator.StringToHash("Shout");
        DoorOpen = Animator.StringToHash("DoorOpen");
    }
}
