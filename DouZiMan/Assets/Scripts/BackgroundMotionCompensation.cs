using UnityEngine;

public class BackgroundMotionCompensation : MonoBehaviour
{
    //主摄像机的tf
    Transform CamMainTF;
    //主摄像机上一帧的位置
    Vector3 CamMainPrePos;
    //相机运动量
    public float fparallax;
    //要运动的背景
    public Transform[] backgroundsTF;
    //每层要运动的系数
    public float layerFraction = 5f;
    //平滑量
    public float fSmooth = 0.05f;

    private void Awake()
    {
        CamMainTF = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        //CamMainTF = Camera.main.transform;
        CamMainPrePos = CamMainTF.position;
    }

    // Update is called once per frame
    void Update()
    {
        //相机运动量
        float fparallaxX = (CamMainPrePos.x - CamMainTF.position.x) * fparallax;
        //各层背景运动量：越近的移动的应该越多
        for (int i = 0; i < backgroundsTF.Length; i++)
        {
            //计算各层背景新位置
            float bkNewX = backgroundsTF[i].position.x + fparallaxX * (1 + layerFraction * i);
            //计算新位置
            Vector3 newPosV3 = new Vector3(bkNewX, backgroundsTF[i].position.y, backgroundsTF[i].position.z);
            //相机平滑移动，用Lerp函数，Unity专门提供了针对于V3的Lerp
            backgroundsTF[i].position = Vector3.Lerp(backgroundsTF[i].position, newPosV3, fSmooth * Time.deltaTime);
        }
        //保存本帧相机位置
        CamMainPrePos = CamMainTF.position;
    }
}
