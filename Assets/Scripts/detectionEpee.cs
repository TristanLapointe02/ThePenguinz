using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Script associé à l'épée pour savoir si elle a été prise
 * 
 * Par : Tristan Lapointe
 * 
 * Dernière modification : 8 décembre 2021
 * 
*/
public class detectionEpee : MonoBehaviour
{
    public static bool epeePrise; //Variable qui détecte si l'épée est prise ou non

    public void OnTriggerEnter(Collider collision)
    {
        //Si l'arme touche un joueur
        if (collision.gameObject.tag == "Player")
        {
            epeePrise = true;
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        //Si l'arme touche un joueur
        if (collision.gameObject.tag == "Player")
        {
            epeePrise = false;
        }
    }
}
