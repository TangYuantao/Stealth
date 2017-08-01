using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlayerSighting : MonoBehaviour {
    /// <summary>
    /// 非警报位置
    /// </summary>
    public Vector3 normalPosition = new Vector3(1000, 1000, 1000);
    /// <summary>
    /// 警报位置
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
    /// 紧张背景音乐
    /// </summary>
    private AudioSource panicMusic;
    /// <summary>
    /// 喇叭音效
    /// </summary>
    private AudioSource[] sirenAud;

    void Start()
    {
        alarmLight = GameObject.FindWithTag(Tags.AlarmLight).GetComponent<AlarmLight>();
        normalMusic = GetComponent<AudioSource>();
        //panicMusic = GetComponentInChildren<AudioSource>();
        panicMusic = transform.GetChild(0).GetComponent<AudioSource>();
        //先找到6个喇叭对象
        GameObject[] sirenObjs = GameObject.FindGameObjectsWithTag(Tags.Siren);
        //初始化
        sirenAud = new AudioSource[sirenObjs.Length];
        //逐个填充
        for (int i=0;i<sirenObjs.Length;i++)
        {
            sirenAud[i] = sirenObjs[i].GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        //警报开启
        if (alarmPosition!=normalPosition)
        {
            //打开警报
            alarmLight.alarmOn = true;
            //正常背景音乐关闭
            normalMusic.volume = Mathf.Lerp(normalMusic.volume, 0, Time.deltaTime * alarmLight.turnSpeed);
            //紧张背景音乐开启
            panicMusic.volume = Mathf.Lerp(panicMusic.volume, 1, Time.deltaTime * alarmLight.turnSpeed);
            //开启喇叭声音
            for (int i=0;i<sirenAud.Length;i++)
            {
                sirenAud[i].volume = Mathf.Lerp(sirenAud[i].volume,1,Time.deltaTime*alarmLight.turnSpeed);
            }
        }
        //警报关闭
        else
        {
            //关闭警报
            alarmLight.alarmOn = false;
            //正常背景音乐开启
            normalMusic.volume = Mathf.Lerp(normalMusic.volume, 1, Time.deltaTime * alarmLight.turnSpeed);
            //紧张背景音乐关闭
            panicMusic.volume = Mathf.Lerp(panicMusic.volume, 0, Time.deltaTime * alarmLight.turnSpeed);
            //关闭喇叭声音
            for (int i = 0; i < sirenAud.Length; i++)
            {
                sirenAud[i].volume = Mathf.Lerp(sirenAud[i].volume, 0, Time.deltaTime * alarmLight.turnSpeed);
            }
        }
    }

}
