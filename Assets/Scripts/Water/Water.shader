Shader "Custom/Water"
{
    Properties
    {
        _WaveStrength ("Wave Strength", Float) = 0.1
        _WaveSpeed ("Wave Speed", Float) = 1.0
        _WaterColor ("Water Color", Color) = (0.0, 0.5, 1.0, 0.5)
        _Transparency ("Transparency", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
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

            float _WaveStrength;
            float _WaveSpeed;
            fixed4 _WaterColor;
            float _Transparency;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                float wave = sin(_Time.y * _WaveSpeed + v.vertex.x * v.vertex.z) * _WaveStrength;
                o.vertex.y += wave;

                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // êÖÇÃêFÇ∆ìßñæìx
                fixed4 col = _WaterColor;
                col.a *= _Transparency;
                return col;
            }
            ENDCG
        }
    }
}