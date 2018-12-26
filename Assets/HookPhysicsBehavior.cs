using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;


public class HookPhysicsBehavior : MonoBehaviour {

    public enum Side { left, right };
    private bool isLeftFlying;
    private bool isRightFlying;
    public Vector3 leftLoc;
    public Vector3 rightLoc;
    private VRTK_BodyPhysics bp;
    private GameObject player;
    public float speed;
    private Vector3 velocity;
    void Start () {
        isLeftFlying = false;
        isRightFlying = false;
        GameObject pa = GameObject.FindGameObjectWithTag("PlayArea");
        bp = pa.GetComponent<VRTK_BodyPhysics>();
    }


    public void SetHook(Side side, Vector3 loc)
    {
        switch (side)
        {
            case Side.left:
                Debug.Log("Left Loc set " + loc.ToString());
                isLeftFlying = true;
                this.leftLoc = loc;
                break;
            case Side.right:
                Debug.Log("Right Loc set " + loc.ToString());
                isRightFlying = true;
                this.rightLoc = loc;
                break;
        }
        
    }



    public void SetFlyingToFalse(Side whichHook)
    {
        switch (whichHook)
        {
            case Side.left:
                this.isLeftFlying = false;
                break;
            case Side.right:
                this.isRightFlying = false;
                break;
        }
    }
    // Update is called once per frame

    private Vector3 getVelocity(Vector3 direction)
    {  

        /*if (Vector3.Distance(player.transform.position, direction) > 4f)
            velocity = (direction - player.transform.position).normalized * speed;
        else*/
            velocity = (direction - player.transform.position);
        return velocity;
    }


    void Update () {

        if(player == null)
            player = GameObject.Find(PhotonNetwork.player.NickName);

        if (isRightFlying && isLeftFlying)
        {
            /*if (Vector3.Distance(player.transform.position, (this.leftLoc + this.rightLoc)/2) > 2f)
                velocity = (((this.leftLoc + this.rightLoc) / 2) - player.transform.position).normalized * 10f;
            else
                velocity = (((this.leftLoc + this.rightLoc) / 2) - player.transform.position);*/

            bp.ApplyBodyVelocity(getVelocity(((this.leftLoc + this.rightLoc) / 2)), true, true);

        }
        else if (isRightFlying)
        {
            /*bp.ApplyBodyVelocity((this.rightLoc - player.transform.position) , true, true);
            if (Vector3.Distance(player.transform.position, this.rightLoc) > 2f)
                velocity = (this.rightLoc - player.transform.position).normalized * 10f;
            else
                velocity = (this.rightLoc - player.transform.position);*/
            bp.ApplyBodyVelocity(getVelocity(this.rightLoc), true, true);
        }
        else if (isLeftFlying)
        {
            /*if (Vector3.Distance(player.transform.position, this.rightLoc) > 2f)
                velocity = (this.leftLoc - player.transform.position).normalized * 10f;
            else
                velocity = (this.leftLoc - player.transform.position);*/
            bp.ApplyBodyVelocity(getVelocity(this.leftLoc), true, true);
        }
		
	}
}
