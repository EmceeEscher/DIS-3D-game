Shader "Ripples/ColumnRippleShader"
{
    // defining the main properties as exposed in the inspector
    Properties
    {
        _WidthOfRippleEffect ("WidthOfRippleEffect", float) = 0.5
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
            float _MaxMeshY;
            float _WidthOfRippleEffect;

            vertexOutput vertexShader (vertexInput vInput)
            {
                vertexOutput vOutput;
                
                vOutput.localPosition = vInput.position;
                vOutput.position = vInput.position;
                
                if (_VibrationProgress > -1.0) {
                    float relativeHeight = (vOutput.position.y + _MaxMeshY) / (_MaxMeshY * 2);
                    float distanceFromHeight = abs(relativeHeight - _VibrationProgress);
                    if (distanceFromHeight < _WidthOfRippleEffect) {
                        if (distanceFromHeight < (_WidthOfRippleEffect * 0.4)) {
                            vOutput.position.x = vOutput.position.x * (1 + (_WidthOfRippleEffect * 1.5 - distanceFromHeight));
                        } else if (distanceFromHeight < (_WidthOfRippleEffect * 0.8)) {
                            vOutput.position.x = vOutput.position.x * (1 - (_WidthOfRippleEffect * 1.5 - distanceFromHeight));
                        } else {
                            vOutput.position.x = vOutput.position.x * (1 + (_WidthOfRippleEffect * 1.5 - distanceFromHeight));
                        }
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
                fixed4 col = fixed4(0,0,0,1); //black (default)
                
                if (_VibrationProgress > -1.0) {
                    float relativeHeight = (vOutput.localPosition.y + _MaxMeshY) / (_MaxMeshY * 2);
                    if (abs(relativeHeight - _VibrationProgress) < _WidthOfRippleEffect) {
                        col.x += (_WidthOfRippleEffect - abs(relativeHeight - _VibrationProgress));
                        col.y = 1 - (_WidthOfRippleEffect - abs(relativeHeight - _VibrationProgress));
                        col.z += (_WidthOfRippleEffect - abs(relativeHeight - _VibrationProgress));
                    } 
                }
                
                return col;
            }

            ENDCG
        }
    }
}
