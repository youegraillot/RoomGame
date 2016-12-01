Shader "PostFx/Silouhette" 
{
    Properties 
    {
        _MainTex( "", any )             = "" {}
        _DeltaX( "Delta X", Float ) = 0.01
        _DeltaY( "Delta Y", Float ) = 0.01
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
                float _DeltaX;
                float _DeltaY;

                // https://en.wikipedia.org/wiki/Sobel_operator
                float sobel( sampler2D tex, float2 uv )
                {
                    float2 delta = float2( _DeltaX, _DeltaY );

                    float4 hr = float4( 0, 0, 0, 0 );
                    float4 vt = float4( 0, 0, 0, 0 );

                    hr += tex2D( tex, ( uv + float2( -1.0, -1.0 ) * delta ) ) *  1.0;
                    hr += tex2D( tex, ( uv + float2( 0.0, -1.0 ) * delta ) ) *  0.0;
                    hr += tex2D( tex, ( uv + float2( 1.0, -1.0 ) * delta ) ) * -1.0;
                    hr += tex2D( tex, ( uv + float2( -1.0,  0.0 ) * delta ) ) *  2.0;
                    hr += tex2D( tex, ( uv + float2( 0.0,  0.0 ) * delta ) ) *  0.0;
                    hr += tex2D( tex, ( uv + float2( 1.0,  0.0 ) * delta ) ) * -2.0;
                    hr += tex2D( tex, ( uv + float2( -1.0,  1.0 ) * delta ) ) *  1.0;
                    hr += tex2D( tex, ( uv + float2( 0.0,  1.0 ) * delta ) ) *  0.0;
                    hr += tex2D( tex, ( uv + float2( 1.0,  1.0 ) * delta ) ) * -1.0;

                    vt += tex2D( tex, ( uv + float2( -1.0, -1.0 ) * delta ) ) *  1.0;
                    vt += tex2D( tex, ( uv + float2( 0.0, -1.0 ) * delta ) ) *  2.0;
                    vt += tex2D( tex, ( uv + float2( 1.0, -1.0 ) * delta ) ) *  1.0;
                    vt += tex2D( tex, ( uv + float2( -1.0,  0.0 ) * delta ) ) *  0.0;
                    vt += tex2D( tex, ( uv + float2( 0.0,  0.0 ) * delta ) ) *  0.0;
                    vt += tex2D( tex, ( uv + float2( 1.0,  0.0 ) * delta ) ) *  0.0;
                    vt += tex2D( tex, ( uv + float2( -1.0,  1.0 ) * delta ) ) * -1.0;
                    vt += tex2D( tex, ( uv + float2( 0.0,  1.0 ) * delta ) ) * -2.0;
                    vt += tex2D( tex, ( uv + float2( 1.0,  1.0 ) * delta ) ) * -1.0;

                    return sqrt( hr * hr + vt * vt );
                }

                float4 frag( v2f_img IN ) : COLOR
                {
                    float s = sobel( _MainTex, IN.uv );
                    return float4( s, s, s, s ) * ( float4( 1.0, 1.0, 1.0, 1.0 ) );
                }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
