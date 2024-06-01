// 2024-06-01 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

Shader "Unlit/Transparent" {
    Properties {
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" { }
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _DispMap ("Displacement Map", 2D) = "black" {}
        _WindStrength ("Wind Strength", Range(0, 1)) = 0.1
        _Color ("Color", Color) = (1, 1, 1, 1)
        _WindDirection ("Wind Direction", Vector) = (1, 0, 0)
        _WindOffset ("Wind Offset", Vector) = (0, 0, 0)
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
    }
    SubShader { 
        LOD 100
        Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }

        Pass {
            Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
            };

            sampler2D _MainTex;
            sampler2D _BumpMap;
            sampler2D _DispMap;
            float _WindStrength;
            float4 _Color = float4(1, 1, 1, 1); // Este es el valor blanco por defecto
            float3 _WindDirection;
            float _Smoothness;

            v2f vert (appdata v) {
                v2f o;
                float wind = _WindStrength * sin(_Time.y + dot(v.vertex.xyz, _WindDirection));
                float disp = tex2D(_DispMap, v.uv).r;
                v.vertex.xyz += wind;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed3 normal = UnpackNormal(tex2D(_BumpMap, i.uv));
                col *= _Color;
                col.a *= pow(1.0 - dot(normal, normalize(i.normal)), _Smoothness * 10.0); // Aquí usamos el parámetro de suavidad
                return col;
            }
            ENDCG
        }
    }
}

