using UnityEngine;
using UnityEngine.InputSystem;

public class Balle : MonoBehaviour
{
    [SerializeField, Tooltip("Force de la frappe")]
    private float forceFrappe;

    [SerializeField, Tooltip("La flèche de direction du tir")]
    private DirectionTir directionTir;

    [SerializeField, Tooltip("Vitesse de la balle est  qui est considérée à l'arrêt")]
    private float seuilVitesseArret = 0.1f;

    [SerializeField, Tooltip("Temps minimal avant de vérifier l'arrêt")]
    private float delaiAvantVerification = 0.5f;

    [SerializeField, Tooltip("Le gestionnaire de la caméra")] 
    private GestionnaireCameraJeu gestionnaireCamera;


    private Rigidbody rb;

    private bool estFrapee = false;
    private float chronoApresTir = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb == null)
            return;

        if (!estFrapee) {
            FrapperBalle();
        }
        else
        {
            VerifierArretBalle();
        }
    }

    public void ReinitialiserTir()
    {
        if (directionTir != null)
        {
            directionTir.gameObject.SetActive(true);
            directionTir.transform.localRotation = Quaternion.identity;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (gestionnaireCamera != null)
        {
            gestionnaireCamera.ActiverCameraLibre();
        }

        transform.rotation = Quaternion.identity;
        estFrapee = false;
        chronoApresTir = 0f;
    }

    public void FrapperBalle()
    {
        // https://docs.unity3d.com/Packages/com.unity.inputsystem@0.1/manual/HowDoI.html
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // La direction vient de la flèche que le joueur oriente avec Q/D (ou les flèches gauche/droite).
            Vector3 direction = directionTir != null ? directionTir.Direction : Vector3.right;

            // https://discussions.unity.com/t/adding-impulse-based-to-an-object-using-other-objects-orientation/586707
            rb.AddForce(direction * forceFrappe, ForceMode.Impulse);

            if (directionTir != null)
            {
                directionTir.gameObject.SetActive(false);
            }

            if (gestionnaireCamera != null)
            {
                gestionnaireCamera.ActiverCameraSuiviBalle(transform);
            }

            estFrapee = true;
        }
    }

    private void VerifierArretBalle()
    {
        chronoApresTir += Time.deltaTime;

        // Attendre un court délai pour éviter de détecter l'arrêt au tout premier frame de la frappe
        if (chronoApresTir < delaiAvantVerification)
            return;

        // Si la vitesse linéaire est très faible
        if (rb.linearVelocity.magnitude <= seuilVitesseArret)
        {
            ReinitialiserTir();
        }
    }
}