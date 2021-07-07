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
      
    }
}
