using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit; // accès aux objets du XR Interaction Toolkit
using UnityEngine.InputSystem; // pour utiliser le nouveau InputSyteme
using UnityEngine.Events;

public class tirFeuBaguette : MonoBehaviourPunCallbacks
{
    public bool peutTirer = true; //Vérifie si le joueur peut faire enflammer la baguette
    public static GameObject particuleFeu; //Particule de feu
    public static GameObject colliderFeu; //Collider du feu. C'est ce qui va toucher à l'ennemi
    public static Transform boutBaguette; //Détermine le bout de la baguette
    public bool feuActif; //Détermine quand le feu est actif
    public bool feuEtteint; //Détecter si le feu est éteint
    public AudioClip feuSon; //Son du feu

    [SerializeField]
    InputActionReference inputActionReference_ActiveTrigger;

    public override void OnEnable()
    {
        inputActionReference_ActiveTrigger.action.performed += ActiveTrigger;
    }

    public override void OnDisable()
    {
        // s'exécute lorsque le script devient inactif (disable)
        // retire la fonction qui sera appelée lorsque l'action sera effectuée
        inputActionReference_ActiveTrigger.action.performed -= ActiveTrigger;
    }

    void Update()
    {
        if (feuActif && feuEtteint == false && colliderFeu != null && particuleFeu != null)
        {
            Invoke("activeDesactiveFeu", 0f);
            feuActif = false;
        }
    }
    private void ActiveTrigger(InputAction.CallbackContext obj)
    {
        // Bouton enfoncé, on active le rayon
        // ReadValue<float> permet de récuperé la valeur de type float contenu dans le paramètre obj
        if (obj.ReadValue<float>() == 1f && boutBaguette != null)
        {
            if (photonView.IsMine)
            {
                //Activer la particule de feu dans la direction du joueur
                particuleFeu.gameObject.SetActive(true);

                //Activer le collider du feu
                colliderFeu.gameObject.SetActive(true);

                //Indiquer que le joueur a activé le feu
                feuActif = true;

                //Faire jouer le son sur réseau
                //photonView.RPC("JoueSonFeu", RpcTarget.All);
            }
        }

        if (obj.ReadValue<float>() == 0f && boutBaguette != null)
        {
            if (photonView.IsMine)
            {
                //Désactiver la particule de feu dans la direction du joueur
                particuleFeu.gameObject.SetActive(false);

                //Désactiver le collider du feu
                colliderFeu.gameObject.SetActive(false);

                //Indiquer que le joueur a désactivé le feu
                feuEtteint = true;

                //Faire jouer le son sur réseau
                //photonView.RPC("JoueSonFeu", RpcTarget.All);
            }
        }
    }

    /*[PunRPC]
    void JoueSonFeu()
    {
        GetComponent<AudioSource>().PlayOneShot(feuSon);
    }*/

    public IEnumerator activeDesactiveFeu()
    {
        //Activer le collider
        colliderFeu.SetActive(true);

        //Attendre un petit délai
        yield return new WaitForSeconds(0.25f);

        //Désactiver le collider
        colliderFeu.SetActive(false);

        //Réactiver le feu
        feuActif = true;
    }
}
