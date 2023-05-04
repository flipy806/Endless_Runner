using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movmoneda : MonoBehaviour
{
   [SerializeField] float speed;
[SerializeField] int health = 5;


// Use this for initialization
void Start () {
Invoke ("Autodestuction", health);

}
// Update is called once per frame
void Update () {
transform. Translate (Vector3.forward * speed * Time.deltaTime);
}
void Autodestuction(){
Destroy (gameObject);

 }

    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player")) 
    {
      
      Destroy (gameObject);
    
       Debug.Log("agarro moneda");
    }

}

}
