Shader "Unlit/RimLightColor"
{
    Properties
    {
		_MainTex("Texture", 2D) = "white" {}

		_Color("Color", Color) = (1,1,1,1)
		_ColorLight("ColorLight", Color) = (1,1,1,1)
		_RimExp("RimExponent", Float) = 1
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				float3 normal : TEXCOORD1;
				float3 color : TEXCOORD2;
            };

			sampler2D _MainTex;
			float4 _MainTex_ST;

			float4 _Color;
			float4 _ColorLight;
			float _RimExp;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.normal = v.normal;
                
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);

				float3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
				float rim = 1 - saturate(dot(normalize(viewDir), v.normal));
				rim = pow(rim, _RimExp);
				o.color = lerp(_Color.rgb, _ColorLight.rgb, rim);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(i.color, 1) * tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
