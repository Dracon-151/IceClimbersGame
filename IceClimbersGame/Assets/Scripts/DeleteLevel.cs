using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteLevel : MonoBehaviour
{
    private Transform despwnerinador;

    private void Start()
    {
        despwnerinador = GameObject.FindObjectOfType<LevelGenerator>().despawnerinador;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < despwnerinador.position.y)
        {
            Destroy(this.gameObject);
        }  
    }
}
