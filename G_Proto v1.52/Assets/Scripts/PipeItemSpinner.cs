using UnityEngine;
using System.Collections;

public class PipeItemSpinner : MonoBehaviour {
    public float SpinSpeed = 0;
    private Transform HostObject;
	// Use this for initialization
	void Start ()
    {

        HostObject = gameObject.transform;

    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //print("sgs");
        //HostObject.rotation.SetEulerAngles(HostObject.rotation.x + SpinSpeed, HostObject.rotation.y, HostObject.rotation.z);
        HostObject.Rotate(HostObject.localRotation.x + SpinSpeed, 0, 0);
	}
}
