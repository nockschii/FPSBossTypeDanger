using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] public float Speed;
    [SerializeField] public float StoppingDistance;
    [SerializeField] public float RetreatDistance;
    [SerializeField] public float Health = 1000f;

    public Transform Player;
    private Color MatColor;

	// Use this for initialization
	void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        MovingTowardsPlayer();
    }

    private void MovingTowardsPlayer()
    {
        if (Vector3.Distance(transform.position, Player.position) > StoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime);
        }
        else if (Vector3.Distance(transform.position, Player.position) < StoppingDistance &&
                 Vector3.Distance(transform.position, Player.position) > RetreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector3.Distance(transform.position, Player.position) < RetreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.position, -Speed * Time.deltaTime);
        }
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        transform.position = Vector3.MoveTowards(transform.position, Player.position, (-1000) * Time.deltaTime);
        GetComponent<Renderer>().material.color = Random.ColorHSV();
        if (Health <= 0f)
        {
            Die();
        }     
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
