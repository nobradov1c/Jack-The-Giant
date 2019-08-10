using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float speed = 1f;
    private float acceleration = 0.2f;
    private float maxSpeed = 3.2f;

    private float easySpeed = 3.2f;
    private float mediumSpeed = 3.7f;
    private float hardSpeed = 4.2f;

    [HideInInspector]
    public bool moveCamera;
    // Start is called before the first frame update
    void Start()
    {
        moveCamera = true;
        if (GamePreferences.GetEasyDifficulty() == 1)
            maxSpeed = easySpeed;
        else if (GamePreferences.GetMediumDifficulty() == 1)
            maxSpeed = mediumSpeed;
        else if (GamePreferences.GetHardDifficulty() == 1)
            maxSpeed = hardSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCamera)
            MoveCamera();
    }

    void MoveCamera()
    {
        Vector3 tmp = transform.position;

        float oldY = tmp.y;
        float newY = tmp.y - (speed * Time.deltaTime);
        tmp.y = Mathf.Clamp(tmp.y, oldY, newY);
        transform.position = tmp;
        speed += acceleration * Time.deltaTime;

        if (speed > maxSpeed)
            speed = maxSpeed;
    }
}
