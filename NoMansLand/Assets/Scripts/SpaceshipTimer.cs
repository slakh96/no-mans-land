using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipTimer : MonoBehaviour
{
    private float time = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0f)
        {
            Destroy(gameObject);
        }
        time -= Time.deltaTime;
    }
}
