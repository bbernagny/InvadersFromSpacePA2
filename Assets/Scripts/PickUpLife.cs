using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpLife : PickUp
{
    public override void PickMeUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddLife();
        gameObject.SetActive(false);
    }
}
