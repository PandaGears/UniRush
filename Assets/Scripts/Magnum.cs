﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnum : Weapon
{
    public GameObject Owner;

    [SerializeField]
    public Transform GunTip;

    [SerializeField]
    public GameObject Bullet;
    private Rigidbody2D rb2d;
    public float slowRate = 0.91f;
    public float throwSpeed = 50f;
    public float offsetX = -0.15f;
    public float offsetY = -0.35f;
    public float offsetRot = -90.0f;
    public int dps = 150;
    public Magnum() : base(6, 6, 0.25f, 0.0f) {}

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Owner == null) {
            rb2d.velocity = new Vector2(rb2d.velocity.x * slowRate, rb2d.velocity.y * slowRate);
            transform.Rotate(0, 0, dps * Time.deltaTime);
            if (dps > 150) {
                dps -= 5;
            }
            if (Mathf.Abs(rb2d.velocity.y) + Mathf.Abs(rb2d.velocity.x) <= 1) {
                GetComponent<CapsuleCollider2D>().isTrigger = true;
            }
        }
    }

    public override void fire() {
        if (Time.time > nextShot && ammo != 0) {
            nextShot = Time.time + fireRate;
            GameObject firedBullet = Instantiate(Bullet, GunTip.position, GunTip.rotation);
            firedBullet.GetComponent<Rigidbody2D>().velocity = GunTip.right * firedBullet.GetComponent<MagnumBullet>().bulletSpeed;
            if (ammo > 0) {
                ammo -= 1;
            }
        }
    }

    public override void toss() {
        rb2d.simulated = true;
        GetComponent<CapsuleCollider2D>().isTrigger = false;
        transform.SetParent(null, true);
        Owner.GetComponent<PlayerBehaviour>().weapon = null;
        Owner = null;
        rb2d.velocity = transform.right * throwSpeed;
        dps = 1000;
    }

    public override void equip(GameObject owner) {
        rb2d.simulated = false;
        this.Owner = owner;
        transform.position = owner.transform.position;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDirection = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        transform.right = mouseDirection;

        transform.parent = owner.transform;
        transform.localPosition = new Vector3(offsetX, offsetY, 0);
    }
}
