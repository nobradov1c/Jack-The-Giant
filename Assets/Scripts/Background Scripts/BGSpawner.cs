using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpawner : MonoBehaviour
{
    private GameObject[] backgrounds;

    private float lastY;
    // Start is called before the first frame update
    void Start()
    {
        GetBackgroundsAndSetLastY();
    }
    void GetBackgroundsAndSetLastY()
    {
        backgrounds = GameObject.FindGameObjectsWithTag("Background");

        lastY = backgrounds[0].transform.position.y;
        for(int i = 1; i < backgrounds.Length; i++)
        {
            if(lastY > backgrounds[i].transform.position.y)
            {
                lastY = backgrounds[i].transform.position.y;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Background")
        {
            if(collision.transform.position.y == lastY)
            {
                Vector3 tmp = collision.transform.position;
                float height = ((BoxCollider2D)collision).size.y;

                for(int i = 0; i < backgrounds.Length; i++)
                {
                    if (!backgrounds[i].activeInHierarchy)
                    {
                        tmp.y -= height;
                        lastY = tmp.y;
                        backgrounds[i].transform.position = tmp;
                        backgrounds[i].SetActive(true);
                    }
                }
            }
        }
    }
}
