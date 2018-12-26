using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GazPropulsion : MonoBehaviour {
    private VRTK_BodyPhysics bp;
    private new Transform camera;
    public GameObject soundsManager;
    private AudioSource[] sounds;
    public ParticleSystem smokeTrail;
    public float speed;

    private bool isSoundPlaying;
    private bool isSmokePlaying;
    // Use this for initialization
    void Start () {
        bp = GameObject.FindGameObjectWithTag("PlayArea").GetComponent<VRTK_BodyPhysics>();
        sounds = soundsManager.GetComponents<AudioSource>();
        smokeTrail.Stop();
    }
	
	// Update is called once per frame
	void Update () {
        isSmokePlaying = smokeTrail.isPlaying;
        isSoundPlaying = sounds[3].isPlaying;
        
        if (GetComponent<VRTK_ControllerEvents>().touchpadPressed)
        {   
            Debug.Log("BURST " + isSmokePlaying + " - " + isSoundPlaying);
            if(!isSmokePlaying)
                smokeTrail.Play();
            if(!isSoundPlaying)
                sounds[3].Play();

            camera = VRTK_DeviceFinder.HeadsetCamera();

            bp.ApplyBodyVelocity(camera.forward * speed,true,true);
        }
        else if (isSoundPlaying || isSmokePlaying)
        {
            Debug.Log(isSmokePlaying + " - " + isSoundPlaying);
            if (!isSoundPlaying)
                sounds[3].Stop();
            if (isSmokePlaying)
                smokeTrail.Stop();
        }

        
	}
}
