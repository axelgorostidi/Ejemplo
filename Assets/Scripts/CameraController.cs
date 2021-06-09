using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    public GameObject referencia;
	public float Velocidad = 4f;
    float ZOriginal = 0;
    Camera camara;
    float internalVel = 0;
    float smoothRate = 0.2f;
    Vector3 velocidadCamara;
    public float magnitudeShootMove = 0.2f;
    public float maxMovement = 2f;


    // Start is called before the first frame update
    void Start()
    {
        ZOriginal = transform.position.z;
        velocidadCamara = new Vector3(Velocidad, Velocidad, 0);
    }

    // Update is called once per frame
    void Update()
    {
        bool puedeMover = true;
        if(!(Math.Abs(transform.position.x - referencia.transform.position.x) > maxMovement || Math.Abs(transform.position.y - referencia.transform.position.y) > maxMovement))
        {
            if (Input.GetKey("up") && Input.GetKey("right") && !Input.GetKey("left") && !Input.GetKey("down")) //******************************
            {
                //Ataque hacia arriba-derecha
                this.transform.position += new Vector3(magnitudeShootMove, magnitudeShootMove, 0f);
                return;
            }
            else if (!Input.GetKey("up") && Input.GetKey("right") && !Input.GetKey("left") && Input.GetKey("down"))
            {
                //Ataque hacia la abajo-derecha
                this.transform.position += new Vector3(magnitudeShootMove, -magnitudeShootMove, 0f);
                return;
            }
            else if (!Input.GetKey("up") && !Input.GetKey("right") && Input.GetKey("left") && Input.GetKey("down"))
            {
                //Ataque hacia la abajo-izquierda
                this.transform.position += new Vector3(-magnitudeShootMove, -magnitudeShootMove, 0f);
                return;
            }
            else if (Input.GetKey("up") && !Input.GetKey("right") && Input.GetKey("left") && !Input.GetKey("down"))
            {
                //Ataque hacia la arriba-izquierda
                this.transform.position += new Vector3(-magnitudeShootMove, magnitudeShootMove, 0f);
                return;
            }
            else if (Input.GetKey("up") && !Input.GetKey("right") && !Input.GetKey("left") && !Input.GetKey("down"))
            {
                //Ataque hacia arriba
                this.transform.position += new Vector3(0f, magnitudeShootMove, 0f);
                return;
            }
            else if (!Input.GetKey("up") && !Input.GetKey("right") && !Input.GetKey("left") && Input.GetKey("down"))
            {
                //Ataque hacia abajo
                this.transform.position += new Vector3(0f, -magnitudeShootMove, 0f);
                return;
            }
            else if (!Input.GetKey("up") && Input.GetKey("right") && !Input.GetKey("left") && !Input.GetKey("down"))
            {
                //Ataque hacia la derecha
                this.transform.position += new Vector3(magnitudeShootMove, -0f, 0f);
                return;
            }
            else if (!Input.GetKey("up") && !Input.GetKey("right") && Input.GetKey("left") && !Input.GetKey("down"))
            {
                //Ataque hacia la izquierda
                this.transform.position += new Vector3(-magnitudeShootMove, 0f, 0f);
                return;
            }
        }

        if (referencia == null)
        {
            return;
        }
        Vector3 tmp = Vector3.SmoothDamp(transform.position,
                                         referencia.transform.position,
                                         ref velocidadCamara,
                                         this.smoothRate);
        tmp.z = ZOriginal;
        this.transform.position = tmp;


    }
}
