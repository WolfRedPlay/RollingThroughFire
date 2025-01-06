Shader "Unlit/Water"
{
    Properties
    {
        _DeepColor ("Deep Water Color", Color) = (0, 0.2, 0.4, 1)
        _ShallowColor ("Shallow Water Color", Color) = (0.2, 0.5, 0.7, 1)
        _WaveSpeed ("Wave Speed", Float) = 1.0
        _WaveStrength ("Wave Strength", Float) = 0.2
        _FresnelPower ("Fresnel Power", Float) = 5.0
        _Transparency ("Transparency", Range(0,1)) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 300

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            // Properties
            fixed4 _DeepColor;
            fixed4 _ShallowColor;
            float _WaveSpeed;
            float _WaveStrength;
            float _FresnelPower;
            float _Transparency;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = mul((float3x3)unity_ObjectToWorld, v.normal);
                o.viewDir = normalize(_WorldSpaceCameraPos - o.worldPos);
                return o;
            }

            // Simple Perlin Noise for Wave Simulation
            float wave(float2 uv, float time)
            {
                return sin(uv.x * 10.0 + time) * 0.1 +
                       cos(uv.y * 15.0 + time * 1.2) * 0.05;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Time-based wave calculation
                float time = _Time.y * _WaveSpeed;
                float waveHeight = wave(i.worldPos.xz, time) * _WaveStrength;

                // Calculate the Fresnel effect
                float fresnel = pow(1.0 - saturate(dot(i.viewDir, normalize(i.worldNormal))), _FresnelPower);

                // Blend shallow and deep water colors based on wave height
                fixed4 color = lerp(_ShallowColor, _DeepColor, saturate(waveHeight + 0.5));
                color.rgb += fresnel;

                // Apply transparency
                color.a = _Transparency;

                return color;
            }
            ENDCG
        }
    }

    FallBack "Transparent/Diffuse"
}
