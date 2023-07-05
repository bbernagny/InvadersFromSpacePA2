using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMaster : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Vector3 hMoveDistance = new Vector3(0.05f, 0, 0);
    private Vector3 vMoveDistance = new Vector3(0, 0.15f, 0);

    private const float Max_left = -2;
    private const float Max_right = 2;
    private const float max_Move_Speed = 0.02f;

    public static List<GameObject> allAliens = new List<GameObject>();

    private bool movingRight;
    private float moveTimer = 0.01f;
    private float moveTime = 0.005f;

   


    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Alien"))
        {
            allAliens.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(moveTimer <= 0)
        {
            MoveEnemies();
        }
        moveTimer -= Time.deltaTime;
    }

    private void MoveEnemies()
    {
        int hitMax = 0;
        for (int i = 0; i < allAliens.Count; i++)
        {
            if (movingRight)
            {
                allAliens[i].transform.position += hMoveDistance;
            }
            else
            {
                allAliens[i].transform.position -= hMoveDistance;
            }
            if (allAliens[i].transform.position.x > Max_right || allAliens[i].transform.position.x < Max_left)
            {
                hitMax++;
            }
        }
        if (hitMax > 0)
        {
            for (int i = 0; i < allAliens.Count; i++)
            {
                allAliens[i].transform.position -= vMoveDistance;
            }
            movingRight = !movingRight;
        }
        moveTimer = GetMovedSpeed();
    }

    private float GetMovedSpeed()
    {
        float f = allAliens.Count * moveTime;
        if(f < max_Move_Speed)
        {
            return max_Move_Speed;
        }
        else
        {
            return f;
        }
    }
}
