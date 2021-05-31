using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetKiller : MonoBehaviour
{

    public GameObject splash;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(splash, collision.transform.position, transform.rotation);
        Destroy(collision.gameObject);

    }
}
