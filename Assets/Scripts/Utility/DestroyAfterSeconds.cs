using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float DeactivePos;

    private void Update()
    {
        if(transform.position.y > DeactivePos || transform.position.y < -DeactivePos)
        {
            gameObject.SetActive(false);
        }
    }
}
