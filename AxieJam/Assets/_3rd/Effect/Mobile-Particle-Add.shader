// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Simplified Additive Particle shader. Differences from regular Additive Particle one:
// - no Tint color
// - no Smooth particle support
// - no AlphaTest
// - no ColorMask

Shader "Mobile/Particles/Additive" {
	Properties {
		_MainColor ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_Scale("Scale", Float) = 1
	}

	Subshader
	{
		Tags { "Queue"="Transparent+1" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Blend SrcAlpha One
		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Color (0,0,0,0) }
		Pass
		{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"
				uniform fixed4 _MainTex_ST;
				uniform fixed4 _MainColor;
				uniform fixed _Scale;
				uniform sampler2D _MainTex;
				
				struct app_data
				{
					fixed4 vertex : POSITION;
					fixed4 texcoord : TEXCOORD0;
					fixed4 color : COLOR;
				};
				
				struct v2f
				{
					fixed4 pos	: SV_POSITION;
					fixed2 uv : TEXCOORD0;
					fixed4 color : TEXCOORD1;
				};
				
				v2f vert (app_data v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos (v.vertex);
					o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
					o.color = v.color;
					return o;
				}
				
			
				
				fixed4 frag (v2f i) : COLOR
				{
					fixed4 c = tex2D(_MainTex, i.uv) * i.color;
					return c * _MainColor * _Scale;
				}
			ENDCG
		}
	}
}