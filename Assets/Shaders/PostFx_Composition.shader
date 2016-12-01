Shader "PostFx/Composition"
{
    Properties
    {
        _MainTex( "", any ) = "" {}
    }

    SubShader
    {
        Tags{ "RenderType" = "Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
                #pragma vertex vert_img // vertex shader postfx par defaut
                #pragma fragment frag

                #include "UnityCG.cginc"    // v2f_img struct

                sampler2D _MainTex;
                sampler2D PostFxTex;
                float4 frag( v2f_img IN ) : COLOR
                {
                    float4 mainTex = tex2D( _MainTex, IN.uv );

                    float2 UV = IN.uv;
                    UV.y = 1.0 - UV.y;

                    mainTex += tex2D( PostFxTex, UV );

                    return mainTex;
                }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
