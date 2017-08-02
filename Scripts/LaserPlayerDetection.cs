using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPlayerDetection : MonoBehaviour
{
    private LastPlayerSighting lastPlayerSighting;

    void Awake()
    {
        //获取警报脚本
        lastPlayerSighting = GameObject.FindWithTag(Tags.GameController).
            GetComponent<LastPlayerSighting>();
    }

    void OnTriggerEnter(Collider other)
    {
        //如果是玩家
        if (other.CompareTag(Tags.Player))
        {
            //赋值警报位置
            lastPlayerSighting.alarmPosition = other.transform.position;
        }
    }
}
