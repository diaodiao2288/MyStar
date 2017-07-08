Shader "Custom/MyTerrainShader" {
	Properties {
		//_Color ("Color", Color) = (1,1,1,1)
		//_Glossiness ("Smoothness", Range(0,1)) = 0.5
		//_Metallic ("Metallic", Range(0,1)) = 0.0
		_MainTex("Base (RGB)", 2D) = "white" {}
	    _SecondTex("Second(RGB)", 2D) = "white" {}
		_ThirdTex("Third(RGB)", 2D) = "white" {}
		_FourthTex("Fourth(RGB)", 2D) = "white" {}
		_Mask("Mask(RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 4.0

		sampler2D _MainTex;
		sampler2D _SecondTex;
		sampler2D _ThirdTex;
		sampler2D _FourthTex;
		sampler2D _Mask;


		struct Input {
			float2 uv_MainTex;
			float2 uv_SecondTex;
			float2 uv_ThirdTex;
			float2 uv_FourthTex;
			float2 uv_Mask;
		};


		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			//fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			//o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			//o.Metallic = _Metallic;
			//o.Smoothness = _Glossiness;
			//o.Alpha = c.a;
			half4 c1 = tex2D(_MainTex, IN.uv_MainTex);
			half4 c2 = tex2D(_SecondTex, IN.uv_SecondTex);
			half4 c3 = tex2D(_ThirdTex, IN.uv_ThirdTex);
			half4 c4 = tex2D(_FourthTex, IN.uv_FourthTex);
			half4 cm = tex2D(_Mask, IN.uv_Mask);
			o.Albedo = c1.rgb * cm.r + c2.rgb * cm.g + c3.rgb * cm.b + c4.rgb * cm.a;
			o.Alpha = c1.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
