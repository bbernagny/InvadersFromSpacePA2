using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPickUps : MonoBehaviour
{
    public int seconds;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, seconds);
    }
}
