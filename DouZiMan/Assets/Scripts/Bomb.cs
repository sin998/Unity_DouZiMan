using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float bombRadius = 10f;  // 杀伤范围
    public float bombForce = 100f;  // 冲击力
    public float fuseTime = 1.5f;   // 引线时间
    public GameObject explosion;    // 爆炸背景圆
    public AudioClip boom;

    private LayBombs layBombs;              // Hero脚本
    private PickupSpawner pickupSpawner;    // 道具生成脚本，启动新协程用
    private ParticleSystem explosionFX;     // 爆炸粒子效果

    void Awake()
    {
        explosionFX = this.transform.GetComponentInChildren<ParticleSystem>();
        pickupSpawner = GameObject.Find("PickupSpawner").GetComponent<PickupSpawner>();
        layBombs = GameObject.FindGameObjectWithTag("Player").GetComponent<LayBombs>();
    }

    void Start()
    {
        if (transform.root == transform)
            StartCoroutine(BombDetonation());
    }

    IEnumerator BombDetonation()
    {
        // 等待两秒，用于播放引信燃烧效果
        yield return new WaitForSeconds(fuseTime);

        Explode();
    }


    public void Explode()
    {
        layBombs.bombLaid = false; // Hero可再次释放Bomb，如何修改为可连续释放Bomb？

        //pickupSpawner.StartCoroutine(pickupSpawner.DeliverPickup());    // 启动产生新道具协程

        // 在杀伤范围内查找敌人
        int nLayer = 1 << LayerMask.NameToLayer("Enemy");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, bombRadius, nLayer);

        foreach (Collider2D en in enemies)
        {
            Rigidbody2D enemyBody = en.GetComponent<Rigidbody2D>();
            if (enemyBody != null && enemyBody.tag == "Enemy")
            {
                //enemyBody.gameObject.GetComponent<EnemyController>().health = 0;
                enemyBody.gameObject.GetComponent<EnemyController>().Hurt();
                enemyBody.gameObject.GetComponent<EnemyController>().Hurt();

                Vector3 deltaPos = enemyBody.transform.position - transform.position;

                Vector3 force = deltaPos.normalized * bombForce;
                enemyBody.AddForce(force);
            }
            else if (enemyBody != null && enemyBody.tag == "Enemy2")
            {
                enemyBody.gameObject.GetComponent<EnemyController>().health = 0;
                enemyBody.gameObject.GetComponent<EnemyController>().Hurt();
                enemyBody.gameObject.GetComponent<EnemyController>().Hurt();

                Vector3 deltaPos = enemyBody.transform.position - transform.position;

                Vector3 force = deltaPos.normalized * bombForce;
                enemyBody.AddForce(force);
            }
        }

        GetComponent<AudioSource>().clip = boom;
        GetComponent<AudioSource>().Play();
        // 播放爆炸后粒子效果
        explosionFX.transform.position = transform.position;
        explosionFX.Play();

        // 实列化爆炸背景圆
        Instantiate(explosion, transform.position, Quaternion.identity);

        // 销毁Bomb
        Destroy(gameObject);
    }
}
