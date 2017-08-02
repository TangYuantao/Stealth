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
    /// 高光强
    /// </summary>
    private float hightIntensity = 7f;
    /// <summary>
    /// 低光强
    /// </summary>
    private float lowIntensity = 0f;
    /// <summary>
    /// 目标光强
    /// </summary>
    private float targetIntensity;
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
        targetIntensity = hightIntensity;
    }

    void Update()
    {
        if (alarmOn)
        {
            alarmLight.intensity = Mathf.Lerp(alarmLight.intensity, targetIntensity, Time.deltaTime * turnSpeed);
            if (Mathf.Abs(targetIntensity-alarmLight.intensity)<.05f)
            {
                if (targetIntensity==hightIntensity)
                {
                    targetIntensity = lowIntensity;
                }else
                {
                    targetIntensity = hightIntensity;
                }
            }
        }else
        {
            alarmLight.intensity = Mathf.Lerp(alarmLight.intensity,lowIntensity,Time.deltaTime*turnSpeed);
            if ((alarmLight.intensity-lowIntensity)<.05f)
            {
                targetIntensity = lowIntensity;
            }
        }
    }
}
