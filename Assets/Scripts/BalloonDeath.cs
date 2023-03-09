using System;
using UnityEngine;

    public class BalloonDeath: MonoBehaviour
    {
        private GameManager gameManager;
        private AudioSource popSound;
        private Vector2 startPosition;
        private CapsuleCollider2D capsuleCollider2D;
        private SpriteRenderer spriteRenderer;
        void Start()
        {
            startPosition = gameObject.transform.position;
            capsuleCollider2D=gameObject.GetComponent<CapsuleCollider2D>();
            spriteRenderer=gameObject.GetComponent<SpriteRenderer>();
            popSound = GameObject.Find("PopSound").GetComponent<AudioSource>();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            gameManager.LoseLife(); 
            capsuleCollider2D.enabled = false;
            spriteRenderer.enabled = false;
            popSound.Play();
        }

        public void Reset()
        {
            transform.position = startPosition;
            capsuleCollider2D.enabled = true;
            spriteRenderer.enabled = true;
        }
    }