using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FeetManagement : MonoBehaviour {

    public FeetType feetType;
    private float getBackToFeetTimer;
    private float currentTimer;
    private Material m_material;
    HealthManagement hm;

    // Use this for initialization  
    void Start () {
        hm = this.GetComponentInParent<HealthManagement>();
        getBackToFeetTimer = hm.getBackToFeetTimer;
       
        m_material = GetComponent<Renderer>().material;
        currentTimer = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (!hm.GetFeetStatus(feetType))
        {
            currentTimer += Time.deltaTime;
            
            if (currentTimer >= getBackToFeetTimer)
            {
                currentTimer = 0;
                hm.SetFeetStatus(feetType, true);
                m_material.color = Color.red;
                //animation get up
                Debug.Log(feetType.ToString() + " Get Up !");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sword" || collision.gameObject.tag == "Arrow")
        {
            if (collision.gameObject.tag == "Sword")
                GetComponent<AudioSource>().Play();
            hm.HitFeet(feetType);
            m_material.color = Color.green;
            hm.SetFeetStatus(this.feetType, false);
            Debug.Log(feetType + " is down!");
        }
    }
}
