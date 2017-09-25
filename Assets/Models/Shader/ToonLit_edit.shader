Shader "Toon/Lit_edit" {
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Diffuse(RGB)", 2D) = "white" {}
		_ShadowTex("Shadow(RGBA)", 2D) = "black" {}
		_CelTex("Cel(RGB)", 2D) = "white" {}
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_OutlineWidth("Outline Width", float) = 1
		_OutlineDepth("Outline Depth", float) = 1
	}
SubShader{
		Tags
		{
			"RenderType" = "Opaque" "Queue" = "Geometry" "LightMode" = "ForwardBase"
		}
Pass{

	CGPROGRAM
	#pragma target 3.0
	#pragma multi_compile_fwdbase
	#pragma vertex vert
	#pragma fragment frag
	#include "UnityCG.cginc"
	//#include "Lighting.cginc"
	#include "AutoLight.cginc"

	half4 _Color;
	sampler2D _MainTex;
	sampler2D _CelTex;
	sampler2D _ShadowTex;

	float4 _MainTex_ST;
	fixed4 _LightColor0;

	struct v2f {
		float4	pos : SV_POSITION;
		float2	uv : TEXCOORD0;
		half3	lightDir : TEXCOORD1;
		half3	viewDir : TEXCOORD2;
		half3  normal : TEXCOORD3;
		LIGHTING_COORDS(4, 5)
		fixed3   color : COLOR0;
		fixed3  ambient : COLOR1;
	};

	v2f vert(appdata_base v) {
		v2f o;

		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

		o.lightDir = normalize(ObjSpaceLightDir(v.vertex));
		o.viewDir = normalize(ObjSpaceViewDir(v.vertex));
		o.normal = v.normal;

		half3 worldNormal = UnityObjectToWorldNormal(v.normal);
		half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
		o.color = nl * _LightColor0.rgb;
		o.ambient = ShadeSH9(half4(worldNormal, 1));

		TRANSFER_VERTEX_TO_FRAGMENT(o);
		return o;
	}

		fixed4 frag(v2f i) : SV_Target
		{
			//comment : NormalMap
			half3 norm = normalize(i.normal);
			norm = normalize(norm);

			//comment : diffuse
			half4 texcol = tex2D(_MainTex,i.uv);

			//comment : Light0
			fixed3 lightColor = _LightColor0 * texcol.rgb * texcol.a;

			//comment : Speculer
			half3 hv = normalize(i.viewDir + i.lightDir/2);
			half lspec = dot(hv, norm);
			half p = pow(lspec, (1 - texcol.a) * 10);
			half4 specWeight = smoothstep(1 - texcol.a, 1 - texcol.a + 0.01, p);

			//comment : harf lambert + vertexColor
			float2 toon = (dot(i.lightDir, norm) * 0.5 + 0.5);

			toon = clamp(toon, 0, 1);

			//comment : shadow
			float4 shadowcol = LIGHT_ATTENUATION(i);
			float4 shadowtex = tex2D(_ShadowTex, i.uv);
			float4 celcol = max((tex2D(_CelTex, half2(toon)) * shadowcol), 0);
			float3 cel = min(celcol.rgb + shadowtex.rgb, 1);

			//comment : color out
			float4 outColor;
			outColor.rgb = _Color * texcol.rgb * cel.rgb * (_LightColor0.rgb);
			outColor.rgb += (lightColor * specWeight.rgb) + (i.ambient-0.2);
			outColor.a = 1.0;

			return outColor;
		}
			ENDCG
		}
Pass{

		Cull Front
		Lighting Off

		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"

		float4 _OutlineColor;
		float _OutlineWidth;
		sampler2D _ShadowTex;
		float _OutlineDepth;

		float4 _MainTex_ST;

		struct v2f {
			float4  pos : SV_POSITION;
			fixed2  uv : TEXCOORD0;
			float3  normal : TEXCOORD1;
		};

		v2f vert(appdata_full v) {
			v2f o;
			//v.color = _Color;
			o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
			o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
			float3 norm = mul(UNITY_MATRIX_MV,float4(v.normal,0));
			float2 offset = TransformViewToProjection(norm.xyz);

			o.pos.xy += _OutlineWidth / 100 * offset * lerp(o.pos.w, 1, _OutlineDepth);
			o.pos.z += 0.0001 / o.pos.w;
			return o;
		}

		half4 frag(v2f i) : COLOR{
			//comment : diffuse
			float4 texcol = tex2D(_ShadowTex,i.uv);

			return half4(texcol * _OutlineColor.rgb,1);
		}
			ENDCG
		}

		}
			FallBack "Diffuse"
}