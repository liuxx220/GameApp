Shader "Hidden/HighLighted/StencilOpaque"
{
	CGINCLUDE
	#include "HightLightingIndude.cginc"
	ENDCG
	
	SubShader
	{
		Pass
		{
			ZWrite Off
			ZTest  Always
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