Shader "Custom/GeometricPattern"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1,1,1,1)
        _SecondaryColor ("Secondary Color", Color) = (0,0,0,1)
        _Scale ("Scale", Float) = 10.0
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

            fixed4 _MainColor;
            fixed4 _SecondaryColor;
            float _Scale;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 scaledUV = i.uv * _Scale;
                float2 pattern = abs(frac(scaledUV - 0.5) - 0.5) / fwidth(scaledUV);
                float line = smoothstep(0.45, 0.55, max(pattern.x, pattern.y));

                // êFÇÃï‚ä‘
                return lerp(_SecondaryColor, _MainColor, line);
            }
            ENDCG
        }
    }
}