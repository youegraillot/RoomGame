using UnityEngine;
using System.Collections;
using System;
using Valve.VR;


[RequireComponent(typeof(SteamVR_TrackedController))]
[RequireComponent(typeof(CapsuleCollider))]
public class ViveController : PlayerController
{
    void OnCollisionEnter(Collision other)
    {
      /*  if(other.gameObject.layer == LayerMask.NameToLayer(""))
        {
            Target = other.gameObject.GetComponent<InteractiveObject>();
        }*/
        if(other.gameObject.GetComponent<InteractiveObject>())
        {
            Target = other.gameObject.GetComponent<InteractiveObject>();
        }
    }
    void OnCollisionExit(Collision other)
    {
      /*  if(other.gameObject.layer == LayerMask.NameToLayer(""))
        {
            Target = null;
        }*/

        if (other.gameObject.GetComponent<InteractiveObject>() && Target)
        {
            Target = null;

        }
    }
    
    private SteamVR_TrackedController m_stickController;
    void OnEnable()
    {
        m_stickController = this.GetComponent<SteamVR_TrackedController>();

        m_stickController.MenuButtonUnclicked += onMenuUnclick;
        m_stickController.TriggerClicked += onTriggerClicked;
        m_stickController.TriggerUnclicked += onTriggerUnClicked;
        m_stickController.PadClicked += onPadClicked;
    }

    void OnDisable()
    {
        m_stickController.MenuButtonUnclicked -= onMenuUnclick;
        m_stickController.TriggerClicked -= onTriggerClicked;
        m_stickController.TriggerUnclicked -= onTriggerUnClicked;
        m_stickController.PadClicked -= onPadClicked;
    }

    void onPadClicked(object sender, ClickedEventArgs e)
    {

    }
    void onTriggerUnClicked(object sender, ClickedEventArgs e)
    {
        m_isTrigger = false;
        if (Target)
        {
            Target = null;
        }
    }
    void onTriggerClicked(object sender, ClickedEventArgs e)
    {
        m_isTrigger = true;
    }
    void onMenuUnclick(object sender, ClickedEventArgs e)
    {
        
    }

    public override void drawObject()
    {
    }

    public override void pickFromInventoryCallBack()
    {
    }

    public override void rotateObject()
    {
        Quaternion newAngle = m_stickController.transform.rotation;
        //newAngle = Quaternion.Inverse(newAngle);
        Target.transform.rotation = newAngle;
        //((MovableObject)Target).rotate(newAngle);
    }

    public override void updateTarget()
    {
        if(m_isTrigger && Target)
        {
            HoldState = true;
            RotateState = true;
        }
        else
        {
            HoldState = false;
            RotateState = false;
        }
    }

    private bool m_isTrigger = false;

    private bool m_isInventoryOpen = false;
    protected override void eventHandler()
    {

    }
}
