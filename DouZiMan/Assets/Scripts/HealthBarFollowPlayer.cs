using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollowPlayer : MonoBehaviour
{
    private Transform PlayerTf;

    public Vector3 offset = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        PlayerTf = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = PlayerTf.position + offset;
    }
}
