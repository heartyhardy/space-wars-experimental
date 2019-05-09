using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header("General")]
    [SerializeField] float health=100f;

    [Header("Offensive")]
    [SerializeField] float shotCounter;    
    [SerializeField] float minCooldownTime = 0.2f;
    [SerializeField] float maxCooldownTime = 3f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;

    [Header("SFX")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0f, 1f)] float deathSFXVolume = 0.2f;
    [SerializeField] [Range(0f, 1f)] float shootSFXVolume = 0.1f;

    // Use this for initialization
    void Start ()
    {
        SetShotCounter();
    }

    private void SetShotCounter()
    {
        shotCounter = UnityEngine.Random.Range(minCooldownTime, maxCooldownTime);
    }

    // Update is called once per frame
    void Update () {
        shotCounter -= Time.deltaTime;

        if(shotCounter <= 0)
        {
            Fire();
            SetShotCounter();
        }
	}

    private void Fire()
    {
        GameObject projectile = Instantiate(laserPrefab, transform.position,new Quaternion(90f,0f,0f,0f));
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();

        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        if (damageDealer != null)
        {
            health -= damageDealer.GetDamage();
            damageDealer.Hit();

            if (health <= 0)
                ProcessDeath();
        }
    }

    private void ProcessDeath()
    {
        Destroy(gameObject);
        GameObject deathvfx = GetComponent<DeathVFX>().GetVFX();
        GameObject explosion=Instantiate(deathvfx, transform.position, Quaternion.identity);
        playDeathSFX();
        Destroy(explosion, .5f);
    }

    private void playDeathSFX()
    {
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }
}
