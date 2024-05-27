Shader "Unlit/BanderaOndeante"
{
    Properties 
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WavingFrequency ("Waving Frequency", Range(0.01, 10)) = 0.02
        _WavingAmplitude ("Waving Amplitude", Range(0.0001, 1)) = 0.5
    }
    SubShader 
    {
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
            float _WavingFrequency;
            float _WavingAmplitude;
            
            v2f vert (appdata v)
            {
                v2f o;
                float waving = _WavingAmplitude * (1 - v.uv.x) * sin(_Time.y * _WavingFrequency + v.uv.x * 2 * 3.14159);  // Ajustamos la función sinusoidal para que el desplazamiento vertical dependa de la posición del vértice.
                v.vertex.z += waving;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    } 
}
