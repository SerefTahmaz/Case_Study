//Simple wave vertex shader
//Wrote for water but used in this as lava by changing texture and scrolling speed
Shader "Custom/Wave"
{
    Properties
    {
        _MainTex("Water", 2D) = "white" {}
        _Tint("Colour Tint", Color) = (1,1,1,1)
        _Freq("Frequency", Range(0,5)) = 3
        _Speed("Speed", Range(0,100)) = 10
        _Amp("Amplitude", Range(0,1)) = 0.5
        _FoamTex("Foam", 2D) = "white" {}
        _ScrollX("Scroll X", Range(-5, 5)) = 1
        _ScrollY("Scroll Y", Range(-5, 5)) = 1
    }
    SubShader
    {
        
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert vertex:vert alpha:fade

        

        struct Input
        {
            float2 uv_MainTex;
            float3 vertColor;
        };

        float4 _Tint;
        float _Freq;
        float _Speed;
        float _Amp;
        sampler2D _FoamTex;
        float _ScrollX;
        float _ScrollY;
        struct appdata {
            float4 vertex: POSITION;
            float3 normal: NORMAL;
            float4 texcoord: TEXCOORD0;
            float4 texcoord1 : TEXCOORD1;
            float4 texcoord2 : TEXCOORD2;
        };
        void vert (inout appdata v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            //As time as input for moving vertices
            float t = _Time * _Speed;
            //For wave effect, Vertices positions manipulated via sin sums
            //From high amplitude and low frequency sin functions to 
            //Low amplitude and high frequency sin functions
            float waveHeight = sin(t + v.vertex.x * _Freq) * _Amp + 
                sin(t + v.vertex.x * _Freq * 4) * _Amp/4 + 
                sin(t + v.vertex.x * _Freq * 8) * _Amp/8 + 
                sin(t + v.vertex.x * _Freq * 16) * _Amp/16;
            //Up direction
            v.vertex.y = v.vertex.y + waveHeight;
            v.normal = normalize(float3(v.normal.x + waveHeight, v.normal.y,
                v.normal.z));
            o.vertColor = waveHeight + 2;
        }
        
        sampler2D _MainTex;
        void surf(Input IN, inout SurfaceOutput o)
        {
            //Scrolling value in x axis according to time
            _ScrollX *= _Time;
            //Scrolling value in y axis according to time
            _ScrollY *= _Time;
            //Scrolling textures uv's by time
            float3 water = (tex2D(_MainTex, IN.uv_MainTex +
                float2(_ScrollX, _ScrollY))).rgb;
            float3 foam = (tex2D(_FoamTex, IN.uv_MainTex +
                float2(_ScrollX / 2.0, _ScrollY / 2.0))).rgb;
            //Set albedo water and foam average
            o.Albedo = (water + foam) / 2.0;
            o.Albedo *= IN.vertColor.rgb/1.2;
            //Set alpha for transparence
            o.Alpha = _Tint.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
