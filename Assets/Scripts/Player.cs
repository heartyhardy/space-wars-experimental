using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("General")]
    [SerializeField] float health = 100f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileSpawnTime = 0.1f;

    [Header("Player Movement")]
    [SerializeField] float movespeedX = 10f;
    [SerializeField] float movespeedY = 5f;
    [SerializeField] float padding = 1f;

    [Header("SFX")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0f, 1f)] float deathSFXVolume = 0.2f;
    [SerializeField] [Range(0f, 1f)] float shootSFXVolume = 0.1f;

    float xMin, xMax, yMin, yMax;
    Coroutine continuousFire;
	// Use this for initialization
	void Start () {
        SetMoveBounds();
	}

    // Update is called once per frame
    void Update () {
        Move();
        Fire();
	}

    public float GetHP()
    {
        return health;
    }
       
    private void SetMoveBounds()
    {
        Camera mainCam = Camera.main;
        padding = 1f;

        xMin = mainCam.ViewportToWorldPoint(
                new Vector3(0,0,0)
            )
            .x;

        xMax = mainCam.ViewportToWorldPoint(
                new Vector3(1, 0, 0)
            )
            .x;

        yMin = mainCam.ViewportToWorldPoint(
            new Vector3(0, 0, 0)
            )
            .y;

        yMax = mainCam.ViewportToWorldPoint(
            new Vector3(0, 1, 0)
            )
            .y;
    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            continuousFire= StartCoroutine(FireContinuously());
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(continuousFire);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject projectile = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
            yield return new WaitForSeconds(projectileSpawnTime);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") *Time.deltaTime * movespeedX;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movespeedY;

        var newPosX = Mathf.Clamp(transform.position.x + deltaX,xMin+padding,xMax-padding);
        var newPosY = Mathf.Clamp(transform.position.y + deltaY,yMin+padding,yMax-padding);

        transform.position = new Vector2(newPosX, newPosY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        if(damageDealer != null)
        {
            health -= damageDealer.GetDamage();
            damageDealer.Hit();

            if (health <= 0)
                ProcessDeath();

        }
    }

    private void ProcessDeath()
    {
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        Destroy(gameObject);
        GameObject deathvfx = GetComponent<DeathVFX>().GetVFX();
        GameObject explosion = Instantiate(deathvfx, transform.position, Quaternion.identity);
        Destroy(explosion, .5f);
        GameOver();
    }

    private void GameOver()
    {
        FindObjectOfType<SceneLoader>().LoadEndGame();        
    }
}
