Shader "Ripples/ObjectRippleShader"
{
    // defining the main properties as exposed in the inspector
    Properties
    {

    }
    // start first subshader (there is only one, but there could be multible)
    SubShader
    {
        Tags { "RenderType"="Opaque" } 
        
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
                float4 worldPosition : POSITION1;
            };
            
            float _VibrationProgress;

            vertexOutput vertexShader (vertexInput vInput)
            {
                vertexOutput vOutput;
                
                // local space into world space transformation:
                vOutput.position = mul(unity_ObjectToWorld, vInput.position);
                vOutput.worldPosition = vOutput.position;
                
                // shift side to side while vibrating:
                // do a thing here
                
                // world space into view space transformation:
                vOutput.position = mul(UNITY_MATRIX_V, vOutput.position);
                // view space into clip space transformation:
                vOutput.position = mul(UNITY_MATRIX_P, vOutput.position);

                return vOutput;
            }
            
            fixed4 fragmentShader (vertexOutput vOutput) : SV_Target
            {
                fixed4 col = fixed4(1,0,0,1); //red (default)
                
                return col;
            }

            ENDCG
        }
    }
}
