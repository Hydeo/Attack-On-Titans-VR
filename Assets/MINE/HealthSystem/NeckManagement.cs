using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeckManagement : MonoBehaviour {
    HealthManagement hm;
    private Material m_material;
    // Use this for initialization
    void Start () {
        hm = this.GetComponentInParent<HealthManagement>();
        m_material = GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sword")
        {
            GetComponent<AudioSource>().Play();
            m_material.color = Color.green;
            hm.NeckHit();
        }
    }
}
