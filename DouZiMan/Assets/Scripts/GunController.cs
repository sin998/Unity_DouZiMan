using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    /*
        发射导弹
        添加Gun空物体位于Bazooka前端
        为Gun添加脚本控制发射和播放声音

        制作导弹碰撞物体后爆炸效果
        爆炸效果为动画
        添加脚本控制爆炸时动画的旋转角度
     */

    //火箭预制体
    public Rigidbody2D rocket;
    //火箭飞行速度
    public float flySpeed;
    //玩家控制器脚本
    private PlayerControl playerControl;

    // Start is called before the first frame update
    void Start()
    {
        //此脚本挂载到Gun上，而Gun是Hero的一个子物体（枪口），所以transform代表this.transform
        //即自身。而transform.root则是返回此层次结构中的最层的transform，即Hero的transform
        //最后再用Hero的transform找下面的组件即可。
        playerControl = transform.root.GetComponent<PlayerControl>();
        //或者可以写为：找父亲（即Hero）的transform下面的组件，效果相同。
        //playerControl = transform.parent.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        //这个是检测按键，和PlayerControl中的检测跳跃（空格触发）一样，它传一个字符串（即映射），
        //它的按键映射和对应键盘的键值可以在「Edit」-「Project Setting」-「Input Manager」中设置。
        if (Input.GetButtonDown("Fire1"))
        {
            //判断朝向
            if (playerControl.isFaceRight)
            {
                //这里使用的是Instantiate其中一个重载。需要一个要生成的物体（刚体）、生成的位置、旋转。
                //rocket是预制体。
                //前面说了Gun是Hero的子物体，而它是一个空物体，仅仅用来占位，初始化导弹的位置，即这里的transform就是Gun的transform。
                //屏幕锚点默认是在中心，屏幕中心向右是X方向，向下是Y方向，垂直屏幕中心向内是Z方向，所以人物朝右正好就是X正方向，此时无需旋转。
                Rigidbody2D bullentInstantRD2D = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                //设置子弹飞行速度。分别是X轴、Y轴速度的方向。
                bullentInstantRD2D.velocity = new Vector2(flySpeed, 0);
            }
            else
            {
                Rigidbody2D bullentInstantRD2D = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                bullentInstantRD2D.velocity = new Vector2(-flySpeed, 0);
            }
        }

        //还有另一种方式是直接对按键进行监听，不做映射。按下空格键：
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    //TODO:Do something...
        //}
        //两者各有优缺，推荐使用第一种方式，因为要是在很多地方都需要按键检测，那么如果万一按键变化只需要改映射即可。

    }
}
