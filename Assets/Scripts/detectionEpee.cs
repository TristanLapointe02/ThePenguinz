using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Script associ� � l'�p�e pour savoir si elle a �t� prise
 * 
 * Par : Tristan Lapointe
 * 
 * Derni�re modification : 8 d�cembre 2021
 * 
*/
public class detectionEpee : MonoBehaviour
{
    public static bool epeePrise; //Variable qui d�tecte si l'�p�e est prise ou non

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
