using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;

    [SerializeField] private ObjectPool objectPool = null;

    Camera cam;
    public float width;
    private float speed= 3f;

    bool isShooting;
    float coolDown = 0.5f;

    //private const float maxX = 0; //sabit değerler kullanacağımızda kullanılır.

    private void Awake()
    {
        cam = Camera.main;
        width = ((1 / (cam.WorldToViewportPoint(new Vector3(1, 1, 0)).x - .5f) / 2)- 0.25f);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(width);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKey(KeyCode.A) && transform.position.x > -width)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x < width)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        if(Input.GetKey(KeyCode.Space) && !isShooting)
        {
            StartCoroutine(Shoot());
        }

#endif
    }

    private IEnumerator Shoot()
    {
        isShooting = true;

        //Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        GameObject obj = objectPool.GetPooledObject();
        obj.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(coolDown);

        isShooting = false;
    }
}
