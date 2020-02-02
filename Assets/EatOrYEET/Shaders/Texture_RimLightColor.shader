Shader "Unlit/Texture_RimLightColor"
{
    Properties
    {
		_MainTex("Texture", 2D) = "white" {}

		_Color("Color", Color) = (1,1,1,1)
		_ColorLight("ColorLight", Color) = (1,1,1,1)
		_RimExp("RimExponent", Float) = 1

		_Hardness("Hardness", Range(0,1)) = 0
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
				float3 normalWorld : TEXCOORD3;
            };

			sampler2D _MainTex;
			float4 _MainTex_ST;

			float4 _Color;
			float4 _ColorLight;
			float _RimExp;

			float _Hardness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.normal = v.normal;
                
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);

				o.normalWorld = mul(unity_ObjectToWorld, float4(v.normal, 0.0)).xyz;
				o.normalWorld = normalize(o.normalWorld);

				float3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
				float rim = 1 - saturate(dot(normalize(viewDir), v.normal));
				rim = pow(rim, _RimExp);
				o.color = lerp(_Color.rgb, _ColorLight.rgb, rim);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 mainColor = fixed4(i.color, 1) * tex2D(_MainTex, i.uv);

				// Normal based fake lighting
				float3 light = float3(0.577, -0.577, 0.577);
				float lDotN = dot(i.normalWorld, -light);
				lDotN = clamp(lDotN, 0, 1);
				lDotN = lDotN * _Hardness + 1.0 - _Hardness;
				mainColor.rgb *= lDotN;

				return mainColor;
            }
            ENDCG
        }
    }
}
