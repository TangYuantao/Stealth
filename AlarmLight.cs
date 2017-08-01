using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLight : MonoBehaviour {
    /// <summary>
    /// 警报灯开关
    /// </summary>
    public bool alarmOn;
    /// <summary>
    /// 警报灯闪烁速度
    /// </summary>
    public float turnSpeed = 3f;
    /// <summary>
    /// 灯光强
    /// </summary>
    private float highIntencity = 4f;
    /// <summary>
    /// 低光强
    /// </summary>
    private float lowIntencity = 0;
    /// <summary>
    /// 目标光强
    /// </summary>
    private float targetIntencity;
    /// <summary>
    /// 警报灯
    /// </summary>
    private Light alarmLight;

    void Awake()
    {
        alarmLight = GetComponent<Light>();
    }

    void Start()
    {
        //初始目标：高光强
        targetIntencity = highIntencity;
    }

    void Update()
    {
        //如果开启警报
        if (alarmOn)
        {
            //接近目标
            alarmLight.intensity = Mathf.Lerp(alarmLight.intensity,
                targetIntencity,Time.deltaTime*turnSpeed);
            //如果到达目标
            if (Mathf.Abs(alarmLight.intensity-targetIntencity)<.05f)
            {
                //如果当前光强是高光强，则下一个光强为低光强
                if (targetIntencity==highIntencity)
                {
                    targetIntencity = lowIntencity;
                }else
                {   
                    //反之，下一个目标为高光强
                    targetIntencity = highIntencity;
                }

            }
        }else
        {
            //渐变到低光强
            alarmLight.intensity = Mathf.Lerp(alarmLight.intensity,lowIntencity
                ,Time.deltaTime*turnSpeed);
            //如果到达目标，停止渐变
            if ((alarmLight.intensity-lowIntencity)<.05f)
            {
                alarmLight.intensity = lowIntencity;
            }
        }
    }
}
