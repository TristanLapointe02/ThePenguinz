using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Description générale
 * Script simple qui permet de synchroniser un avatar (corps et tête) avec un XR Rig
 * Mathieu Dionne
 * Dernière modifications : 10 septembre 2021
 */

public class ControleAvatar : MonoBehaviour
{

    //Transforms de l'avatar
    public Transform AvatarTransformPrincipal;
    public Transform AvatarTete;
    public Transform AvatarCorps;


    //Transforms du XR Rig qui est la tête (normalement, c'est la caméra)
    public Transform XRTete;

    //Distance entre la position du coprs et la tête (environ -0.9 dans l'exemple du cours)
    public Vector3 tetePositionOffset;
 

    
    void Update()
    {
        //Synchronisation de la position de l'avatar. On se sert de la position de la tête qu'on abaisse avec le Offset.
        //La synchronisation se fait de façon fluide en utilisant la méthode Lerp.
        // On synchronise également la rotation de la tête et du corps
        AvatarTransformPrincipal.position = Vector3.Lerp(AvatarTransformPrincipal.position, XRTete.position + tetePositionOffset, 0.5f);
        AvatarTete.rotation = Quaternion.Lerp(AvatarTete.rotation, XRTete.rotation, 0.5f);
        AvatarCorps.rotation = Quaternion.Lerp(AvatarCorps.rotation, Quaternion.Euler(new Vector3(0, AvatarTete.rotation.eulerAngles.y, 0)), 0.05f);
    }
}
