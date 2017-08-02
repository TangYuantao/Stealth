using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : MonoBehaviour {
    public AudioClip pickUpClip;
    private PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = GameObject.FindWithTag(Tags.Player).GetComponent<PlayerInventory>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            //播放声音
            AudioSource.PlayClipAtPoint(pickUpClip, transform.position);
            //给玩家设置标志位
            playerInventory.hasKey = true;
            Destroy(gameObject);
        }
    }
}
