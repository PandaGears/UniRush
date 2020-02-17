﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerBehaviour : MonoBehaviour
{
    private bool haswon = false;
    private bool haslost = false;
    public AudioClip death;
    public AudioSource aSource;
    [SerializeField]
    public int health = 1; 
    public int deaths;
    public Weapon weapon;
    public Text Ammodeets;

    public Text weapondeets;

    public Text death_count;

    [SerializeField]
    private Transform GunTip;
    private Rigidbody2D rb2d;
    [SerializeField]
    public GameObject bullet;

    public Vector2 bulletPos;
    public Collider2D droppedWeapon;
    public GameObject effect;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        death_count.text = " KILL";

        if (weapon == null){
            weapondeets.text = "WEAPON : ";
		    Ammodeets.text = "AMMO : ";
            } 
        if (Input.GetKey(KeyCode.Mouse0) && weapon != null) {
            weapondeets.text = "WEAPON : " + weapon;
		    Ammodeets.text = "AMMO : " + weapon.ammo; 
            weapon.fire();
        }

        if (Input.GetKey(KeyCode.Mouse1) && weapon != null) {
            weapon.toss();
        }

        if (Input.GetKey(KeyCode.E)) {
            if (droppedWeapon != null) {
                weapon = droppedWeapon.GetComponent<Weapon>();
                weapon.equip(gameObject);
    
    
            }
        }
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0) {
            FindObjectOfType<GameManager>().GenocideLevel();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Weapon") {
            droppedWeapon = collider;
        }
       else if(collider.gameObject.tag == "Enemy"){
            health--;
            Instantiate(effect, transform.position, Quaternion.identity);
             Debug.Log(health);
            if (health <= 0){
                aSource.PlayOneShot(death);
                aSource.Play();
                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
                FindObjectOfType<GameManager>().LostLevel();
            }
        }
        else if(collider.gameObject.tag == "EnemyBullet"){
            health--;
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(collider.gameObject);
            Debug.Log(health);
            if (health <= 0){
            aSource.PlayOneShot(death);
            aSource.Play();
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            FindObjectOfType<GameManager>().LostLevel();
            deaths++;
       }
        }
        else if (collider.gameObject.tag == "goal") {
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
                FindObjectOfType<GameManager>().CompleteLevel();
            haswon = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Weapon") {
            droppedWeapon = null;
        }
    }
}