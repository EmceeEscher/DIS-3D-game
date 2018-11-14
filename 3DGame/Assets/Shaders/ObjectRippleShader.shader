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
                float4 localPosition : POSITION1;
            };
            
            float _VibrationProgress;
            float _MinMeshPoint;
            float _MaxMeshPoint;

            vertexOutput vertexShader (vertexInput vInput)
            {
                vertexOutput vOutput;
                
                vOutput.localPosition = vInput.position;
                vOutput.position = vInput.position;
                
                if (_VibrationProgress > -1.0) {
                    float relativeHeight = (vOutput.position.y + _MaxMeshPoint) / (_MaxMeshPoint * 2);
                    if (abs(relativeHeight - _VibrationProgress) < 0.5) {
                        vOutput.position.x = vOutput.position.x * (1 + (0.5 - abs(relativeHeight - _VibrationProgress)));
                    } 
                }
                
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
                fixed4 col = fixed4(0,0,0,1); //green (default)
                
                if (_VibrationProgress > -1.0) {
                    float relativeHeight = (vOutput.localPosition.y + _MaxMeshPoint) / (_MaxMeshPoint * 2);
                    if (abs(relativeHeight - _VibrationProgress) < 0.5) {
                        col.x += (0.5 - abs(relativeHeight - _VibrationProgress));
                        col.y = 1 - (0.5 - abs(relativeHeight - _VibrationProgress));
                        col.z += (0.5 - abs(relativeHeight - _VibrationProgress));
                    } 
                }
                
                return col;
            }

            ENDCG
        }
    }
}
