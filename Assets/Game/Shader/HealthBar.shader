// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Unlit/HealthBar"
{
	Properties
	{

	}
		SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "DisableBatching" = "True" }

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing

			#include "UnityCG.cginc"

			struct appdata
			{
				float2 uv : TEXCOORD0;
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float value : TEXCOORD1;
				float4 pos : SV_POSITION;
			};

			float _Values[1023];

			v2f vert(appdata v, uint instanceID: SV_InstanceID)
			{
				// Allow instancing.
				UNITY_SETUP_INSTANCE_ID(v);

				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv.xy;

				// billboard mesh towards camera
				float3 vpos = mul((float3x3)unity_ObjectToWorld, v.vertex.xyz) * float3(2, 0.3, 1);
				float4 worldCoord = float4(unity_ObjectToWorld._m03, unity_ObjectToWorld._m13, unity_ObjectToWorld._m23, 1) + float4(0, 1, 0, 0);
				float4 viewPos = mul(UNITY_MATRIX_V, worldCoord) + float4(vpos, 0);
				float4 outPos = mul(UNITY_MATRIX_P, viewPos);

				o.pos = outPos;
				o.value = _Values[instanceID];

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return fixed4(step(i.uv.x, i.value), 0, 0, 1.0);
			}
			ENDCG
		}
	}
}