using UnityEngine;

public class ViveController : PlayerController
{
    SteamVR_TrackedController m_controllerLeft;     // ID = 0
    SteamVR_TrackedController m_controllerRight;    // ID = 1

    void Start()
    {
        m_controllerLeft = transform.FindChild("Controller (left)").GetComponent<SteamVR_TrackedController>();
        m_controllerRight = transform.FindChild("Controller (right)").GetComponent<SteamVR_TrackedController>();

        m_controllerLeft.PadClicked += onPadClickedLeft;            // TP init
        m_controllerLeft.PadUnclicked += onPadUnClickedLeft;        // TP launch
        m_controllerRight.PadClicked += onPadClickedRight;          // Take obj
        m_controllerRight.PadUnclicked += onPadUnClickedRight;      // Drop obj

        m_controllerLeft.TriggerClicked += onTriggerClicked;        // interract
        //m_controllerRight.TriggerClicked += onTriggerClicked;

        m_controllerLeft.MenuButtonClicked += onMenuClickLeft;      // Main menu
        m_controllerRight.MenuButtonClicked += onMenuClickRight;    // Inventory menu
    }
    
    void onPadClickedLeft(object sender, ClickedEventArgs e)
    {
    }
    void onPadUnClickedLeft(object sender, ClickedEventArgs e)
    {
    }


    void onPadClickedRight(object sender, ClickedEventArgs e)
    {
        if(Target)
        HoldState = true;
    }
    void onPadUnClickedRight(object sender, ClickedEventArgs e)
    {
        HoldState = false;
    }
    
    void onTriggerClicked(object sender, ClickedEventArgs e)
    {
        m_isTrigger = true;
    }

    void onMenuClickLeft(object sender, ClickedEventArgs e)
    {
        
    }
    void onMenuClickRight(object sender, ClickedEventArgs e)
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
    }

    public override void updateTarget()
    {
    }

    private bool m_isTrigger = false;

    private bool m_isInventoryOpen = false;
    protected override void eventHandler()
    {

    }
}
