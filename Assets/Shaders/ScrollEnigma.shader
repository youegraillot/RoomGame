Shader "Custom/ScrollEnigma" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_TextTex ("Text display", 2D) = "white" {}
		_SolutionTex ("Solution display", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_ProximityLight("Distance from light", Range(0,1)) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		Blend SrcAlpha OneMinusSrcAlpha
		Lighting On
		//Pass{
		//	Material
		//	{
		//		Diffuse[_Color]
		//	}
		//	Lighting On
		//	SetTexture[_MainTex]{
		//		combine texture 
		//	}
			
		//	SetTexture[_TextTex]{
		//		combine texture  lerp(texture) previous
		//	}
			
			
			
				
		//}
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _TextTex;
		sampler2D _SolutionTex;
		float _ProximityLight;
		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) + tex2D (_TextTex, IN.uv_MainTex) * _Color;
			fixed4 text = tex2D (_TextTex, IN.uv_MainTex) * _Color;
			fixed4 solution = tex2D (_SolutionTex, IN.uv_MainTex) * _Color;
			
			o.Albedo = c.rgb * (1-text.a - solution.a * _ProximityLight) + text.rgb * text.a + solution.rgb*solution.a * _ProximityLight;//(text.rgb*text.a) ;//+c.rgb ;// * (solution.rgb*solution.a);
		
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}