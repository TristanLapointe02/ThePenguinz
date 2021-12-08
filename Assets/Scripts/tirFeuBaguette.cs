using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit; // acc�s aux objets du XR Interaction Toolkit
using UnityEngine.InputSystem; // pour utiliser le nouveau InputSyteme
using UnityEngine.Events;

public class tirFeuBaguette : MonoBehaviourPunCallbacks
{
    public bool peutTirer = true; //V�rifie si le joueur peut faire enflammer la baguette
    public static GameObject particuleFeu; //Particule de feu
    public static GameObject colliderFeu; //Collider du feu. C'est ce qui va toucher � l'ennemi
    public static Transform boutBaguette; //D�termine le bout de la baguette
    public bool feuActif; //D�termine quand le feu est actif
    public bool feuEtteint; //D�tecter si le feu est �teint
    public AudioClip feuSon; //Son du feu

    [SerializeField]
    InputActionReference inputActionReference_ActiveTrigger;

    public override void OnEnable()
    {
        inputActionReference_ActiveTrigger.action.performed += ActiveTrigger;
    }

    public override void OnDisable()
    {
        // s'ex�cute lorsque le script devient inactif (disable)
        // retire la fonction qui sera appel�e lorsque l'action sera effectu�e
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
        // Bouton enfonc�, on active le rayon
        // ReadValue<float> permet de r�cuper� la valeur de type float contenu dans le param�tre obj
        if (obj.ReadValue<float>() == 1f && boutBaguette != null)
        {
            if (photonView.IsMine)
            {
                //Activer la particule de feu dans la direction du joueur
                particuleFeu.gameObject.SetActive(true);

                //Activer le collider du feu
                colliderFeu.gameObject.SetActive(true);

                //Indiquer que le joueur a activ� le feu
                feuActif = true;

                //Faire jouer le son sur r�seau
                //photonView.RPC("JoueSonFeu", RpcTarget.All);
            }
        }

        if (obj.ReadValue<float>() == 0f && boutBaguette != null)
        {
            if (photonView.IsMine)
            {
                //D�sactiver la particule de feu dans la direction du joueur
                particuleFeu.gameObject.SetActive(false);

                //D�sactiver le collider du feu
                colliderFeu.gameObject.SetActive(false);

                //Indiquer que le joueur a d�sactiv� le feu
                feuEtteint = true;

                //Faire jouer le son sur r�seau
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

        //Attendre un petit d�lai
        yield return new WaitForSeconds(0.25f);

        //D�sactiver le collider
        colliderFeu.SetActive(false);

        //R�activer le feu
        feuActif = true;
    }
}
