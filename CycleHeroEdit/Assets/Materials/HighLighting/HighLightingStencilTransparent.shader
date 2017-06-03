Shader "Hidden/HighLighted/StencilTransparent"
{

	Properties
	{
		_MainTex( "", 2D ) = "" {}
		_Cutoff ( "", Float ) = 0.5
	}
	
	
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
			#pragma vertex vert_alpha
			#pragma fragment frag_alpha
			#pragma fragmentoption ARB_precision_hint_fastest
			ENDCG
		}
	}
	
	Fallback Off
}