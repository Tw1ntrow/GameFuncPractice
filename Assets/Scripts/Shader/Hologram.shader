Shader "Custom/Hologram"
{
    Properties
    {
        _MainColor("Color", Color) = (0.0, 0.8, 1.0, 1.0)
        _NoiseScale("Noise Scale", Float) = 1.0
        _NoiseSpeed("Noise Speed", Float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        AlphaToMask Off

        Pass
        {
            Name "FORWARD"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            float4 _MainColor;
            float _NoiseScale;
            float _NoiseSpeed;

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 normalWS : NORMAL;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                return OUT;
            }

            float random (float2 uv)
            {
                return frac(sin(dot(uv.xy, float2(12.9898, 78.233))) * 43758.5453);
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float2 noiseUV = IN.positionCS.xy / IN.positionCS.w * _NoiseScale;
                noiseUV.y += _Time.y * _NoiseSpeed;
                float noise = random(noiseUV);
                
                half4 color = _MainColor;
                color.a *= noise;

                return color;
            }
            ENDHLSL
        }
    }
    FallBack Off
}