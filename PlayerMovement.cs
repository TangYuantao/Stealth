using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    /// <summary>
    /// 玩家喊叫的声音片段
    /// </summary>
    public AudioClip playerShoutingClip;
    /// <summary>
    /// 转向的速度
    /// </summary>
    public float turnSpeed=10;
    /// <summary>
    /// 横纵轴
    /// </summary>
    private float hor, ver;
    private bool sneak, shout;
    /// <summary>
    /// 玩家移动方向
    /// </summary>
    private Vector3 dir;
    /// <summary>
    /// 动画组件 
    /// </summary>
    private Animator ani;
    /// <summary>
    /// 声音组件
    /// </summary>
    private AudioSource aud;

    void Start()
    {
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
    }

    void Update()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        sneak = Input.GetButton("Sneak");
        shout = Input.GetButtonDown("Shout");
        //监听是否潜行
        ani.SetBool(HashIDs.Sneak, sneak);
        if (shout)
        {
            ani.SetTrigger(HashIDs.Shout);
        }
        //获取方向
        dir = new Vector3(hor, 0, ver);
        //玩家按下方向键 
        if (hor!=0 || ver!=0)
        {
            ani.SetFloat(HashIDs.Speed,5,.2f,Time.deltaTime);
            //获取方向
            dir = new Vector3(hor, 0, ver);
            //获取转向的一个目标 
            Quaternion turnTarget = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, turnTarget, Time.deltaTime * turnSpeed);
            //如果非潜行状态
            if (!sneak)
            {
                //播放脚步声音
                if (!aud.isPlaying)
                {
                    aud.Play();
                }
            }
            else
            {
                //停止脚步声音
                aud.Stop();
            }
        }else
        {
            //玩家停下来
            ani.SetFloat(HashIDs.Speed,0);
        }
        
    }

    /// <summary>
    /// 玩家喊叫帧事件 
    /// </summary>
    public void PlayerShouting()
    {
        AudioSource.PlayClipAtPoint(playerShoutingClip,transform.position);
    }
}
