using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Script associé fusil pour détecté s'il a été pris ou non
 * 
 * Par : Tristan Lapointe
 * 
 * Dernière modification : 8 décembre 2021
 * 
*/
public class detectionTir : MonoBehaviour
{
    public Transform fusilEnfant; //Référence au bout du fusil

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
        if (collision.gameObject.tag == "Player")
        {
            //Si l'arme ne touche plus à un joueur
            tirBalle.boutFusil = null;
        }
    }
}
