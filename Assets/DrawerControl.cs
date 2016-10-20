using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class DrawerControl : MoveableObject {

    public Vector3 openingDirection = new Vector3(0,0,1);
    public float openingForce = 0.25f;
    private Rigidbody rgbd;
    private GameObject player;
    private bool isGrab = false;
    private Vector3 previousControllerPos;
	// Use this for initialization
	void Start () {
        rgbd = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if(Input.GetMouseButtonDown(0))
        {
            isGrab = true;
            previousControllerPos = Input.mousePosition;
            Cursor.lockState = CursorLockMode.None;
        }

        else if(Input.GetMouseButtonUp(0))
        {
            isGrab = false;
            rgbd.velocity = Vector3.zero;
            player.GetComponent<RigidbodyFirstPersonController>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (isGrab)
        {
            
            player.GetComponent<RigidbodyFirstPersonController>().enabled = false;
            if (previousControllerPos.y != Input.mousePosition.y)
            {
                rgbd.AddForce(openingDirection * (Input.mousePosition.y - previousControllerPos.y) * openingForce);
                
                previousControllerPos = Input.mousePosition;
            }
           
        }
    }
   
}
