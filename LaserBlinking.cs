using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlinking : MonoBehaviour {

    /// <summary>
    /// 时间间隔
    /// </summary>
    public float interval = 2f;
    /// <summary>
    /// 计时器
    /// </summary>
    private float timer;

    private MeshRenderer meshRenderer;
    private AudioSource aud;
    private BoxCollider boxCollider;
    private Light lt;


    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        aud = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider>();
        lt = GetComponent<Light>();
    }
    void Update()
    {
        timer+= Time.deltaTime;
        if (timer>=interval)
        {
            meshRenderer.enabled = !meshRenderer.enabled;
            aud.enabled = !aud.enabled;
            boxCollider.enabled = !boxCollider.enabled;
            lt.enabled = !lt.enabled;
            timer = 0;
        }
    }
}
