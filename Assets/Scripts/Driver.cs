using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Driver : MonoBehaviour
{
    // Car speed
    [SerializeField] float steerSpeed = 150f;
    [SerializeField] float moveSpeed = 14f;
    [SerializeField] float slowSpeed = 12f;
    [SerializeField] float boostSpeed = 15f;

    // Crash Sound Effects
    [SerializeField] AudioClip bumpSFX;
    [SerializeField] AudioClip boostSFX;

    // Car color on Collision
    [SerializeField] Color32 carBrokenColor = new Color32(253, 83, 83, 255);
    [SerializeField] Color32 noPackageColor = new Color32(255, 255, 255, 255);

    float destroyDelay = 0.2f;

    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // to make turning feels right by adding -steerAmount
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boost") && moveSpeed != boostSpeed)
        {
            Debug.Log("You got a boost! Your car is fixed and back in tip-top shape!");
            moveSpeed = boostSpeed;
            spriteRenderer.color = noPackageColor;
            Destroy(other.gameObject, destroyDelay);
            audioSource.PlayOneShot(boostSFX, 0.4f);
        }

        if (other.CompareTag("Bumps"))
        {
            Debug.Log("You have hit a bumps");
            moveSpeed = slowSpeed;

            Destroy(other.gameObject, destroyDelay);
            spriteRenderer.color = carBrokenColor;

            audioSource.PlayOneShot(bumpSFX, 1f);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("You have crashed into an obstacle.");
        moveSpeed = slowSpeed;

        spriteRenderer.color = carBrokenColor;
        audioSource.PlayOneShot(bumpSFX, 1f);

    }
}

