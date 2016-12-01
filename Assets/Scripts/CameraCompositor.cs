using System;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

class CameraCompositor : PostEffectsBase
{
    public  RenderTexture   m_fxCameraRenderTex = null;
    public  Shader          m_postFxShader      = null;
    private Material        m_postFxMat         = null;

    public override bool CheckResources()
    {
        CheckSupport( true );
        
        m_postFxMat = CheckShaderAndCreateMaterial( m_postFxShader, m_postFxMat );

        if ( !isSupported ) {
            ReportAutoDisable();
        }
        
        m_fxCameraRenderTex.width = Screen.width;
        m_fxCameraRenderTex.height = Screen.height;

        return isSupported;
    }
    
    void OnRenderImage( RenderTexture source, RenderTexture destination )
    {
        m_postFxMat.SetTexture( "PostFxTex", m_fxCameraRenderTex );
        Graphics.Blit( source, destination, m_postFxMat );
    }
}
