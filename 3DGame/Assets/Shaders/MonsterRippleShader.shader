// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

/* 
* Utilizes TV Static Shader by Unity Forum User dandeentremont.
* https://forum.unity.com/threads/tv-static-shader.354472/
*/

Shader "Ripples/MonsterRippleShader"
{
	// defining the main properties as exposed in the inspector
	Properties
	{
		_ColorA ("Color A", Color) = (1,1,1,1)
		_ColorB ("Color B", Color) = (0,0,0,0)

		_ResX("X Resolution", Float) = 100
		_ResY("Y Resolution", Float) = 200

		_ScaleWithZoom("Scale With Cam Distance", Range(0,1)) = 1.0
        
        _WidthOfVibration ("vertical width of the vibration", float) = 0.5
        _AmplitudeOfVibration ("amplitude of sine curve of vibration", float) = 3.0
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
			#pragma target 3.0

			#include "UnityCG.cginc"

			float rand(float2 co)
			 {
				// produces random values between 0 and 1
				return frac((sin(dot(co.xy , float2(12.345 * _Time.w, 67.890 * _Time.w))) * 12345.67890 + _Time.w));
			 }
			
			fixed4 _ColorA;
			fixed4 _ColorB;

			float _ResX;
			float _ResY;
			float _ScaleWithZoom;

			struct vertexInput
			{
				float4 position : POSITION;
			};

			struct vertexOutput
			{
				float4 position : SV_POSITION;
				float4 localPosition : POSITION1;
				float4 camDist : TEXCOORD2;
			};

			float _VibrationProgress;
            float _WidthOfVibration;
            float _AmplitudeOfVibration;
			float _MaxMeshY;

			vertexOutput vertexShader(vertexInput vInput)
			{
				vertexOutput vOutput;
				UNITY_INITIALIZE_OUTPUT(vertexOutput, vOutput);

				vOutput.localPosition = vInput.position;
				vOutput.position = vInput.position;
                
                float4 worldPosition = mul(unity_ObjectToWorld, vOutput.position);
                
				if (_VibrationProgress > -1.0) {
                        if (vOutput.position.x < 0) {
                            vOutput.position.x = vOutput.position.x - _AmplitudeOfVibration * sin(worldPosition.x * _Time[1]);
                        } else {
                            vOutput.position.x = vOutput.position.x + _AmplitudeOfVibration * sin(worldPosition.x * _Time[1]);
                        }
                        
                        if (vOutput.position.y < 0) {
                            vOutput.position.y = vOutput.position.y - _AmplitudeOfVibration * sin(worldPosition.x * _Time[1]);
                        } else {
                            vOutput.position.y = vOutput.position.y + _AmplitudeOfVibration * cos(worldPosition.z * _Time[1]);
                        }
                        
                        if (vOutput.position.z < 0) {
                            vOutput.position.z = vOutput.position.z - _AmplitudeOfVibration * cos(worldPosition.z * _Time[1]);
                        } else {
                            vOutput.position.z = vOutput.position.z + _AmplitudeOfVibration * cos(worldPosition.z * _Time[1]);
                        }
				}

				// local space into world space transformation:
				vOutput.position = mul(unity_ObjectToWorld, vOutput.position);


				vOutput.camDist.x = distance(_WorldSpaceCameraPos.xyz, vOutput.position);
				// grayscale
				vOutput.camDist.x = lerp(1.0, vOutput.camDist.x, _ScaleWithZoom);

				// world space into view space transformation:
				vOutput.position = mul(UNITY_MATRIX_V, vOutput.position);
				// view space into clip space transformation:
				vOutput.position = mul(UNITY_MATRIX_P, vOutput.position);

				return vOutput;
			}

			fixed4 fragmentShader(float4 screenPos : SV_POSITION, vertexOutput vOutput) : SV_Target
			{
				fixed4 col = fixed4(0,0,0,0);
                //col = fixed4(0.5, 0.5, 0.5, 1); //DEBUG: uncomment to make monster visible

				// TODO use position information to change shader
				fixed4 local = vOutput.localPosition;

				if (_VibrationProgress > -1.0) {
                
                    float relativeHeight = (vOutput.localPosition.y + _MaxMeshY) / (_MaxMeshY * 2);
                    if (abs(relativeHeight - _VibrationProgress) < _WidthOfVibration) {

    					// static 
    					fixed4 sc = fixed4((screenPos.xy), 0.0, 1.0);
    					sc *= 0.001;

    					sc.xy -= 0.5;
    					sc.xy *= vOutput.camDist.xx;
    					sc.xy += 0.5;

    					//round the screen coordinates to give it a blocky appearance
    					sc.x = round(sc.x*_ResX) / _ResX;
    					sc.y = round(sc.y*_ResY) / _ResY;

    					float noise = rand(sc.xy);
    					float4 stat = lerp(_ColorA, _ColorB, noise.x);

    					col = fixed4(stat.xyz, 1.0);
    					// end static 
                    }
                    col.a = 1.0;
				}

				return col;
			}

			ENDCG
		}
	}
}
