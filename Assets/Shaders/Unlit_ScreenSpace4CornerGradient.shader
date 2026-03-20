Shader "Unlit/ScreenSpace4CornerGradient"
{
    Properties
    {
        // _TopLeft     ("Top Left", Color) = (0.2,0.5,1,1)
        // _TopRight    ("Top Right", Color) = (0.1,0.3,0.8,1)
        // _BottomLeft  ("Bottom Left", Color) = (0.8,0.9,1,1)
        // _BottomRight ("Bottom Right", Color) = (0.9,0.95,1,1)
        _TopLeft     ("Top Left", Color) = (0.8,0.9,1,1)
        _TopRight    ("Top Right", Color) = (0.9,0.95,1,1)
        _BottomLeft  ("Bottom Left", Color) = (0.2,0.5,1,1)
        _BottomRight ("Bottom Right", Color) = (0.1,0.3,0.8,1)
    }
    SubShader
    {
        Tags { "Queue"="Background" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Cull Off
            ZWrite Off
            ZTest Always
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _TopLeft, _TopRight, _BottomLeft, _BottomRight;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos       : SV_POSITION;
                float4 screenPos : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.pos);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.screenPos.xy / i.screenPos.w;
                // #if UNITY_UV_STARTS_AT_TOP
                //     uv.y = 1.0 - uv.y;
                // #endif

                fixed4 bottom = lerp(_BottomLeft, _BottomRight, uv.x);
                fixed4 top    = lerp(_TopLeft,    _TopRight,    uv.x);
                return lerp(bottom, top, uv.y);
            }
            ENDCG
        }
    }
    FallBack Off
}