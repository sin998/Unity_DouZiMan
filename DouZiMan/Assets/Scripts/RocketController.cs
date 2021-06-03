using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{

    //子弹在多少时间之后销毁
    public float destroyDelayTime = 2.0f;
    //爆炸效果预制体
    public GameObject ExplosionAnimation;

    // Start is called before the first frame update
    void Start()
    {
        //要是在destroyDelayTime之后还没有销毁就自动把自己销毁，不能子弹越来越多造成资源浪费。
        Destroy(gameObject,destroyDelayTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.collider.tag != "Player")
        //{
        //}
        Instantiate(ExplosionAnimation, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        if (collision.collider.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().Hurt();
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
