using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Object")]
    public GameObject objWeapon;
    public GameObject objBody;
    public GameObject objWheel;

    [HideInInspector]
    public Weapon weapon;
    [HideInInspector]
    public Body body;
    [HideInInspector]
    public Wheel wheel;


    public void Awake()
    {
        weapon = objWeapon.GetComponent<Weapon>();
        body = objBody.GetComponent<Body>();
        wheel = objWheel.GetComponent<Wheel>();
    }

    public void InitEnemy()
    {
        body.SetTransformCenter(objWheel);
    }

    public void FixedUpdate()
    {
        
    }
}
