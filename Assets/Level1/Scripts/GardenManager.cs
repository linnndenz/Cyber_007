using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//相机初始x = 0，最大20.73f
public class GardenManager : MonoBehaviour
{
    public Transform backfar;
    float initBackfar;
    public Transform backnear;
    float initBacknear;

    private Transform player;
    private Transform mainCam;

    void Start()
    {
        player = Player.Instance.transform;
        mainCam = Camera.main.transform;

        initBackfar = backfar.position.x;
        initBacknear = backnear.position.x;
    }

    void LateUpdate()
    {
        CamMove();
    }

    public void CamMove()
    {
        float x = player.position.x - (-0.8f);
        if (x < 0) x = 0.01f;
        if (x > 20.73f) x = 20.72f;
        mainCam.position = new Vector3(x, mainCam.position.y, mainCam.position.z);

        backfar.position = new Vector3(initBackfar + x * 0.5f, backfar.position.y, backfar.position.z);
        backnear.position = new Vector3(initBacknear + x * 0.7f, backnear.position.y, backnear.position.z);

    }
}
