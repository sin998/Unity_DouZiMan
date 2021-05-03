using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    //相机：平滑过渡X
    public float SmoothX = 5.0f;
    //相机：平滑过渡Y
    public float SmoothY = 5.0f;
    //相机：摄像机里离hero的距离达到x后开始移动
    public float xMargin = 2.0f;
    //相机：摄像机里离hero的距离达到y后开始移动
    public float yMargin = 2.0f;
    //相机：Camera的XY的最小值：此值应该是在左上角，符号为负，XY值为相机实际的宽高的一半
    public Vector2 MinCamXandY = new Vector2(-100.0f, -100.0f);
    //相机：Camera的XY的最大值
    public Vector2 MaxCamXandY = new Vector2(100.0f, 100.0f);

    public Transform player;
    // Start is called before the first frame update
    private void Awake()
    {
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
    bool NeedMoveX()
    {
        return Mathf.Abs(transform.position.x - player.transform.position.x) > xMargin;
    }
    bool NeedMoveY()
    {
        return Mathf.Abs(transform.position.y - player.transform.position.y) > yMargin;
    }
    void TrackPlayer()
    {
        float fTargetX = transform.position.x;
        float fTargety = transform.position.y;
        if (NeedMoveX())
        {
            fTargetX = Mathf.Lerp(transform.position.x, player.transform.position.x,
                                        Time.deltaTime * SmoothX);
            fTargetX = Mathf.Clamp(fTargetX, MinCamXandY.x, MaxCamXandY.x);
        }
        if (NeedMoveY())
        {
            fTargety = Mathf.Lerp(transform.position.y, player.transform.position.y,
                                        Time.deltaTime * SmoothY);
            fTargety = Mathf.Clamp(fTargety, MinCamXandY.y, MaxCamXandY.y);
        }

        transform.position = new Vector3(fTargetX, fTargety, transform.position.z);
    }
}
