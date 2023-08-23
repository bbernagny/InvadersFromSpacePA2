using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealth : PickUp
{
    public override void PickMeUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddHealth();
        gameObject.SetActive(false);
    }
}
