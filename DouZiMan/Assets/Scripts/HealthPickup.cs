using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour
{
    public float healthBonus;               // How much health the crate gives the player.
    public AudioClip boom;

    private PickupSpawner pickupSpawner;    // Reference to the pickup spawner.
    private Animator anim;                  // Reference to the animator component.
    private bool landed = false;                    // Whether or not the crate has landed.


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
            //other.GetComponent<PlayerHealth>();
            PlayerHealth playerHealth = other.gameObject.transform.root.GetComponent<PlayerHealth>();

            GameObject.FindGameObjectWithTag("PickupSpawner").GetComponent<AudioSource>().clip = boom;
            GameObject.FindGameObjectWithTag("PickupSpawner").GetComponent<AudioSource>().Play();

            // 加血
            playerHealth.health += healthBonus;
            if (playerHealth.health > 100)
                playerHealth.health = 100;

            // 更新血条.
            playerHealth.updateHealthBar();

            // 销毁医疗包
            Destroy(transform.root.gameObject);
            // 开启新协程
            pickupSpawner.StartCoroutine(pickupSpawner.DeliverPickup());

            
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
