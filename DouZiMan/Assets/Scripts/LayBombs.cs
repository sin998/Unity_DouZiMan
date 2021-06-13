using UnityEngine;
using System.Collections;

public class LayBombs : MonoBehaviour
{
    [HideInInspector]
    public bool bombLaid = false;		// 释放了炸弹.
    public GameObject bomb;             // Prefab of the bomb.
    public int bombCount = 0;           // Hero有多少个炸弹.

    void Update()
    {

        if (Input.GetButtonDown("Fire2") && !bombLaid && bombCount > 0)
        {
            bombCount--;
            bombLaid = true;
            Instantiate(bomb, transform.position, transform.rotation);
        }
    }
}
