using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private AudioSource GunShotSound;
    [SerializeField] private KeyCode ShootKey;
    public float damage = 10f;
    public float range = 100f;

    public GameObject Munition;
    public GameObject Hull;

    public Camera FpsCam;
    public AudioSource GunAudio;
    public ParticleSystem MuzzleFlash;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            ((Animator)Munition.GetComponent("Animator")).enabled = true;
            ((Animator)Hull.GetComponent("Animator")).enabled = true;
            GunAudio.Play();    
            StartCoroutine(WaitAnimator());
        }
	}

    private IEnumerator WaitAnimator()
    {
        yield return new WaitForSeconds(0.2f);
        ((Animator)Munition.GetComponent("Animator")).enabled = false;
        ((Animator)Hull.GetComponent("Animator")).enabled = false;
        //GetComponent<Animation>().Play("Hull")
    }

    private void Shoot()
    {
        MuzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(FpsCam.transform.position, FpsCam.transform.forward, out hit, range))
        {
            Target enemy = hit.transform.GetComponent<Target>();
            EnemyAI boss = hit.transform.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
        }

    }
}
