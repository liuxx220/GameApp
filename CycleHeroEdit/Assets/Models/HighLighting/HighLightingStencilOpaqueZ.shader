Shader "Hidden/HighLighted/StencilOpaqueZ"
{
	CGINCLUDE
	#include "HightLightingIndude.cginc"
	ENDCG
	
	SubShader
	{
		Pass
		{
			ZWrite On
			ZTest  LEqual
			Lighting Off
			Fog { Mode Off }
			
			
			CGPROGRAM
			#pragma vertex vertex
			#pragma fragment fragment
			#pragma fragmentoption ARB_precision_hint_fastest
			ENDCG
		}
	}
	
	Fallback Off
}