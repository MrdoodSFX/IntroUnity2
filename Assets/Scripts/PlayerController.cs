using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("vitesse de déplacement"), Range(0, 20)]
    public float speed = 0;
    public bool isGrounded;

    public Transform objectToThrow;
    public Transform MainCamera;
    // Start is called before the first frame update
    void Start()
    {
        if (MainCamera == null)

        {
            MainCamera = transform.GetComponentInChildren<Camera>().transform;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        //sauve la rotation
        Quaternion lastRotation = MainCamera.rotation;
        //baisse / leve la tete
        float rot = Input.GetAxis("Mouse Y") * -10;
        Quaternion q = Quaternion.AngleAxis(rot, MainCamera.right);
        MainCamera.rotation = q * MainCamera.rotation;

        //est qu'on a la tete a l'envers ?
        Vector3 forwardCam = MainCamera.forward;
        Vector3 forwardBody = transform.forward;
        float regardeDevant = Vector3.Dot(forwardCam, forwardBody);
        if (regardeDevant < 0.0f)
        {
            MainCamera.rotation = lastRotation;
        }
        if (regardeDevant > 1.0f)
        {
            MainCamera.rotation = lastRotation;
        }
        rot = Input.GetAxis("Mouse X") * 10;
        q = Quaternion.AngleAxis(rot, transform.up);
        transform.rotation = q * transform.rotation;

        if (Input.GetButtonDown("Fire1"))
        {
            Transform obj = GameObject.Instantiate<Transform>(objectToThrow);
            obj.position = MainCamera.position + MainCamera.forward;
            obj.GetComponent<Rigidbody>().AddForce(MainCamera.forward * 40, ForceMode.Impulse);
        }
    }
    void FixedUpdate()
    {
        Vector3 vert = Input.GetAxis("Vertical") * transform.forward;
        Vector3 horiz = Input.GetAxis("Horizontal") * transform.right;
        Vector3 deplacement = (vert + horiz) * speed;
        //Vector3 camVert = Input.GetAxis("Mouse X") * transform.up;
        //Vector3 camHoriz = Input.GetAxis("Mouse Y") * transform.right;
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 horizontalVelocity = Vector3.zero;

        /* Autre maniere de faire le mouvement en prenant en compte la velocité
        //horizontalVelocity += vert * transform.forward * 10;
        //horizontalVelocity += horiz * transform.forward * 10;
        //rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, rb.velocity.z);
        */

        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {
            rb.velocity = new Vector3(deplacement.x, rb.velocity.y, deplacement.z);
        }

        //tomber plus vite
        if (rb.velocity.y < 0)
        {
            rb.AddForce(-transform.up * 30);
        }
        //Si on appuie sur jump
        /*if (Input.GetButton("Jump"))
        {
            //limite de hauteur
            if (transform.position.y < 0.3)
            {
                //jetpack
                rb.AddForce(transform.up * 15);
            }

        }*/
        //verif si le perso touche le sol
        RaycastHit infos;
        bool trouve = Physics.SphereCast(transform.position, 0.05f, -transform.up, out infos, 10);
        if (trouve && infos.distance < 0.05)
        {
            isGrounded = true;
        }

        if (Input.GetButton("Jump") && isGrounded)
        {
            rb.AddForce(transform.up * 20, ForceMode.Impulse);
        }
        else
        {
            if (rb.velocity.y < 3)
            {
                rb.AddForce(-transform.up * 30);
            }
            /*else
                rb.velocity = new Vector3(rb.velocity.x, 1, rb.velocity.z);*/
        }
        // rb.angularVelocity = (camVert + camHoriz) * speed;
        //rb.AddTorque(Input.GetAxis("Mouse X") * speed * transform.up);
    }
}
