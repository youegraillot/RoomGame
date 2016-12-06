Shader "Unlit/flame"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_OutlineColor("Outline color ", Color) = (0,0,1,1)
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
			float4 normal : NORMAL;
		};

		sampler2D _MainTex;
		float4 _MainTex_ST;
		float4 _OutlineColor;
	ENDCG
	SubShader
	{
		Tags { "RenderType"="Transparent" }
		LOD 100
		Pass
		{
			Name "DISPLACEMENT"
			Cull Back
			ZWrite On
			ZTest LEqual
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			v2f vert (appdata v)
			{
				v2f o;
				v.vertex.xz /= lerp(1,10,v.uv.y );
				v.vertex.x += lerp(0,1,v.uv.y )/10 * sin(_Time.y);
				v.vertex.z += lerp(0,1,v.uv.y )/10 * cos(_Time.y*lerp(1,1.01,v.uv.y ));
				//v.normal.xz /= lerp(1,10,v.uv.y );
				//v.normal.x += lerp(0,1,v.uv.y )/10 * sin(_Time.y);
				//v.normal.z += lerp(0,1,v.uv.y )/10 * cos(_Time.y*lerp(1,1.01,v.uv.y ));
				//half3 norm = mul((half3x3)UNITY_MATRIX_IT_MV, v.normal);
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				//o.normal.xyz = norm.xyz;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				col.rgb = (0,0,0);
				//col.b = lerp(0,255,abs(i.uv.y/255));
				//col.a = lerp(0,255,abs(i.uv.y));
				col.rgb = (i.uv.y, i.uv.y, 1-i.uv.y); 

				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
		
		//Pass
		//{
		//	Name "OUTLINE"
		//	Cull Front
		//	CGPROGRAM
		//	#pragma vertex vert
		//	#pragma fragment frag
		//	// make fog work
		//	#pragma multi_compile_fog
		//	v2f vert (appdata v)
		//	{
		//		v2f o;
		//		o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
		//		half3 norm = mul((half3x3)UNITY_MATRIX_IT_MV, v.normal);
		//		half2 offset = TransformViewToProjection(norm.xy);
		//		o.vertex.xy += offset * o.vertex.z * 1/1000000000;
				
		//		return o;
		//	}
			
		//	fixed4 frag (v2f i) : SV_Target
		//	{
		//		// sample the texture
		//		fixed4 col = _OutlineColor;
				

		//		UNITY_APPLY_FOG(i.fogCoord, col);
		//		return col;
		//	}
		//	ENDCG
		//}
		
		
	}
}
