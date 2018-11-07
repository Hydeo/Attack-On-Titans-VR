using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FeetType
{
    left, right
}

public class HealthManagement : Photon.MonoBehaviour {

    public int maxHealth;
    private bool neckIsAlive = false;
    private bool leftFeetAlive = true;
    private bool rightFeetAlive = true;
    private bool tryingToGetUp = false;
    public float getBackToFeetTimer;
    private Animator m_animator;

    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        Debug.Log("Chan Destroyed");
    }

    // Update is called once per frame
    void Update () {

        if (!leftFeetAlive && !rightFeetAlive && !tryingToGetUp)
        {
            Debug.Log("D");
            TriggerDeath();
            tryingToGetUp = true;
        }

        if((leftFeetAlive && rightFeetAlive) && tryingToGetUp)
        {
            Debug.Log("U");
            tryingToGetUp = false;
            TriggerGetUp();
        }

    }

    public void NeckHit()
    {
        Debug.Log("Dead Titan !");
        TriggerDeath();
        if (PhotonNetwork.isMasterClient)
        {
            Invoke("DestroyTitan", 5);
        }
        //Destroy after X

    }

    public void DestroyTitan()
    {
        Debug.Log("Destroy");
        PhotonNetwork.Destroy(transform.root.gameObject);
    }

    public void TriggerDeath()
    {
        m_animator.SetTrigger("Death");
    }

    public void TriggerGetUp()
    {
        m_animator.SetTrigger("GetUp");
    }
   public void HitFeet(FeetType feetStroke)
    {
        switch (feetStroke)
        {
            case FeetType.left:
                this.leftFeetAlive = false;
                break;

            case FeetType.right:
                this.rightFeetAlive = false;
                break;
        }

    }

    public bool GetFeetStatus(FeetType feet)
    {
        switch (feet)
        {
            case FeetType.left:
                return leftFeetAlive;
            case FeetType.right:
                return rightFeetAlive;
            default:
                return true;
        }
    }

    public void SetFeetStatus(FeetType feet, bool status)
    {

        switch (feet)
        {
            case FeetType.left:
                this.leftFeetAlive = status;
                break;
            case FeetType.right:
                this.rightFeetAlive = status;
                break;
        }
    }
}
