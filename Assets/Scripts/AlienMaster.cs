using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMaster : MonoBehaviour
{
    [SerializeField] private ObjectPool objectPool = null;
    [SerializeField] Player playerScript; //Bunun yerine levelBoundery olusturmak daha iyi olur

    public GameObject motherShipPrefab;
    public GameObject bulletPrefab;
    private Vector3 motherShipSpawnPos = new Vector3(3.72f, 4.85f, 0);
    private Vector3 hMoveDistance = new Vector3(0.05f, 0, 0);
    private Vector3 vMoveDistance = new Vector3(0, 0.15f, 0);
    private float width;
    private const float max_Move_Speed = 0.02f;

    public static List<GameObject> allAliens = new List<GameObject>();

    private bool movingRight;
    private float moveTimer = 0.01f;
    private float moveTime = 0.005f;
    private float shootTimer = 3f;
    private const float shootTime = 3f;
    private float motherShipTimer = 1f;
    private const float MOTH_SHIP_MIN = 15f;
    private const float MOTH_SHIP_MAX = 60f;

    void Start()
    {
        width  = playerScript.width - 0.15f;
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Alien"))
        {
            allAliens.Add(go);
        }
    }

    void Update()
    {
        if(moveTimer <= 0)
        {
            MoveEnemies();
        }
        if (shootTimer <= 0)
        {
            Shoot();
        }
        if(motherShipTimer <= 0)
        {
            MotherShipSpawn();
        }
        moveTimer -= Time.deltaTime;
        shootTimer -= Time.deltaTime;
        motherShipTimer -= Time.deltaTime;
    }

    private void MoveEnemies()
    {
        int hitMax = 0;
        if(allAliens.Count > 0)
        {
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

                if (allAliens[i].transform.position.x > width || allAliens[i].transform.position.x < -width)
                {
                    hitMax++;
                }
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

    private void MotherShipSpawn()
    {
        Instantiate(motherShipPrefab, motherShipSpawnPos, Quaternion.identity);
        motherShipTimer = Random.Range(MOTH_SHIP_MIN, MOTH_SHIP_MAX);
    }

    private void Shoot()
    {
        Vector2 pos = allAliens[Random.Range(0, allAliens.Count)].transform.position;

        GameObject obj = objectPool.GetPooledObject();
        obj.transform.position = pos;

        shootTimer = shootTime;
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
