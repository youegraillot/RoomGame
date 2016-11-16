using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class DrawerControl : MoveableObject {

    public Vector3 openingDirection = new Vector3(0,0,1);
    public float openingForce = 0.25f;

    
    
	// Use this for initialization
	void Start () {
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        
	}
   
    public override void moveTo(Vector3 newPosition)
    {
        m_rigidbody.AddForce(openingDirection * (newPosition.y) * openingForce);
    }
    public override void rotate(Quaternion newRotation)
    {
        
    }
    

   
    void FixedUpdate () {
	    
    }
   
}
