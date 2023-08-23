using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;

    [SerializeField] private ObjectPool objectPool = null;

    private SpriteRenderer spriteRenderer;
    private Vector2 offSetPos = new Vector2(0, -7f);
    private Vector2 startPos = new Vector2(0, -6f);
   
    Camera cam;
    public float width;
    private float dirx;
    private bool isShooting;

    public ShipStats shipStats;

    private void Awake()
    {
        cam = Camera.main;
        width = ((1 / (cam.WorldToViewportPoint(new Vector3(1, 1, 0)).x - .5f) / 2)- 0.25f);
        
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //Debug.Log(width);
        shipStats.currentHealth = shipStats.maxHealth;
        shipStats.currentLifes = shipStats.maxLifes;
        UIManager.UpdateHealthBar(shipStats.currentHealth);
        UIManager.UpdateLives(shipStats.currentLifes);
        UIManager.UpdateHighScore();
        transform.DOMove(startPos, 2f).SetEase(Ease.OutQuad);

    }

    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKey(KeyCode.A) && transform.position.x > -width)
        {
            transform.Translate(Vector2.left * Time.deltaTime * shipStats.shipSpeed);
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x < width)
        {
            transform.Translate(Vector2.right * Time.deltaTime * shipStats.shipSpeed);
        }
        if(Input.GetKey(KeyCode.Space) && !isShooting)
        {
            StartCoroutine(Shoot());
        }
#endif
        dirx = Input.acceleration.x;
        //Debug.Log(dirx);
        if (dirx <= -0.1f && transform.position.x > -width)
        {
            transform.Translate(Vector2.left * Time.deltaTime * shipStats.shipSpeed);
        }
        if (dirx >= 0.1f && transform.position.x < width)
        {
            transform.Translate(Vector2.right * Time.deltaTime * shipStats.shipSpeed);
        }
    }

    public void ShootButton()
    {
        if (!isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    public void AddHealth()
    {
        if(shipStats.currentHealth == shipStats.maxHealth)
        {
            UIManager.UpdateScore(250);//Maxhealth'de iken obje ile karşılaşırsa ek puan kazanma
        }
        else
        {
            shipStats.currentHealth++;UIManager.UpdateHealthBar(shipStats.currentHealth);
        }
    }

    public void AddLife()
    {
        if (shipStats.currentLifes == shipStats.maxLifes)
        {
            UIManager.UpdateScore(1000);//Maxhealth'de iken obje ile karşılaşırsa ek puan kazanma
        }
        else
        {
            shipStats.currentLifes++; UIManager.UpdateHealthBar(shipStats.currentLifes);
        }
    }


    private IEnumerator Shoot()
    {
        isShooting = true;
        //Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        GameObject obj = objectPool.GetPooledObject();
        obj.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(shipStats.fireRate);
        isShooting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("Player Hit..!");
            collision.gameObject.SetActive(false);
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        shipStats.currentHealth--;
        UIManager.UpdateHealthBar(shipStats.currentHealth);

        if (shipStats.currentHealth <=0)
        {
            shipStats.currentLifes--;
            UIManager.UpdateLives(shipStats.currentLifes);

            if (shipStats.currentLifes <= 0)
            {
                Debug.Log("Game Over");
                UIManager.UpdateHighScore();
                AlienMaster.allAliens.Clear(); 
                UIManager.GameOver();
            }
            else
            {
                Debug.Log("Respawn");
                StartCoroutine(ReSpawn());
                
            }
        } 
    }

    private IEnumerator ReSpawn()
    {
        spriteRenderer.enabled = false;
        transform.position = offSetPos;

        yield return new WaitForSeconds(2);
        shipStats.currentHealth = shipStats.maxHealth;
        spriteRenderer.enabled = true;
        transform.DOMove(startPos, 1f).SetEase(Ease.OutQuad);

        UIManager.UpdateHealthBar(shipStats.currentHealth);
        
    }
}
