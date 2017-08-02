using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlinking : MonoBehaviour {
    /// <summary>
    /// 计时器
    /// </summary>
    public float timer;
    private BoxCollider boxCollider;
    private Light lt;
    private MeshRenderer meshRender;
    private AudioSource aud;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        lt = GetComponent<Light>();
        meshRender = GetComponent<MeshRenderer>();
        aud = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer>=2f)
        {
            boxCollider.enabled = !boxCollider.enabled;
            lt.enabled = !lt.enabled;
            meshRender.enabled = !meshRender.enabled;
            aud.enabled = !aud.enabled;
            timer = 0;
        }
    }

}
