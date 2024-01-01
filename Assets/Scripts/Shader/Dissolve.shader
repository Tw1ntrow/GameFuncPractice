Shader "Custom/Dissolve"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DissolveThreshold ("Dissolve Threshold", Range(0, 1)) = 0.5
        _EdgeColor ("Edge Color", Color) = (1,0,0,1)
        _EdgeWidth ("Edge Width", Range(0, 1)) = 0.1
        _NoiseScale ("Noise Scale", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _DissolveThreshold;
            fixed4 _EdgeColor;
            float _EdgeWidth;
            float _NoiseScale;

            float pseudoNoise(float2 coord)
            {
                return frac(sin(dot(coord, float2(12.9898, 78.233))) * 43758.5453);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 疑似ノイズ
                fixed4 col = tex2D(_MainTex, i.uv);
                float noise = pseudoNoise(i.uv * _NoiseScale);

                float edge = smoothstep(_DissolveThreshold - _EdgeWidth, _DissolveThreshold, noise);
                col = lerp(_EdgeColor, col, edge);

                // しきい値を超えたらフラグメントを破棄
                clip(noise - _DissolveThreshold);

                return col;
            }
            ENDCG
        }
    }
}