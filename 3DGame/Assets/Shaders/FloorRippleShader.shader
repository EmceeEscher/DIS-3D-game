Shader "Ripples/FloorRippleShader"
{
	// defining the main properties as exposed in the inspector
    Properties
    {
        _Ripple ("Center of ripple", Vector) = (0.0, 0.0, 0.0, 0.0) //centerX, radius, centerZ, thickness
        // TODO: remove this, set in script
    }
    // start first subshader (there is only one, but there could be multible)
    SubShader
    {
        Tags { "RenderType"="Opaque" } // other render types are "Transparent" or "Geometry", defines when stuff gets rendered. 
        
        Pass // A shader can have multible passes. One pass = one time render everything.
        {
            CGPROGRAM // start a section of CG code

            #pragma vertex vertexShader // define the vertex shader function
            #pragma fragment fragmentShader // define the pixel shader function
            
            #include "UnityCG.cginc" //has many helpful functions

            //******* everything above here is just setup, you can ignore that ********

            // struct defining the Input for the VertexShader
            struct vertexInput
            {
                //float4 _Ripple : TEXCOORD0; // TODO: find better option than TEXCOORD0?
                float4 position : POSITION; // The : POSITION means what semantic to put this variable in. (what it is intendet for)
            };

            // struct defining the Output of the VertexShader and Input for the FragmentShader
            struct vertexOutput
            {
                float4 position : SV_POSITION;
                float4 worldPosition : POSITION1;
                //float4 _Ripple : TEXCOORD0;
            };

            vertexOutput vertexShader (vertexInput vInput)
            {
                vertexOutput vOutput;
                //vOutput._Ripple = vInput._Ripple;

                // the vertex data is comming in in local space, we need to transform it into clip space!
                // first into world space:
                vOutput.position = mul(unity_ObjectToWorld, vInput.position); // (unity_ObjectToWorld is UNITY_MATRIX_M)
                vOutput.worldPosition = vOutput.position;
                // then into view space:
                vOutput.position = mul(UNITY_MATRIX_V, vOutput.position);
                // finally via projection into clip space! (the cube)
                vOutput.position = mul(UNITY_MATRIX_P, vOutput.position);

                // Or do all operations with the combined ModelViewProjection Matrix!:
                // vOutput.position = mul(UNITY_MATRIX_MVP, vInput.position);

                return vOutput;
            }
            
            float4 _Ripple;
            
            fixed4 fragmentShader (vertexOutput vOutput) : SV_Target
            {
                fixed4 col = fixed4(0,0,0,1); //black (default)
                
                // calculate if point is on current ring
                float4 center = float4(_Ripple.x, vOutput.worldPosition.y, _Ripple.z, vOutput.worldPosition.w);
                float distanceToCenter = distance(center, vOutput.worldPosition);
                float diff = distanceToCenter - _Ripple.y;
                
                // if point is within thickness of current ring, color it   
                if (abs(diff) < _Ripple.w) {
                    col = fixed4(1,0,0,1); //red
                }
                
                
                return col;
            }

            ENDCG
        }
    }
}
