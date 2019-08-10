using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private PlayerMoveMobile playerMove;

    private void Start()
    {
        playerMove = GameObject.Find("Player").GetComponent<PlayerMoveMobile>();
    }
    public void OnPointerDown(PointerEventData data)
    {
        if (gameObject.name == "Button Left")
        {
            playerMove.SetMoveLeft(true);
        } else if (gameObject.name == "Button Right")
        {
            playerMove.SetMoveLeft(false);
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        playerMove.StopMoving();
    }
}
