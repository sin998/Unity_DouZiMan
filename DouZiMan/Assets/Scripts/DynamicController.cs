using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicController : MonoBehaviour
{
    //现在的朝向
    public bool isFaceRight = true;
    //每几秒往前走
    private float moveTime;
    //几秒以后调换朝向
    private float flipTime;

    // Start is called before the first frame update
    void Start()
    {
        moveTime = 0.2f; 
        flipTime = moveTime * 50;
        StartCoroutine(move());
    }

    private IEnumerator move()
    {
        // 第一次时间间隔
        yield return new WaitForSeconds(moveTime);

        if (isFaceRight)
        {
            this.transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        }
        else
        {
            this.transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }

        flipTime -= moveTime;
        //Debug.Log("flipTime:" + flipTime);
        if (flipTime <= 0)
        {
            flip();
            flipTime = moveTime * 50;
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
