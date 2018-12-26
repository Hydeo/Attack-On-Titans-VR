using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanAggro : Photon.MonoBehaviour {

    private TitansFieldOfView fov;
    public Transform target;
    public GameObject sphereAttack = null;
    private SphereCollider sphereCollider= null;
    public GameObject attackEffect;
    private bool attacking = false;
    private Titan_Mouvement tm;
    private AudioSource[] roars;
    private HealthManagement hm;
    private ParticleSystem attackParticule;
    private bool coolDownRoarDone = true;
    private Animator m_animator;

    // Use this for initialization
    void Start () {

        m_animator = GetComponent<Animator>();
        fov = GetComponent<TitansFieldOfView>();
        tm = GetComponent<Titan_Mouvement>();
        hm = GetComponent<HealthManagement>();
        roars = GetComponents<AudioSource>();
        sphereCollider = sphereAttack.GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
        attackParticule = attackEffect.GetComponent<ParticleSystem>();
        //StartCoroutine(UnAggroRoutine());
        InvokeRepeating("UnAggroRoutine2", 2.0f, 5.0f);
    }

    private void UnAggroRoutine2()
    {
        Debug.Log("Coroutine " + target);

        if (fov.visibleTargets.Count == 0)
        {
            target = null;
        }
        
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
            //sphereCollider.enabled = true;
            Attack();

        }
    }

    private bool IsARoarPlaying()
    {
        foreach(AudioSource roar in roars)
        {
            if (roar.isPlaying)
            {
                return true;
            }
        }
        return false;
    }

    private void Attack()
    {
        
        int randomSound = Random.Range(0, 3);

        if (!IsARoarPlaying() && !coolDownRoarDone)
        {
            roars[randomSound].volume = 0.0f;
            roars[randomSound].Play();
            coolDownRoarDone = true;
            tm.navStart();
        }
        Debug.Log("Roared" + randomSound);
        if (!IsARoarPlaying() && coolDownRoarDone)
        {
            tm.navStop();
            Debug.Log("Boum Attacking");
            roars[randomSound].volume = 0.6f;
            roars[randomSound].Play();
            m_animator.SetTrigger("Attack");
            attackParticule.Emit(30);
            sphereCollider.enabled = true;
            attacking = false;
            Invoke("SphereColliderDisable",0.5f);
            coolDownRoarDone = false;
        }
    }

    private void SphereColliderDisable()
    {
        sphereCollider.enabled = false;
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
