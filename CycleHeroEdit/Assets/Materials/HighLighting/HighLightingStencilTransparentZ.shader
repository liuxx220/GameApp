Shader "Hidden/HighLighted/StencilTransparentZ"
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