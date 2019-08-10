using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesScript : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("DestroyColectable", 15f);
    }

    void DestroyColectable()
    {
        gameObject.SetActive(false);
    }
}
