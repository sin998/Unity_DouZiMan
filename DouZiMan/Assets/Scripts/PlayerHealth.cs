using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //最大血量
    public float maxHealth = 100.0f;
    //英雄血量
    public float health = 100.0f;
    //每次扣血量
    private float damage = 20.0f;
    //血条的渲染器
    public SpriteRenderer healthBarRender;
    //玩家动画控制器，死亡、掉落动画。
    public Animator playerAnimatior;
    //反弹的力的大小
    public float hurtForce = 100f;
    //声音控制
    private AudioSource audioSource;
    //受伤声音控制
    public AudioClip[] hurtClips;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    //英雄和其他东西碰撞时触发
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("血量是：" + health);
        //碰到敌人
        if (collision.collider.tag == "Enemy")
        {
            //设置声音片段
            audioSource.clip = hurtClips[Random.Range(0, hurtClips.Length - 1)];
            //播放声音
            audioSource.Play();
            //英雄碰到敌人时反弹
            Vector3 hurtVector = transform.position - collision.gameObject.transform.position + Vector3.up;
            this.GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);

            //血>0
            if (health > 0)
            {
                takeDamage();
                updateHealthBar();
            }
            //死亡
            else
            {
                //脚本失活，使脚本无效
                this.GetComponent<PlayerControl>().enabled = false;

                //刚体禁用
                Rigidbody2D rigidbody2D = this.GetComponent<Rigidbody2D>();
                rigidbody2D.freezeRotation = true;
                rigidbody2D.velocity = Vector3.zero;

                //状态机:播放死亡动画
                this.GetComponent<Animator>().SetTrigger("Die");

                //子物体的碰撞全部设置为IsTrigger，即掉到河里
                Collider2D[] collider2Ds = this.GetComponentsInChildren<Collider2D>();
                foreach( Collider2D c in collider2Ds){
                    c.isTrigger = true;
                }

            }

        }

    }

    //扣血
    private void takeDamage()
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        Vector3 hurtVector = transform.position - this.transform.position + Vector3.up * 5f;
        GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);
    }

    //更新血条
    public void updateHealthBar()
    {
        //颜色插值。100（绿） <-> 0（红）
        healthBarRender.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
        //血条缩放比例
        healthBarRender.transform.localScale = new Vector3(healthBarRender.transform.localScale.x * health * 0.01f, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
