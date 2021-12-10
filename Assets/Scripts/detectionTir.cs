using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Script associ� fusil pour d�tect� s'il a �t� pris ou non
 * 
 * Par : Tristan Lapointe
 * 
 * Derni�re modification : 8 d�cembre 2021
 * 
*/
public class detectionTir : MonoBehaviour
{
    public Transform fusilEnfant; //R�f�rence au bout du fusil

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
            //Si l'arme ne touche plus � un joueur
            tirBalle.boutFusil = null;
        }
    }
}
