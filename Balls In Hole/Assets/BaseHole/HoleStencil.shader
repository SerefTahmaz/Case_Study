//Simple lambert shader with stencil buffer for hole
Shader "Seref/HoleStencil" {
	Properties {
		_MainTex ("Diffuse", 2D) = "white" {}
	}
	SubShader {
		//Hole is drawn before basecube
		Tags { "Queue"="Geometry-1" }

		//Turns off rendering to all color channels
		ColorMask 0
		//Hole itself does not any depth
		ZWrite off
		//Stencil Buffer
		Stencil
		{
			//The value to be compared against, 
			//and the value to be written to the buffer
			Ref 1
			//Compare reference and current value in stencil buffer and always pass
			Comp always
			//Write the reference value into the buffer.
			Pass replace
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
