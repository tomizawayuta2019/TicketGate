Shader "Unlit/color"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Range(0, 1.0)) = .1
	}
		
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform float _Color;
			
			fixed4 frag (v2f_img i) : SV_Target
			{
				return float4(_Color, 1.0 - _Color, 0, 1);
			}
			ENDCG
		}
	}
}
