using System;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

class CameraRenderer : PostEffectsBase
{
    public  Shader   m_postFxShader = null;
    private Material m_postFxMat    = null;

    public override bool CheckResources()
    {
        CheckSupport( true );

        m_postFxMat = CheckShaderAndCreateMaterial( m_postFxShader, m_postFxMat );

        if ( !isSupported ) {
            ReportAutoDisable();
        }

        return isSupported;
    }

    void OnRenderImage( RenderTexture source, RenderTexture destination )
    {
        Graphics.Blit( source, destination, m_postFxMat );
    }
}
