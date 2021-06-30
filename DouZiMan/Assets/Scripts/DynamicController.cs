using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicController : MonoBehaviour
{
    //现在的朝向
    public bool isFaceRight = true;
    //每几秒往前走
    private float moveIntervalTime;
    //几秒以后调换朝向
    private float flipTime;
    //施加的力：移动
    public float moveForce = 100;
    //刚体
    private Rigidbody2D rigidbody2D;
    //是否在运动
    private bool isMove = false;
    //上一次施加的力
    private Vector2 lastMoveForce;
    //运动总时间
    public float moveTotalTime;

    // Start is called before the first frame update
    void Start()
    {
        moveIntervalTime = 0.2f;
        flipTime = moveIntervalTime * moveTotalTime;
        StartCoroutine(move());
        rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    private IEnumerator move()
    {
        // 第一次时间间隔
        yield return new WaitForSeconds(moveIntervalTime);

        if (isMove == false)
        {
            if (isFaceRight)
            {
                lastMoveForce = Vector2.right * moveForce * (-1);
                rigidbody2D.AddForce(lastMoveForce);
            }
        else
            {
                lastMoveForce = Vector2.right * moveForce * (1);
                rigidbody2D.AddForce(lastMoveForce);
            }
            isMove = true;
        }

        flipTime -= moveIntervalTime;
        if (flipTime <= 0.0f)
        {
            flip();
            flipTime = moveIntervalTime * moveTotalTime;
            rigidbody2D.AddForce( (-1) * lastMoveForce);
            isMove = false;
        }

        StartCoroutine(move());
    }

    private void flip()
    {
        //改变标志位
        isFaceRight = !isFaceRight;
        //得到当前的转身
        Vector3 v3LocalScale = this.transform.localScale;
        //转身
        v3LocalScale.x *= -1;
        //设置
        transform.localScale = v3LocalScale;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
