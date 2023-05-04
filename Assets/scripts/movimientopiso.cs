using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientopiso : MonoBehaviour
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
}
