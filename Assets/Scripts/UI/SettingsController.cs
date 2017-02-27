using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
public class SettingsController : MonoBehaviour
{
    [SerializeField] MainMenu m_mainMenuBehaviour;
    enum UICompName
    {
        GraphicsQual,
        Resolution,
        FullScreen,
        AA,
        ShadowQuality,
        TextureQuality,
        Halo,
        Reticle,
        Volume
    }
    //1 : true , 2 : false
    public Toggle fullScreen;
    public Toggle halo;
    public Toggle reticle;

    public Slider volume;//0-100

    public Dropdown graphicsQuality;//value 0 : Ultra 1 : normale 2: basse
    public Dropdown resolution;
    public Dropdown antiAliasing;
    public Dropdown shadowQuality;
    public Dropdown textureQuality;

    Settings m_settings;
    #region UIEvents
    void Start()
    {
        m_settings = Settings.Instance;
        setUpView();
        graphicsQuality.onValueChanged.AddListener(delegate { ValueChange(graphicsQuality.value, UICompName.GraphicsQual); });
        resolution.onValueChanged.AddListener(delegate { ValueChange(resolution.value, UICompName.Resolution); });
        antiAliasing.onValueChanged.AddListener(delegate { ValueChange(antiAliasing.value, UICompName.AA); });
        shadowQuality.onValueChanged.AddListener(delegate { ValueChange(shadowQuality.value, UICompName.ShadowQuality); });
        textureQuality.onValueChanged.AddListener(delegate { ValueChange(textureQuality.value, UICompName.TextureQuality); });
        halo.onValueChanged.AddListener(delegate { ValueChange(Convert.ToInt32(halo.isOn), UICompName.Halo); });
        fullScreen.onValueChanged.AddListener(delegate { ValueChange(Convert.ToInt32(fullScreen.isOn), UICompName.FullScreen); });
        reticle.onValueChanged.AddListener(delegate { ValueChange(Convert.ToInt32(reticle.isOn), UICompName.Reticle); });
        volume.onValueChanged.AddListener(delegate { ValueChange(Convert.ToInt32(volume.value), UICompName.Volume); });

    }

    void setUpView()
    {
        graphicsQuality.ClearOptions();
        foreach(string str in m_settings.getPresetQ())
            graphicsQuality.options.Add(new Dropdown.OptionData(str));

        resolution.ClearOptions();
        foreach(string str in m_settings.getResolutions())
            resolution.options.Add(new Dropdown.OptionData(str));

        antiAliasing.ClearOptions();
        foreach(string str in m_settings.getAAQ())
            antiAliasing.options.Add(new Dropdown.OptionData(str));

        textureQuality.ClearOptions();
        foreach(string str in m_settings.getTextureQ())
            textureQuality.options.Add(new Dropdown.OptionData(str));

        shadowQuality.ClearOptions();
        foreach (string str in m_settings.getShadowQ())
            shadowQuality.options.Add(new Dropdown.OptionData(str));

        RefreshView();
    }

    void RefreshView()
    {
        volume.value = m_settings.m_controlerProxyData.m_volume;
        halo.isOn = m_settings.m_controlerProxyData.m_halo;
        reticle.isOn = m_settings.m_controlerProxyData.m_reticle;

        shadowQuality.value = m_settings.m_controlerProxyData.m_qShadow;
        shadowQuality.RefreshShownValue();

        textureQuality.value = m_settings.m_controlerProxyData.m_qTexture;
        textureQuality.RefreshShownValue();

        resolution.value = m_settings.m_controlerProxyData.m_resolution;
        resolution.RefreshShownValue();

        antiAliasing.value = m_settings.m_controlerProxyData.m_aa;
        antiAliasing.RefreshShownValue();

        graphicsQuality.value = m_settings.m_controlerProxyData.m_qGraphic;
        graphicsQuality.RefreshShownValue();
    }

    void ValueChange(int value, UICompName name)
    {
        switch (name)
        {
            case UICompName.GraphicsQual:
                m_settings.PresetQ(value);
                //m_settings.m_controlerProxyData..m_qGraphic = value;
                break;
            case UICompName.Resolution:
                break;
            case UICompName.FullScreen:
                if (value == 1)
                    m_settings.m_controlerProxyData.m_fullscreen = true;
                else
                    m_settings.m_controlerProxyData.m_fullscreen = false;
                break;
            case UICompName.AA:
                m_settings.m_controlerProxyData.m_aa = value;
                if (value != m_settings.m_controlerProxyData.m_qGraphic)
                {
                    m_settings.m_controlerProxyData.m_qGraphic = m_settings.getPresetQ().Count - 1;//custom Quality
                }
                break;
            case UICompName.ShadowQuality:
                m_settings.m_controlerProxyData.m_qShadow = value;
                if (value != m_settings.m_controlerProxyData.m_qGraphic)
                {
                    m_settings.m_controlerProxyData.m_qGraphic = m_settings.getPresetQ().Count - 1;//custom Quality
                }
                break;
            case UICompName.TextureQuality:
                m_settings.m_controlerProxyData.m_qTexture = value;
                if (value != m_settings.m_controlerProxyData.m_qGraphic)
                {
                    m_settings.m_controlerProxyData.m_qGraphic = m_settings.getPresetQ().Count - 1;//custom Quality
                }
                break;
            case UICompName.Halo:
                if (value == 1)
                    m_settings.m_controlerProxyData.m_halo = true;
                else
                    m_settings.m_controlerProxyData.m_halo = false;
                break;
            case UICompName.Reticle:
                if (value == 1)
                    m_settings.m_controlerProxyData.m_reticle = true;
                else
                    m_settings.m_controlerProxyData.m_reticle = false;
                break;
            case UICompName.Volume:
                m_settings.m_controlerProxyData.m_volume = value;
                break;
            default:
                break;
        }
        RefreshView();
    }


    public void applyChange()
    {
        m_settings.applySettings();
        if (m_mainMenuBehaviour)
            m_mainMenuBehaviour.backToMain();
        else
            this.gameObject.SetActive(false);
    }
    void OnEnable()
    {
        m_settings = Settings.Instance;
        RefreshView();
    }
    public void resetSettings()
    {
        m_settings.PresetQ(1);
        m_settings.m_controlerProxyData.m_halo = true;
        m_settings.m_controlerProxyData.m_reticle = true;
        m_settings.m_controlerProxyData.m_volume = 100;
        m_settings.m_controlerProxyData.m_fullscreen = true;
        RefreshView();
    }

    public void Cancel()
    {
        m_settings.m_controlerProxyData = m_settings.getCurrentSettings();
        if (m_mainMenuBehaviour)
            m_mainMenuBehaviour.backToMain();
        else
            this.gameObject.SetActive(false);
    }
    #endregion
}
