using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public float lifePoints = 100f;
    private float distance;
    public Transform Player;
    public Transform Boss;
    public Image LifeBar;
    public Text StatusText;

    private float maxHealth = 100f;

    // Use this for initialization
    void Start ()
    {
        LifeBar.rectTransform.localScale = new Vector3(319.0f, 1f, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
        LoseHealth();
        UpdateHealthBar();
        //Debug.Log(lifePoints);
	}

    private void UpdateHealthBar()
    {
        float ratio = lifePoints / maxHealth;
        LifeBar.rectTransform.localScale = new Vector3(ratio, 0.5f, 0.1f);
    }

    public void LoseHealth()
    {
        distance = Vector3.Distance(Player.position, Boss.position);
        if (distance < 13.5f)
        {
            StatusText.text = "I am hurting.";   
            lifePoints -= 10f * Time.deltaTime;
        }
        else
        {
            StatusText.text = "I am fine.";
        }
            
        if (lifePoints < 1)
        {
            SceneManager.LoadScene(3);
        }
    }
}
