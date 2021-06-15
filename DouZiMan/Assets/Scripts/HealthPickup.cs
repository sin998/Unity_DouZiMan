using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour
{
    public float healthBonus;               //加血量
    public AudioClip health;

    private PickupSpawner pickupSpawner;    //道具生成控制器
    private Animator anim;                  //动画
    private bool landed = false;            //是否落地

    void Awake()
    {
        anim = transform.root.GetComponent<Animator>();
        pickupSpawner = GameObject.Find("PickupSpawner").GetComponent<PickupSpawner>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        // 半空中被接着
        if (other.tag == "Player")
        {
            //Debug.Log("是hero！");
            //other.GetComponent<PlayerHealth>();
            PlayerHealth playerHealth = other.gameObject.transform.root.GetComponent<PlayerHealth>();
            GameObject.FindGameObjectWithTag("PickupSpawner").GetComponent<AudioSource>().clip = health;
            GameObject.FindGameObjectWithTag("PickupSpawner").GetComponent<AudioSource>().Play();

            // 加血
            playerHealth.health += healthBonus;
            if (playerHealth.health > 100)
                playerHealth.health = 100;

            //Debug.Log("当前hero血量是：" + playerHealth.health);

            // 更新血条.
            playerHealth.updateHealthBar();

            // 开启新协程
            pickupSpawner.StartCoroutine(pickupSpawner.DeliverPickup());

            // 销毁医疗包
            Destroy(transform.root.gameObject);

            
        }
        // 落在地面
        else if (other.tag == "ground" && !landed)
        {
            anim.SetTrigger("Land");

            transform.parent = null;
            gameObject.AddComponent<Rigidbody2D>();
            landed = true;
        }
    }
}
