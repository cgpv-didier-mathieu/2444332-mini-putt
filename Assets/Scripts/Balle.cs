using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    [SerializeField, Tooltip("La barre de force à l'écran")]
    private Slider barreForce;

    private Rigidbody rb;

    private bool estFrapee = false;
    private float chronoApresTir = 0f;

    private Vector3 positionDepart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        positionDepart = transform.position;
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
            FigerBalle(true);
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
        if (Keyboard.current.spaceKey.isPressed)
        {
            barreForce.value += 1 * Time.deltaTime;
        }

        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            FigerBalle(false);

            Vector3 direction = directionTir != null ? directionTir.Direction : Vector3.right;

            rb.AddForce(direction * forceFrappe * barreForce.value, ForceMode.Impulse);

            if (directionTir != null)
            {
                directionTir.gameObject.SetActive(false);
            }

            if (gestionnaireCamera != null)
            {
                gestionnaireCamera.ActiverCameraSuiviBalle(transform);
            }

            barreForce.value = 0f;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HorsMap"))
        {
            transform.position = positionDepart;
            ReinitialiserTir();
        }
    }

    private void FigerBalle(bool figer)
    {
        if (rb == null) return;

        if (figer)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }
}