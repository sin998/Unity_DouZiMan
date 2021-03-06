using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    //角色的刚体
    private Rigidbody2D heroRD2D;
    //角色的heroTransform
    private Transform heroTransform;
    //施加的力：移动
    public float moveForce = 100;
    //现在的朝向
    public bool isFaceRight = true;
    //移动：左右
    private float h = 0.0f;
    //最大速度
    public float maxSpeed = 20;
    //是否在地面上
    private bool beGrounded = false;
    //Ground的heroTransform
    private Transform GroundCheckTf;
    //跳跃力量的大小
    public float JumpForce = 500;
    //能否跳跃
    private bool canJump = false;
    //Hero的动画控制器
    public Animator playeraimator;
    //跳跃声音控制
    public AudioClip[] jumpClips;
    //声音控制
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        heroRD2D = this.GetComponent<Rigidbody2D>();
        GroundCheckTf = heroTransform.Find("GroundCheck");
        audioSource = this.GetComponent<AudioSource>();
    }
    void Awake()
    {
        //heroTransform = GameObject.FindGameObjectWithTag("Player").transform; 
        heroTransform = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //获取英雄的heroTransform，这里能用this是因为这个脚本就是挂在英雄上面的
        heroTransform = this.gameObject.transform;

        //获取水平方向的输入：AD/← →
        h = Input.GetAxis("Horizontal");

        //动画:Run
        playeraimator.SetFloat("Speed", Mathf.Abs(h));

        //移动
        move();

        //跳跃
        //射线检测
        beGrounded = Physics2D.Linecast(heroTransform.position, GroundCheckTf.position, 1 << LayerMask.NameToLayer("Ground"));
        //检测按键
        if (beGrounded)
        {
            playeraimator.SetBool("IsJump", false);
            canJump = true;
            //Debug.Log("在地上！");
            if (Input.GetButtonDown("Jump"))
            {
                //Debug.Log("开始跳跃！");
                //动画:Jump
                jump();
            }
        }
        else
        {
            playeraimator.SetBool("IsJump", true);
        }
    }

    private void FixedUpdate()
    {
        //转身
        //速度大于0（向右的力）并且现在朝向左，所以要转身
        if (h > 0 && !isFaceRight)
        {
            flip();
        }
        //速度小于0（向左的力）并且现在朝向右，所以要转身
        if (h < 0 && isFaceRight)
        {
            flip();
        }
    }

    //跳跃
    private void jump()
    {
        if (canJump)
        {
            //设置声音片段
            audioSource.clip = jumpClips[Random.Range(0, jumpClips.Length - 1)];
            //播放声音
            audioSource.Play();
            //Debug.Log("可以跳跃！");
            heroRD2D.AddForce(new Vector2(0, 1) * JumpForce);
            canJump = false;
        }
    }

    //移动
    private void move()
    {
        //控制移动
        if (Mathf.Abs(heroRD2D.velocity.x) < maxSpeed)
        {
            heroRD2D.AddForce(Vector2.right * h * moveForce);
        }
        //控制最高速度
        if (Mathf.Abs(heroRD2D.velocity.x) > maxSpeed)
        {
            heroRD2D.velocity = new Vector2(Mathf.Sign(heroRD2D.velocity.x) * maxSpeed, heroRD2D.velocity.y);
        }
    }

    //转身
    private void flip()
    {
        //改变标志位
        isFaceRight = !isFaceRight;
        //得到当前的转身
        Vector3 v3LocalScale = heroTransform.localScale;
        //转身
        v3LocalScale.x *= -1;
        //设置
        heroTransform.localScale = v3LocalScale;
    }

}
