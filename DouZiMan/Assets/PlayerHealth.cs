using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //最大血量
    public float maxHealth = 100.0f;
    //英雄血量
    private float health = 100.0f;
    //每次扣血量
    private float damage = 20.0f;
    //血条的渲染器
    public SpriteRenderer healthBarRender;
    //玩家动画控制器，死亡、掉落动画。
    public Animator playerAnimatior;

    // Start is called before the first frame update
    void Start()
    {

    }

    //英雄和其他东西碰撞时触发
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("血量是：" + health);
        //碰到敌人
        if (collision.collider.tag == "Enemy")
        {
            //Debug.Log("碰撞了！");
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
    }

    //更新血条
    private void updateHealthBar()
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
