Shader "Unlit/Transparent" {
    Properties {
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" { }
        _WindStrength ("Wind Strength", Range(0, 1)) = 0.1
        _Color ("Color", Color) = (1, 1, 1, 1)
        _WindDirection ("Wind Direction", Vector) = (1, 0, 0)
        _WindOffset ("Wind Offset", Vector) = (0, 0, 0)
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
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _WindStrength;
            float4 _Color;
            float3 _WindDirection;

            v2f vert (appdata v) {
                v2f o;
                // Añade una variación de tiempo a la función de ondulación del viento
                float wind = _WindStrength * sin(_Time.y + dot(v.vertex.xyz, _WindDirection));
                v.vertex.xyz += wind;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= _Color;
                return col;
            }
            ENDCG
        }
    }
}