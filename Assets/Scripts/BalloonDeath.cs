using UnityEngine;

    public class BalloonDeath: MonoBehaviour
    {
        private GameManager gameManager;
        private AudioSource popSound;
        void Start()
        {
            popSound = GameObject.Find("PopSound").GetComponent<AudioSource>();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            gameManager.LoseLife(); 
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            popSound.Play();
        }
    }