using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleDoorAnimation : MonoBehaviour {
    /// <summary>
    /// 触发器内的人数
    /// </summary>
    private int count = 0;

    private Animator ani;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        if (count>0)
        {
            //开门
            ani.SetBool(HashIDs.DoorOpen,true);
        }else
        {
            //关门
            ani.SetBool(HashIDs.DoorOpen, false);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player) || other.CompareTag(Tags.Enemy))
        {
            count++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.Player) || other.CompareTag(Tags.Enemy))
        {
            count--;
        }
    }
}
