using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class playermovement : MonoBehaviour
{
    Rigidbody rigid;

    [Header("Setup")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float laneDistance;
    [SerializeField] float slideDuration;
    [SerializeField] float slideSpeed;
     [SerializeField] public TMP_Text Score;
     [SerializeField] public TMP_Text Life;
    [SerializeField] public TMP_Text HighScores;
    [SerializeField] public int Cupcake;
    [SerializeField] public float Vidas = 3;
    public int SaveScore;
    public GameObject Panel;
    public Animator Freddy;
    public BoxCollider Agachado;
    public CapsuleCollider DePie;
    public AudioSource BGmusic;
    

    int currentLane = 0;
    int desiredLane = 0;
    bool jumping = false;
    bool sliding = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        Cupcake = 0;
        Vidas = 3;
        Life.text = " " + Vidas;
        Score.text = "X " + Cupcake; 
        Panel.SetActive(false);
        BGmusic.Play();
        SaveScore = PlayerPrefs.GetInt("HighScore", 0);
        //HighScores.text = "High Score: " + Cupcake.ToString();

    }

    void Update()
    {
        // Check if the player wants to move to the right
        if (Input.GetButtonDown("Right") && desiredLane < 1 && !jumping)
        {
            desiredLane++;
            
        }
        // Check if the player wants to move to the left
        if (Input.GetButtonDown("Left") && desiredLane > -1 && !jumping)
        {
            desiredLane--;
        }
        // Check if the player wants to jump
        if (Input.GetButtonDown("Up") && !jumping)
        {
            Freddy.SetBool("Salto",true);
            StartCoroutine(JumpFred());
            jumping = true;
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
         // Check if the player wants to slide
        if (Input.GetButtonDown("Down") && !jumping && !sliding)
        {
            Freddy.SetBool("despla",true);
            DePie.enabled = false;
            Agachado.enabled = true;
            StartCoroutine(SlideFred());
            sliding = true;
            Invoke("StopSliding", slideDuration);
        }
        
        // Move the player horizontally
        Vector3 targetPosition = transform.position;
        targetPosition.x = desiredLane * laneDistance;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the player is in the desired lane
        if (transform.position.x == targetPosition.x)
        {
            currentLane = desiredLane;
        }

          // Slide the player
        if (sliding)
        {
            transform.position += Vector3.down * slideSpeed * Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Reset jumping when the player lands on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumping = false;
            sliding = false;
        }
    }

     void StopSliding()
    {
        sliding = false;
    }

      private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("obstaculo")) 
    {
    
       Vidas --;
       Life.text = " " + Vidas;

       if(Vidas == 0){
         Debug.Log("se murio");
         Time.timeScale = 0;
         BGmusic.Stop();
         Panel.SetActive(true);
         AddScore(Cupcake);

       }
       
    }

    else if (other.CompareTag("Cupcake")) 
    {
      Cupcake ++;
      Score.text = "X " + Cupcake; 
    }

}

  IEnumerator JumpFred(){
        
        yield return new WaitForSeconds(1.03f);
        Freddy.SetBool("Salto",false);
  }

  IEnumerator SlideFred(){

     yield return new WaitForSeconds(1.05f);
        DePie.enabled = true;
        Agachado.enabled = false;
        Freddy.SetBool("despla",false);
  }

public void AddScore(int points)
    {
        SaveScore += points;
        PlayerPrefs.SetInt("HighScore", SaveScore);

    }


}


