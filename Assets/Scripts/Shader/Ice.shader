Shader "Custom/Ice"
{
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

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
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

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
                float3 viewDirWS : TEXCOORD0;
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
                float3 worldPosWS = TransformObjectToWorld(IN.positionOS).xyz;
                OUT.viewDirWS = normalize(_WorldSpaceCameraPos.xyz - worldPosWS);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half3 viewDir = normalize(IN.viewDirWS);
                half3 normal = normalize(IN.normalWS);
                float alpha = 1 - abs(dot(viewDir, normal));
                alpha *= 1.5;
                return half4(1, 1, 1, saturate(alpha));
            }
            ENDHLSL
        }
    }
    FallBack Off
}