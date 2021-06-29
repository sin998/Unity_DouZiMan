using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //移动速度
    public float moveSpeed = 20.0f;
    //怪物的ri2D
    private Rigidbody2D enemyRiB2D;
    //FrontCheck的TF
    private Transform frontCheckTF;
    //怪物血量
    public float health = 2.0f;
    //怪物最大血量
    private float maxHealth = 2.0f;
    //怪物半血SP
    public Sprite damageSprite;
    //怪物死亡SP
    public Sprite deadSprite;
    //怪物body
    private SpriteRenderer bodySP;
    //伤害量
    private float damageVal = 1.0f;
    //是否死亡
    //private bool isDead;
    //100Points动画
    public GameObject PointsUI100;
    //受伤声音控制
    public AudioClip[] deathClips;
    //被打了几枪
    private int attackCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemyRiB2D = this.GetComponentInChildren<Rigidbody2D>();
        frontCheckTF = this.transform.Find("FrontCheck");
        bodySP = this.transform.Find("body").GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        //水平匀速运动
        enemyRiB2D.velocity = new Vector2(this.transform.localScale.x * moveSpeed, enemyRiB2D.velocity.y);
        //转身
        Collider2D[] collider2Ds = Physics2D.OverlapPointAll(frontCheckTF.position);
        foreach(Collider2D c in collider2Ds)
        {
            if(c.tag == "Wall" || c.tag =="Enemy")
            {
                flip();
                break;
            }
        }
    }

    public void addAttackCount()
    {
        attackCount++;
        if(attackCount == 2)
        {
            GameObject.Find("Canvas").GetComponent<UIController>().addScore();
        }
    }

    public void Hurt()
    {
        health -= damageVal;
        health = Mathf.Clamp(health, 0, maxHealth);
        if(health <= maxHealth / 2)
        {
            if (damageSprite != null)
            {
                bodySP.sprite = damageSprite;
            }
        }
        if(health <= 0)
        {
            Dead();
        }
    }    
    
    
    public void Dead()
    {
        GameObject.FindGameObjectWithTag("PickupSpawner").GetComponent<AudioSource>().clip = deathClips[Random.Range(0, deathClips.Length)];
        GameObject.FindGameObjectWithTag("PickupSpawner").GetComponent<AudioSource>().Play();
        // isDead = true;
        bodySP.sprite = deadSprite;
        //子物体的碰撞全部设置为IsTrigger，即掉到河里
        Collider2D[] collider2Ds = this.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D c in collider2Ds)
        {
            c.isTrigger = true;
        }
        //取消冻结
        enemyRiB2D.freezeRotation = false;
        //随机加上一个旋转扭矩
        Vector3 scorePos = transform.position;
        scorePos.y += 1.5f;

        Instantiate(PointsUI100, scorePos, Quaternion.identity);
        Destroy(gameObject);
    }

    //转身
    private void flip()
    {
        //得到当前的转身
        Vector3 v3LocalScale = this.transform.localScale;
        //转身
        v3LocalScale.x *= -1;
        //设置
        this.transform.localScale = v3LocalScale;
    }
}
