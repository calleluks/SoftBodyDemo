Shader "Custom/Circle" {
    Properties {
        _Color ("Color", Color) = (1, 1, 1, 1)
    }

    SubShader {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            CGPROGRAM
            #include "UnityCG.cginc"

            #pragma vertex vert_img
            #pragma fragment frag
            #pragma target 3.0

            float4 _Color;

            float4 frag(v2f_img i): COLOR {
                fixed4 transparent = float4(float3(_Color), 0);
                float distance = length(i.uv - float2(0.5, 0.5));
                float delta = fwidth(distance);
                float alpha = smoothstep(0.5, 0.5 - delta, distance);
                return lerp(transparent, _Color, alpha);
            }
            ENDCG
        }
    }
}
