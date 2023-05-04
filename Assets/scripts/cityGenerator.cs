using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cityGenerator : MonoBehaviour
{
    [SerializeField] Transform city;
    [SerializeField] float citySpeed;

    //lista de objetos

    [SerializeField] List<Transform> cityPieces;

    [SerializeField] Transform cityPiece;
    Transform lastPiece;

    [Header ("OBSTACLES")]
    [SerializeField] List<Transform> obstPieces;

   // Use this for initialization
    void Start () {
    GenerateCity ();
    GenerateObstacle();
    }
    
    // Update is called once per frame
    void Update () {
    city.Translate (Vector3.back * citySpeed * Time.deltaTime);
    }

    void GenerateCity (){

    lastPiece = cityPiece;

    cityPiece = cityPieces [Random.Range (0, cityPieces.Count) ];
    cityPiece.position = new Vector3 (0, 0, lastPiece.position.z + 35);
    cityPiece.gameObject.SetActive (true);
    cityPiece.SetParent (city);
    cityPieces. Remove (cityPiece);

    Invoke ("Generatecity", 3.5f);
    
    }

    void OnTriggerEnter (Collider coll){
        Transform t = coll. GetComponent<Transform> ();
        t.SetParent (null);

        if (coll.CompareTag ("Citypiece"))
        {
            cityPieces.Add (t);
        }
        else if (coll.CompareTag ("Obstpiece"))
        {
            obstPieces.Add (t);
        }


        

        coll.gameObject.SetActive (false);

     }

     void GenerateObstacle (){

        Transform obstPiece = obstPieces [Random.Range (0, obstPieces .Count)];

        obstPiece.position = Vector3. forward * 80;
        obstPiece.gameObject.SetActive (true);
        obstPiece.SetParent (city);

        obstPieces.Remove (obstPiece);

    Invoke ("GenerateObstacle", 6);

 }
 
 }