using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanAggro : Photon.MonoBehaviour {

    private TitansFieldOfView fov;
    public Transform target;
    public GameObject sphereAttack = null;
    private SphereCollider sphereCollider= null;
    private bool attacking = false;
    private Titan_Mouvement tm;
    private AudioSource[] roars;
    private HealthManagement hm;

    // Use this for initialization
    void Start () {
        fov = GetComponent<TitansFieldOfView>();
        tm = GetComponent<Titan_Mouvement>();
        hm = GetComponent<HealthManagement>();
        roars = GetComponents<AudioSource>();
        sphereCollider = sphereAttack.GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
        //StartCoroutine(UnAggroRoutine());
        InvokeRepeating("UnAggroRoutine2", 2.0f, 5.0f);
    }

    private void UnAggroRoutine2()
    {
        Debug.Log("Coroutine " + target);

        if (fov.visibleTargets.Count == 0)
            tm.Wander();
        
    }
    IEnumerator UnAggroRoutine()    
    {
        Debug.Log("Coroutine " + target);

        if (fov.visibleTargets.Count == 0)
            target = null;
        
        yield return new WaitForSecondsRealtime(1f);
    }

    private int FindIndexOfClosest()
    {
        float min = fov.distTargets[0];
        int index = 0;
        for(int i = 1; i < fov.distTargets.Count; i++)
        {
            if (fov.distTargets[i] < min)
            {
                min = fov.distTargets[i];
                index = i;
            }
        }

        return index;
        
    }
	// Update is called once per frame
	void Update () {

        if (fov.visibleTargets.Count != 0)
            target = fov.visibleTargets[FindIndexOfClosest()];

        Debug.Log("!IsDown"+!hm.IsDown());
        if (attacking && (!hm.IsDown()))
        {
            sphereCollider.enabled = true;
            Attack();
        }
    }


    private void Attack()
    {
        tm.navStop();
        int randomSound = Random.Range(0, 2);
        if (!roars[0].isPlaying && !roars[1].isPlaying && !roars[2].isPlaying)
            roars[randomSound].Play();
        Debug.Log("Roared");
        if (!roars[randomSound].isPlaying)
        {
            Debug.Log("Boum Attacking");
            float factor = (1.05f);
            sphereAttack.transform.localScale = new Vector3(sphereAttack.transform.localScale.x * factor, sphereAttack.transform.localScale.y * factor, sphereAttack.transform.localScale.z * factor);
            if (sphereAttack.transform.localScale.x >= 5)
            {
                tm.navStart();
                sphereAttack.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                attacking = false;
            }
        }
    }



    private void OnCollisionEnter(Collision other)
    {

        Debug.Log(other.gameObject.name + " NotTrigger");
        if (other.gameObject.tag == "Arrow")
        {
            Debug.Log("Hit by " + other.gameObject.name.Substring(other.gameObject.name.IndexOf("_") + 1));
            target = GameObject.Find(other.gameObject.name.Substring(other.gameObject.name.IndexOf("_") + 1)).transform;
        }
        if (other.gameObject.name == "playerCollider")
        {
            Debug.Log("Attacking => True");
            attacking = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.name);
        if (other.gameObject.tag == "Arrow")
        {
            Debug.Log("Hit by " + other.gameObject.name.Substring(other.gameObject.name.IndexOf("_") + 1));
            target = GameObject.Find(other.gameObject.name.Substring(other.gameObject.name.IndexOf("_") + 1)).transform;
        }
        if (other.name == "playerCollider")
        {
            Debug.Log("Attacking => True");
            attacking = true;
        }

    }
}
