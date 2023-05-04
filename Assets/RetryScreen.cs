using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryScreen : MonoBehaviour
{
    
    public AudioSource BGmusic;
   public void Retrys() {

     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
     Time.timeScale = 1;
     
}
}
