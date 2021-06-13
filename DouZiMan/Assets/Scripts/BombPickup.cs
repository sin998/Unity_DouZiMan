using UnityEngine;
using System.Collections;

public class BombPickup : MonoBehaviour
{
    public AudioClip boom;
    private PickupSpawner pickupSpawner;
    private Animator anim;              
    private bool landed = false;        
    
    void Awake()
    {
        anim = transform.root.GetComponent<Animator>();
        pickupSpawner = GameObject.Find("PickupSpawner").GetComponent<PickupSpawner>();
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
       
        // 炸弹还在半空被接住
        if (other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("PickupSpawner").GetComponent<AudioSource>().clip = boom;
            GameObject.FindGameObjectWithTag("PickupSpawner").GetComponent<AudioSource>().Play();
            // 销毁炮弹
            Destroy(transform.root.gameObject);
            pickupSpawner.StartCoroutine(pickupSpawner.DeliverPickup());
            //增加炮弹数:这里碰撞的其实是body
            //Debug.Log(other.gameObject.name);
            other.gameObject.transform.root.GetComponent<LayBombs>().bombCount++;
        }
        // 掉地上
        else if (other.tag == "Ground" && !landed)
        {
            anim.SetTrigger("Land");
            transform.parent = null;
            //gameObject.AddComponent<Rigidbody2D>();
            landed = true;
        }
    }
}
