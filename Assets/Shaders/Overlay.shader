Shader "Custom/Overlay" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Main Texture", 2D) = "white" {}
		_Overlay ("Overlay Texture", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Ratio("Ratio", Range(0,1)) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		Blend SrcAlpha OneMinusSrcAlpha
		Lighting On
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Overlay;
		float _Ratio;
		struct Input {
			float2 uv_MainTex;
			float2 uv_Overlay;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 background = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 solution = tex2D (_Overlay, IN.uv_Overlay) * _Color;

			o.Albedo = background.rgb * (1 -solution.a*_Ratio) + solution.rgb * _Ratio;

			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = background.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}