//Simple lambert shader with stencil buffer for basecube
Shader "Seref/BaseCubeStencil" {
	Properties {
		_MainTex ("Diffuse", 2D) = "white" {}
	}
	SubShader {
		//Draw after hole
		Tags { "Queue"="Geometry" }		
		//Stencil Buffer
		//If you do find a one in the stencil buffer for this pixel,
		//then do not draw it, otherwise draw it
		Stencil
		{
			//The value to be compared against, 
			//and the value to be written to the buffer
			Ref 1
			//Compare reference and current value in stencil buffer and always pass
			Comp notequal
			//Write a value into the buffer.
			Pass keep
		}

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
