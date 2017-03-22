using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

[System.Serializable]
public struct SettingsData
{
    public bool m_halo;
    public bool m_reticle ;
    public bool m_fullscreen;
    public int m_volume;
    //public int m_resolutionX = Screen.currentResolution.width;
    //public int m_resolutionY = Screen.currentResolution.height;
    public int m_resolution;
    public int m_aa;    //possible value : 0,2,4,8
    public int m_qGraphic;
    public int m_qShadow;
    public int m_qTexture;
}


public class Settings : MonoBehaviour
{
    public List<string> getResolutions()
    {
        List<string> resList = new List<string>();
        foreach(Resolution res in Screen.resolutions)
        {
            resList.Add(res.width.ToString()+"x"+res.height.ToString());
        }
        return resList;
    }

    public float getVolume() {return m_controlerProxyData.m_volume;}

    public bool getHalo(){ return m_controlerProxyData.m_halo; }

    public bool getReticle() { return m_controlerProxyData.m_reticle; }

    public List<string> getTextureQ()
    {
        List<string> texQList = new List<string>();
        texQList.Add("Low");
        texQList.Add("Medium");
        texQList.Add("High");
        return texQList;
    }

    public List<string> getAAQ()
    {
        return new List<string>() { "x0", "x2", "x4", "x8" };
    }

    public List<string>getPresetQ()
    {
        return new List<string>() { "Low", "Medium", "High", "Custom" };
    }

    public List<string>getShadowQ()
    {
        return new List<string>() { "Low", "Medium", "High" };
    }

    SettingsData m_settingsData;


    public SettingsData m_controlerProxyData = new SettingsData();
    public SettingsData getCurrentSettings() { return m_settingsData; }

   


    public void PresetQ(int value)
    {
        switch (value)
        {
            case 0:
                m_controlerProxyData.m_qGraphic = 0;
                m_controlerProxyData.m_qTexture = 0;
                m_controlerProxyData.m_volume = 100;
                m_controlerProxyData.m_qShadow = 0;
                m_controlerProxyData.m_aa = 0;
                break;
            case 1:
                m_controlerProxyData.m_qGraphic = 1;
                m_controlerProxyData.m_qTexture = 1;
                m_controlerProxyData.m_volume = 100;
                m_controlerProxyData.m_qShadow = 1;
                m_controlerProxyData.m_aa = 1;
                break;
            case 2:
                m_controlerProxyData.m_qGraphic = 2;
                m_controlerProxyData.m_qTexture = 2;
                m_controlerProxyData.m_volume = 100;
                m_controlerProxyData.m_qShadow = 2;
                m_controlerProxyData.m_aa = 2;
                break;
            default:
                m_controlerProxyData.m_qGraphic = 3;
                break;
        }
    }
    void Awake()
    {
        m_filename = Application.persistentDataPath + "/Player.settings";
        DontDestroyOnLoad(this.gameObject);
        m_settingsData = new SettingsData();
        m_settingsData.m_aa = -1;
        m_settingsData.m_qTexture = -1;
        m_settingsData.m_volume = -1;
        m_settingsData.m_qShadow = -1;
        m_settingsData.m_qGraphic = -1;
        //m_controlerProxyData = new SettingsData();
        loadSettings();
        m_controlerProxyData = m_settingsData;

    }


    string m_filename; 
    private static Settings _instance;
    public static Settings Instance
    {
        get
        {
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(Settings))as Settings;
                if(!_instance)
                {
                    GameObject go = new GameObject("Settings");
                    _instance = go.AddComponent <Settings>();
                }
            }
            return _instance;
        }
    }

   
    void loadSettings()
    {
        if(File.Exists(m_filename))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(m_filename, FileMode.Open);
            SettingsData sData =(SettingsData) bf.Deserialize(file);
            file.Close();
            m_controlerProxyData = sData;

        }
        else
        {
            PresetQ(1);//default quality
        }
        applySettings(false);
    }

    public void applySettings(bool save=true)
    {
        if(m_settingsData.m_aa != m_controlerProxyData.m_aa)
        {
            QualitySettings.antiAliasing =(int)Mathf.Pow(2f,m_controlerProxyData.m_aa);
        }
        if(m_settingsData.m_fullscreen != m_controlerProxyData.m_fullscreen)
        {
            Screen.fullScreen = m_controlerProxyData.m_fullscreen;
        }
        if(m_settingsData.m_halo != m_controlerProxyData.m_halo)
        {
            //change halo
        }
        if(m_settingsData.m_qGraphic != m_controlerProxyData.m_qGraphic)
        {
            //preset qualitée graphique
        }
      
        if(m_settingsData.m_qShadow != m_controlerProxyData.m_qShadow)
        {
            switch(m_controlerProxyData.m_qShadow)
            {
                case 0:
                    QualitySettings.shadowCascades = 0;
                    QualitySettings.shadowDistance = 20;
                    QualitySettings.pixelLightCount = 1;
                    break;
                case 1:
                    QualitySettings.shadowCascades = 2;
                    QualitySettings.shadowDistance = 70;
                    QualitySettings.shadowCascade2Split = 33.3f;
                    QualitySettings.pixelLightCount = 3;

                    break;
                case 2:
                    QualitySettings.shadowCascades = 4;
                    QualitySettings.shadowDistance = 150;
                    QualitySettings.shadowCascade4Split = new Vector3(6.7f, 13.3f, 26.7f);
                    QualitySettings.pixelLightCount = 4;
                    break;
                default:
                    break;
            }
        }
        if(m_settingsData.m_qTexture != m_controlerProxyData.m_qTexture)
        {
            switch(m_controlerProxyData.m_qTexture)
            {
                case 0:
                    QualitySettings.masterTextureLimit = 2;

                    break;
                case 1:
                    QualitySettings.masterTextureLimit = 1;
                    break;
                case 2:
                    QualitySettings.masterTextureLimit = 0;
                    break;
            }
        }
        if(m_settingsData.m_reticle != m_controlerProxyData.m_reticle)
        {
            //change reticle
        }
        if(m_settingsData.m_volume!= m_controlerProxyData.m_volume)
        {
            //change volume
        }
        m_settingsData = m_controlerProxyData;

        if (save)
            saveSettings();
    }

    void saveSettings()
    {
        if (File.Exists(m_filename))
            File.Delete(m_filename);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(m_filename);
        bf.Serialize(file, m_settingsData);
        file.Close();
    }
}