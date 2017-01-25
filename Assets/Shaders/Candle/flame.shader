Shader "Unlit/flame"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_OutlineColor("Outline color ", Color) = (0,0,1,1)
		_OutlineSize("Outline Size", Range(0,3)) = 0.2
		_DisplacementIntensity("Displacement Instensity", Range(0,30)) = 10
	}
	CGINCLUDE
		#include "UnityCG.cginc"
		struct appdata
		{
			float4 vertex : POSITION;
			float2 uv : TEXCOORD0;
			float4 normal : NORMAL;
		};

		struct v2f
		{
			float2 uv : TEXCOORD0;
			UNITY_FOG_COORDS(1)
			float4 vertex : SV_POSITION;
			float4 localPos : TEXCOORD1;
			float4 normal : NORMAL;
		};

		sampler2D _MainTex;
		float4 _MainTex_ST;
		float4 _OutlineColor;
		float _OutlineSize;
		float _DisplacementIntensity;
	ENDCG
	SubShader
	{
		Tags  {"Queue"="Transparent" "RenderType" = "Transparent" }
		
		LOD 100
		
		Pass
		{
			Name "OUTLINE"
			
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Front
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			v2f vert (appdata v)
			{
				v2f o;
				v.vertex.xz /= lerp(1,10,v.uv.y );
				v.vertex.x += lerp(0,1,v.uv.y )/(30-_DisplacementIntensity) * sin(_Time.y);
				v.vertex.z += lerp(0,1,v.uv.y )/(30-_DisplacementIntensity) * cos(_Time.y*lerp(1,1+1/_DisplacementIntensity,v.uv.y ));
				o.localPos = v.vertex;
				v.vertex += v.normal * _OutlineSize;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);			
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = _OutlineColor;
				col.a = 0.1;// lerp(col.a, 0, i.localPos.y);
				return col;
			}
			ENDCG
		}
		Pass
		{
			Name "DISPLACEMENT"
			
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			v2f vert (appdata v)
			{
				v2f o;
				o.localPos = v.vertex;
				v.vertex.xz /= lerp(1,10,v.uv.y );
				v.vertex.x += lerp(0,1,v.uv.y )/(30-_DisplacementIntensity) * sin(_Time.y);
				v.vertex.z += lerp(0,1,v.uv.y )/(30-_DisplacementIntensity) * cos(_Time.y*lerp(1,1+1/_DisplacementIntensity,v.uv.y ));
				
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
			
				col = lerp(fixed4(1,1,0, 0.8), fixed4(1,0,0,0), i.localPos.y);
				return col;
			}
			ENDCG
		}
		
		
		

		
		
		
	}
}