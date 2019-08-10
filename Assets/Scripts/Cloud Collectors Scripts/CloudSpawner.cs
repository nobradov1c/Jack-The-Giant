using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] clouds;

    private float distanceBetweenClouds = 3f;

    private float minX, maxX;

    private float lastCloudPositionY;

    private float controlX;

    [SerializeField]
    private GameObject[] collectables;

    private GameObject player;

    private void Awake()
    {
        controlX = 0f;
        SetMinAndMaxX();
        CreateClouds();
        player = GameObject.Find("Player");

        for(int i = 0; i < collectables.Length; i++)
        {
            collectables[i].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PositionThePlayer();
    }

    void SetMinAndMaxX()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        maxX = bounds.x - 0.6f;
        minX = -bounds.x + 0.6f;
    }

    void Shuffle(GameObject[] array)
    {
        for(int i = 0; i < array.Length; i++)
        {
            GameObject tmp = array[i];
            int random = Random.Range(1, array.Length-1);
            if(array[i].tag == "Deadly")
            {
                while(array[random-1].tag == "Deadly" || array[random + 1].tag == "Deadly")
                    random = Random.Range(1, array.Length - 1);
            }
            array[i] = array[random];
            array[random] = tmp;
        }
    }

    void CreateClouds()
    {
        Shuffle(clouds);

        float positionY = 0f;

        for(int i = 0; i < clouds.Length; i++)
        {
            Vector3 tmp = clouds[i].transform.position;

            tmp.y = positionY;
            if(controlX == 0)
            {
                tmp.x = Random.Range(0.0f, maxX);
                controlX = 1;
            } else if (controlX == 1)
            {
                tmp.x = Random.Range(minX, 0.0f);
                controlX = 2;
            } else if (controlX == 2)
            {
                tmp.x = Random.Range(1.0f, maxX);
                controlX = 3;
            } else if (controlX == 3)
            {
                tmp.x = Random.Range(minX, -1.0f);
                controlX = 0;
            }
            lastCloudPositionY = positionY;
            clouds[i].transform.position = tmp;
            positionY -= distanceBetweenClouds;
        }
    }

    void PositionThePlayer()
    {
        GameObject[] darkClouds = GameObject.FindGameObjectsWithTag("Deadly");
        GameObject[] whiteClouds= GameObject.FindGameObjectsWithTag("Cloud");

        for(int i = 0; i < darkClouds.Length; i++)
        {
            if(darkClouds[i].transform.position.y == 0f)
            {
                Vector3 temp = darkClouds[i].transform.position;
                darkClouds[i].transform.position = new Vector3(whiteClouds[0].transform.position.x, whiteClouds[0].transform.position.y, whiteClouds[0].transform.position.z);
                whiteClouds[0].transform.position = temp;
            }
        }

        Vector3 tmp = whiteClouds[0].transform.position;

        for(int i = 1; i < whiteClouds.Length; i++)
        {
            if (tmp.y < whiteClouds[i].transform.position.y)
            {
                tmp = whiteClouds[i].transform.position;
            }
        }

        tmp.y += 0.8f;

        player.transform.position = tmp;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Cloud" || target.tag == "Deadly")
        {
            if(target.transform.position.y == lastCloudPositionY)
            {
                Shuffle(clouds);
                Shuffle(collectables);

                Vector3 tmp = target.transform.position;
                for (int i = 0; i < clouds.Length; i++)
                {
                    if (!clouds[i].activeInHierarchy)
                    {
                        if (controlX == 0)
                        {
                            tmp.x = Random.Range(0.0f, maxX);
                            controlX = 1;
                        }
                        else if (controlX == 1)
                        {
                            tmp.x = Random.Range(minX, 0.0f);
                            controlX = 2;
                        }
                        else if (controlX == 2)
                        {
                            tmp.x = Random.Range(1.0f, maxX);
                            controlX = 3;
                        }
                        else if (controlX == 3)
                        {
                            tmp.x = Random.Range(minX, -1.0f);
                            controlX = 0;
                        }

                        tmp.y -= distanceBetweenClouds;
                        lastCloudPositionY = tmp.y;
                        clouds[i].transform.position = tmp;
                        clouds[i].SetActive(true);
                        int chance = Random.Range(1, 5);
                        if (chance < 3)
                        {
                            if (clouds[i].tag != "Deadly")
                            {
                                int random = Random.Range(0, collectables.Length);
                                while (collectables[random].activeInHierarchy)
                                {
                                    random = Random.Range(0, collectables.Length);
                                }
                                Vector3 tmp2 = clouds[i].transform.position;
                                tmp2.y += 0.7f;
                                if (collectables[random].tag == "Life")
                                {
                                    if (PlayerScore.lifeScore < 2)
                                    {
                                        collectables[random].transform.position = tmp2;
                                        collectables[random].SetActive(true);
                                    }
                                }
                                else
                                {
                                    collectables[random].transform.position = tmp2;
                                    collectables[random].SetActive(true);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
