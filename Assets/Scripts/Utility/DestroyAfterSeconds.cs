using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float bulletDeactivePos;

    private void Update()
    {
        if(transform.position.y > bulletDeactivePos || transform.position.y < -bulletDeactivePos)
        {
            gameObject.SetActive(false);
        }
    }
}
