//I wrote this shader in shader graph first. When I learned surface shader. I converted it to a surface shader
//One of super power shaders I wrote
//Mainly fresnel effect used
//Because of using gamma colour space in this app, It is not appering good as when it is in linear colour space
Shader "Custom/Electric"
{
    Properties
    {
        //Fresnel color
        [HDR]_RimColor("Rim Color", Color) = (0,0.5,0.5,0.0)
        //Thickness
        _RimPowerA("Rim Power A", float) = 1.0
        _RimPowerB("Rim Power B", float) = 4.0
        //Color of fresnel
        _AlbedoColor("Albedo Color", Color) = (0.5,0.5,0.5,0)
        //Texture to slide
        _MainTex("Main Texture", 2D) = "white"{}
        //Texture top of main texture
        _ScanTex("Scan Line", 2D) = "white"{}
        //Noise texture for electric effect
        _NoiseTex("Noise", 2D) = "white"{}
        //Electric texture
        _EleTex("Electric Tex", 2D) = "white"{}
        //Scroll
        _ScrollY("Scroll Y", Range(-20,20)) = 1
        //Electric frequency
        _EleFreq("Electric Frequency", float) = 1
        //Electric texture where color intesity is cut off
        _CutoffStart("Cutoff Start", float) = 0.1
        _CutoffEnd("Cutoff End", float) = 0.9
        _Thickness("Thickness", float) = 0.05
        [HDR]_ElectricColor("Electric Color", Color) = (0.0,0.0,1.0,0.0)
        //Speed of pulse
        _RimPulseSpeed("Rim Pulse Speed", float) = 3
    }
        SubShader
    {
            //Transparent so we can see the things behind it
        Tags{
            "Queue" = "Transparent"
        }
            //Depth test
        Pass{
            ZWrite On
            ColorMask 0
            }

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha:fade

        struct Input
        {
            float3 viewDir;
            float2 uv_MainTex;
            float2 uv_NoiseTex;
            float2 uv_ScanTex;
            float2 uv_EleTex;
            float3 worldPos;
        };
        //Getting variables we wrote
        float4 _RimColor;
        float _RimPowerA;
        float _RimPowerB;
        float4 _AlbedoColor;
        float _RimPulseSpeed;
        sampler2D _MainTex;
        sampler2D _NoiseTex;
        sampler2D _ScanTex;
        sampler2D _EleTex;
        float _ScrollY;
        float _EleFreq;
        float _CutoffStart;
        float _CutoffEnd;
        float _Thickness;
        float4 _ElectricColor;
       
        //Unity stadard remap function 
        void Remap_float4(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        //Unity step_float function 
        void Step_float(float Edge, float In, out float Out)
        {
            Out = step(Edge, In);
        }
        //Surface function where I do scrolling texture and color manipulations
        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            //Sinfunction to smooth scroll
            float sinMul = _EleFreq * _Time;
            
            float electricBlue = tex2D(_EleTex, IN.uv_EleTex).b;

            float sinOutElectric;
            
            //Cutoff for rim color
            Remap_float4(sin(sinMul), float2(-1, 1), float2(_CutoffStart, _CutoffEnd), sinOutElectric);

            float stepNative;

            //Apply cut off
            Step_float(electricBlue, sinOutElectric, stepNative);

            //Set electric thickness
            sinOutElectric += _Thickness;

            float stepCustom;
            Step_float(electricBlue, sinOutElectric, stepCustom);

            float ElectricResult = stepCustom - stepNative;

            //Scroll uv value for y direction 
            _ScrollY *= _Time;

            //According to normal vector and view direction sets rim color
            half rim = 1.0 - saturate(dot(normalize(o.Normal), normalize(IN.viewDir)));

            //Scrolling scan texture
            fixed4 b = tex2D(_ScanTex, IN.uv_ScanTex +
            float2(0, _ScrollY));
            //Scrolling main texture
            fixed4 a = 2*b*tex2D(_MainTex, IN.uv_MainTex);

            
            float sinOut;
            //Simple remapping to handle fresnel effect
            Remap_float4(sin(_Time * _RimPulseSpeed), float2(-1, 1), float2(_RimPowerA, _RimPowerB), sinOut);

            //Produce final effect
            o.Emission = _RimColor.rgb * pow(rim, sinOut) + a + _ElectricColor.rgb * ElectricResult;
            //Change alpha to sync the effect
            o.Alpha = pow(rim, sinOut) + a;
            o.Smoothness = 0.5;
            o.Occlusion = 1;
            //Set albedo color
            o.Albedo = _AlbedoColor.rgb ;

            
        }
        ENDCG
    }
        FallBack "Diffuse"
}
