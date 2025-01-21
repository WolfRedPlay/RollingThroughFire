Shader "Unlit/WaterElectro"
{
    Properties
    {
        // Water Shader Properties
        _DeepColor ("Deep Water Color", Color) = (0, 0.2, 0.4, 1)
        _ShallowColor ("Shallow Water Color", Color) = (0.2, 0.5, 0.7, 1)
        _WaveSpeed ("Wave Speed", Float) = 1.0
        _WaveStrength ("Wave Strength", Float) = 0.2
        _FresnelPower ("Fresnel Power", Float) = 5.0

        // Electric Effect Properties
        _BaseColor ("Electric Base Color", Color) = (0.42, 0.58, 0.77, 1.0)
        _ElectricDensity ("Electric Density", Float) = 0.9
        _ElectricRadius ("Electric Radius", Float) = 0.4
        _Velocity ("Electric Velocity", Float) = 0.1
        _Scale ("Electric World Space Scale", Float) = 2.5

        // Global Transparency
        _Transparency ("Transparency", Range(0,1)) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }   
        LOD 300

        Blend SrcAlpha OneMinusSrcAlpha

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

            fixed4 _BaseColor;
            float _ElectricDensity;
            float _ElectricRadius;
            float _Velocity;
            float _Scale;

            float _Transparency;

            // Utility Functions for Electric Effect
            float3 hash3_3(float3 p3)
            {
                float3 mod3_ = float3(0.1031, 0.11369, 0.13787);
                p3 = frac(p3 * mod3_);
                p3 += dot(p3, p3.yzx + 19.19);
                return -1.0 + 2.0 * frac(float3(
                    (p3.x + p3.y) * p3.z,
                    (p3.x + p3.z) * p3.y,
                    (p3.y + p3.z) * p3.x
                ));
            }

            float perlin_noise3(float3 p)
            {
                float3 pi = floor(p);
                float3 pf = p - pi;

                float3 w = pf * pf * (3.0 - 2.0 * pf);

                return lerp(
                    lerp(
                        lerp(
                            dot(pf - float3(0, 0, 0), hash3_3(pi + float3(0, 0, 0))),
                            dot(pf - float3(1, 0, 0), hash3_3(pi + float3(1, 0, 0))),
                            w.x),
                        lerp(
                            dot(pf - float3(0, 0, 1), hash3_3(pi + float3(0, 0, 1))),
                            dot(pf - float3(1, 0, 1), hash3_3(pi + float3(1, 0, 1))),
                            w.x),
                        w.z),
                    lerp(
                        lerp(
                            dot(pf - float3(0, 1, 0), hash3_3(pi + float3(0, 1, 0))),
                            dot(pf - float3(1, 1, 0), hash3_3(pi + float3(1, 1, 0))),
                            w.x),
                        lerp(
                            dot(pf - float3(0, 1, 1), hash3_3(pi + float3(0, 1, 1))),
                            dot(pf - float3(1, 1, 1), hash3_3(pi + float3(1, 1, 1))),
                            w.x),
                        w.z),
                    w.y);
            }

            float noise_sum_abs3(float3 p)
            {
                float f = 0.0;
                p = p * 3.0;
                f += 1.0000 * abs(perlin_noise3(p)); p = 2.0 * p;
                f += 0.5000 * abs(perlin_noise3(p)); p = 3.0 * p;
                f += 0.2500 * abs(perlin_noise3(p)); p = 4.0 * p;
                f += 0.1250 * abs(perlin_noise3(p)); p = 5.0 * p;
                f += 0.0625 * abs(perlin_noise3(p)); p = 6.0 * p;

                return f;
            }

            struct appdata
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
                float3 objectPos: TEXCOORD3;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = mul((float3x3)unity_ObjectToWorld, v.normal);
                o.viewDir = normalize(_WorldSpaceCameraPos - o.worldPos);
                o.objectPos = v.vertex.xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Time-based wave calculation
                float time = _Time.y * _WaveSpeed;
                float waveHeight = sin(i.worldPos.x * 10.0 + time) * 0.1 +
                                   cos(i.worldPos.z * 15.0 + time * 1.2) * 0.05;
                waveHeight *= _WaveStrength;

                // Calculate Fresnel effect
                float fresnel = pow(1.0 - saturate(dot(i.viewDir, normalize(i.worldNormal))), _FresnelPower);

                // Blend shallow and deep water colors
                fixed4 waterColor = lerp(_ShallowColor, _DeepColor, saturate(waveHeight + 0.5));
                waterColor.rgb += fresnel;

                // Electric Effect
                float2 p = i.objectPos.xz / _Scale;

                float electric_radius = length(p) - _ElectricRadius;
                float moving_coord = sin(_Velocity * _Time.y) / 0.2 * cos(_Velocity * _Time.y);
                float3 electric_local_domain = float3(p, moving_coord);
                float electric_field = _ElectricDensity * noise_sum_abs3(electric_local_domain);

                fixed4 electricColor = _BaseColor;
                electricColor.rgb += (1.0 - (electric_field + electric_radius));
                electricColor.rgb += 1.0 - 4.2 * electric_field;

                // Combine Water and Electric Effects
                fixed4 finalColor = waterColor * 5 + electricColor * 0.01;

                // Apply transparency
                finalColor.a = _Transparency;

                return finalColor;
            }
            ENDCG
        }
    }

    FallBack "Transparent/Diffuse"

}

