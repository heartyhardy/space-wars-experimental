using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathVFX : MonoBehaviour {

    [Header("Death Animation")]
    [SerializeField] GameObject deathVFX;

    public GameObject GetVFX()
    {
        return deathVFX;
    }
}
