Shader "Ripples/FloorRippleShader"
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
            
            int _NumRipples = 3; // must be less than the size of the _Ripples array below
            float4 _Ripples[50];

            vertexOutput vertexShader (vertexInput vInput)
            {
                vertexOutput vOutput;
                
                // local space into world space transformation:
                vOutput.position = mul(unity_ObjectToWorld, vInput.position);
                vOutput.worldPosition = vOutput.position;
                
                // world space into view space transformation:
                vOutput.position = mul(UNITY_MATRIX_V, vOutput.position);
                // view space into clip space transformation:
                vOutput.position = mul(UNITY_MATRIX_P, vOutput.position);

                return vOutput;
            }
            
            fixed4 fragmentShader (vertexOutput vOutput) : SV_Target
            {
                fixed4 col = fixed4(0,0,0,1); //black (default)
				col = fixed4(0.5, 0.5, 0.7, 1); //DEBUG: uncomment to make floors/walls visible

                // calculate if point is on current ring
                for (int i = 0; i < _NumRipples; i++) {
                    float4 center = float4(_Ripples[i].x, vOutput.worldPosition.y, _Ripples[i].z, vOutput.worldPosition.w);
                    float distanceToCenter = distance(center, vOutput.worldPosition);
                    float diff = distanceToCenter - _Ripples[i].y;
                    
                    // if point is within thickness of current ring, color it   
                    if (abs(diff) < _Ripples[i].w) {
                        col = fixed4(1,0,0,1); //red
                        break;
                    }
                }
                
                return col;
            }

            ENDCG
        }
    }
}
