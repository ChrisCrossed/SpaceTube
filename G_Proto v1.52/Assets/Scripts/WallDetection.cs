using UnityEngine;
using System.Collections;

public class WallDetection : MonoBehaviour
{
    public GameObject Target;
    private Vector3 Direction;
    public bool DebugDraw;
    private Transform Obstruction;
	// Use this for initialization
	void Start ()
    {
        Obstruction = null;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Direction = Target.transform.position - transform.position;
        RaycastHit HitInfo;
        Physics.Raycast(transform.position, Direction, out HitInfo);
        
        if(HitInfo.transform.name != Target.name && HitInfo.transform.GetComponent<Renderer>())
        {
            Obstruction = HitInfo.transform;
            Color color = HitInfo.transform.GetComponent<Renderer>().material.color;
            color.a = 0.5f;
            HitInfo.transform.GetComponent<Renderer>().material.color = color;
        }
        else if(HitInfo.transform.name == Target.name && Obstruction)
        {
            Obstruction.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        }

        if(DebugDraw)
        {
            Debug.DrawRay(transform.position, Direction);
        }
    }
}
