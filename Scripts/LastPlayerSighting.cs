using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlayerSighting : MonoBehaviour {
    /// <summary>
    /// 非报警位置
    /// </summary>
    public Vector3 normalPosition = new Vector3(1000,1000,1000);
    /// <summary>
    /// 报警位置
    /// </summary>
    public Vector3 alarmPosition = new Vector3(1000, 1000, 1000);
    /// <summary>
    /// 警报灯脚本
    /// </summary>
    private AlarmLight alarmLight;
    /// <summary>
    /// 默认背景音乐
    /// </summary>
    private AudioSource normalMusic;
    /// <summary>
    /// 紧张背景音乐1
    /// </summary>
    private AudioSource panicMusic;
    /// <summary>
    /// 喇叭音效
    /// </summary>
    private AudioSource[] sirenAud;
    
    void Awake()
    {
        alarmLight = GameObject.FindWithTag(Tags.AlarmLight).GetComponent<AlarmLight>();
        normalMusic = GetComponent<AudioSource>();
        panicMusic = transform.GetChild(0).GetComponent<AudioSource>();
        GameObject[] sirnObjs = GameObject.FindGameObjectsWithTag(Tags.Siren);
        sirenAud = new AudioSource[sirnObjs.Length];
        for (int i=0;i<sirnObjs.Length;i++)
        {
            sirenAud[i] = sirnObjs[i].GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (alarmPosition!=normalPosition)
        {
            alarmLight.alarmOn = true;
            normalMusic.volume = Mathf.Lerp(normalMusic.volume, 0, Time.deltaTime * alarmLight.turnSpeed);
            panicMusic.volume = Mathf.Lerp(panicMusic.volume, 1, Time.deltaTime * alarmLight.turnSpeed);
            for (int i=0;i<sirenAud.Length;i++)
            {
                sirenAud[i].volume = Mathf.Lerp(sirenAud[i].volume, 1, Time.deltaTime * alarmLight.turnSpeed);
            }
        }
        //警报关闭
        else
        {
            alarmLight.alarmOn = false;
            normalMusic.volume = Mathf.Lerp(normalMusic.volume, 1, Time.deltaTime * alarmLight.turnSpeed);
            panicMusic.volume = Mathf.Lerp(panicMusic.volume, 0, Time.deltaTime * alarmLight.turnSpeed);
            for (int i = 0; i < sirenAud.Length; i++)
            {
                sirenAud[i].volume = Mathf.Lerp(sirenAud[i].volume, 0, Time.deltaTime * alarmLight.turnSpeed);
            }
        }
    }
}
