Shader "Ripples/LightRippleShader"
{
   // defining the main properties as exposed in the inspector
    Properties
    {
        _BaseColor ("base color of light column", Color) = (0,1,0,1)
        _Transparency ("transparency of light column", float) = 0.5
        
    }
    // start first subshader (there is only one, but there could be multible)
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent" } 
        
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass 
        {
            CGPROGRAM

            #pragma vertex vertexShader
            #pragma fragment fragmentShader
            
            #include "UnityCG.cginc"
            
            struct vertexInput
            {
                float4 position : POSITION; 
            };
            
            struct vertexOutput
            {
                float4 position : SV_POSITION;
                float4 localPosition : POSITION1;
            };
			
            float4 _BaseColor;         
			float _Transparency;

            vertexOutput vertexShader (vertexInput vInput)
            {
                vertexOutput vOutput;
                
                vOutput.localPosition = vInput.position;
                vOutput.position = vInput.position;
                
                // local space into world space transformation:
                vOutput.position = mul(unity_ObjectToWorld, vOutput.position);
                
                // world space into view space transformation:
                vOutput.position = mul(UNITY_MATRIX_V, vOutput.position);
                // view space into clip space transformation:
                vOutput.position = mul(UNITY_MATRIX_P, vOutput.position);

                return vOutput;
            }
            
            fixed4 fragmentShader (vertexOutput vOutput) : SV_Target
            {
                fixed4 col = _BaseColor;
                
                return col;
            }

            ENDCG
        }
    }
}
