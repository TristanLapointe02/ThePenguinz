using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectionTir : MonoBehaviour
{
    public Transform fusilEnfant; //Bout du fusil

    void Start()
    {
        //Ignorer la collision avec les ennemis
        Physics.IgnoreLayerCollision(9, 10);
    }
    public void OnTriggerEnter(Collider collision)
    {
        //Si l'arme touche un joueur
        if (collision.gameObject.tag == "Player")
        {
            tirBalle.boutFusil = fusilEnfant;

        }
    }

    public void OnTriggerExit(Collider collision)
    {
        //Si l'arme ne touche plus à un joueur
        tirBalle.boutFusil = null;
    }
}
