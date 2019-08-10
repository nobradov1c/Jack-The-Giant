using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    private float minX, maxX;

    // Start is called before the first frame update
    void Start()
    {
        SetMinAndMaxX();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < minX)
        {
            Vector3 tmp = transform.position;
            tmp.x = minX;
            transform.position = tmp;
        } else if (transform.position.x > maxX)
        {
            Vector3 tmp = transform.position;
            tmp.x = maxX;
            transform.position = tmp;
        }
    }
    void SetMinAndMaxX()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        maxX = bounds.x;
        minX = -bounds.x;
    }

}
