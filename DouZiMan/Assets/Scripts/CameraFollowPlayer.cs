using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    //相机：平滑过渡X
    public float SmoothX = 5.0f;
    //相机：平滑过渡Y
    public float SmoothY = 5.0f;
    //相机：摄像机里离hero的距离达到xMargin后开始移动
    public float xMargin = 2.0f;
    //相机：摄像机里离hero的距离达到yMargin后开始移动
    public float yMargin = 2.0f;
    //相机：Camera的XY的最小值：此值应该是在左上角，符号为负，XY值为相机实际的宽高的一半
    public Vector2 MinCamXandY;
    //相机：Camera的XY的最大值
    public Vector2 MaxCamXandY;
    //玩家的TF
    public Transform player;

    // Start is called before the first frame update
    private void Awake()
    {
        //找到玩家
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TrackPlayer();
    }

    //是否需要移动X
    bool NeedMoveX()
    {
        return Mathf.Abs(transform.position.x - player.transform.position.x) > xMargin;
    }

    //是否需要移动Y
    bool NeedMoveY()
    {
        return Mathf.Abs(transform.position.y - player.transform.position.y) > yMargin;
    }

    //摄像机追踪玩家
    void TrackPlayer()
    {
        //目标XY
        float fTargetX = transform.position.x;
        float fTargety = transform.position.y;
        //判断X方向是否需要移动
        if (NeedMoveX())
        {
            //直接设置位置，不过要用Lerp平滑
            fTargetX = Mathf.Lerp(transform.position.x, player.transform.position.x,
                                        Time.deltaTime * SmoothX);
            //把摄像机限定到X范围内
            fTargetX = Mathf.Clamp(fTargetX, MinCamXandY.x, MaxCamXandY.x);
        }
        if (NeedMoveY())
        {
            fTargety = Mathf.Lerp(transform.position.y, player.transform.position.y,
                                        Time.deltaTime * SmoothY);
            fTargety = Mathf.Clamp(fTargety, MinCamXandY.y, MaxCamXandY.y);
        }
        //设置TF
        transform.position = new Vector3(fTargetX, fTargety, transform.position.z);
    }
}
