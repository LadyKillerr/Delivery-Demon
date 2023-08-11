using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Delivery;
namespace Driver
{
    public class Driver : MonoBehaviour
    {
        // Car speed
        [SerializeField] float steerSpeed = 150f;
        [SerializeField] float moveSpeed = 10f;
        [SerializeField] float slowSpeed = 7f;
        [SerializeField] float boostSpeed = 12f;

        // Car color on Collision
        [SerializeField] Color32 carBrokenColor = new Color32(253, 83, 83, 255);
        [SerializeField] Color32 noPackageColor = new Color32(255, 255, 255, 255);

        
        SpriteRenderer spriteRenderer;
        void Start()
        {
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
            if (other.CompareTag("Boost"))
            {
                Debug.Log("You got a boost! Your car is fixed and back in tip-top shape!");
                moveSpeed = boostSpeed;
                spriteRenderer.color = noPackageColor;
                Destroy(other.gameObject, 0.2f);
            }

            if (other.CompareTag("Bumps"))
            {
                Debug.Log("You have hit a bumps");
                moveSpeed = slowSpeed;
                
                Destroy(other.gameObject, 0.2f);
                spriteRenderer.color = carBrokenColor;

            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("You have crashed into an obstacle.");
            moveSpeed = slowSpeed;
            
            spriteRenderer.color = carBrokenColor;
        }
    }
}
