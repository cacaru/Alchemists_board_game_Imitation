Shader "Custom/Cube_Shader"
{
	Properties
	{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Main Texture", 2D) = "white" {}
		_Outline("Outline", Range(0,1)) = 0.0
		_OutlineColor("Outline Color", Color) = (1,1,1,1)
	}

	SubShader
	{
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

		// 외곽선 그리기
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Front // 뒷면만 그리기
			ZWrite Off

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			half _Outline;
			half4 _OutlineColor;

			struct vertexInput
			{
				float4 vertex: POSITION;
			};

			struct vertexOutput
			{
				float4 pos: SV_POSITION;
			};

			float4 CreateOutline(float4 vertPos, float Outline)
			{
				// 행렬 중에 크기를 조절하는 부분만 값을 넣는다.
				/* x 0 0 0 
				 * 0 y 0 0
				 * 0 0 z 0
				 * 0 0 0 1   <- 4차원 행렬이기떄문에 마지막 값은 일정하게 유지해줌 
				 *  float4로 사용하는 이유 ::float : 값의 정밀도 높임 
				 *  4차원 : x y z의 위치와 w 방향을 나타냄 
				       > 3차원으로 이루어진 유니티에서 포인터와 점을 구분짓기 위해 w의 방향을 지정함으로써 변환시의 방향 파악 및 변환을 이루어내게 하기 때문.
				 */
				float4x4 scaleMat;
				scaleMat[0][0] = 1.0f + Outline;
				scaleMat[0][1] = 0.0f;
				scaleMat[0][2] = 0.0f;
				scaleMat[0][3] = 0.0f;
				scaleMat[1][0] = 0.0f;
				scaleMat[1][1] = 1.0f + Outline;
				scaleMat[1][2] = 0.0f;
				scaleMat[1][3] = 0.0f;
				scaleMat[2][0] = 0.0f;
				scaleMat[2][1] = 0.0f;
				scaleMat[2][2] = 1.0f + Outline;
				scaleMat[2][3] = 0.0f;
				scaleMat[3][0] = 0.0f;
				scaleMat[3][1] = 0.0f;
				scaleMat[3][2] = 0.0f;
				scaleMat[3][3] = 1.0f;

				return mul(scaleMat, vertPos);
			}

			// vertex shader
			vertexOutput vert(vertexInput v)
			{
				vertexOutput o;

				o.pos = UnityObjectToClipPos(CreateOutline(v.vertex, _Outline));

				return o;
			}

			// pixel shader
			half4 frag(vertexOutput i) : COLOR
			{
				return _OutlineColor;
			}

			ENDCG
		}

		// 정상적으로 그리기
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			half4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;

			struct vertexInput
			{
				float4 vertex: POSITION;
				float4 texcoord: TEXCOORD0;
			};

			struct vertexOutput
			{
				float4 pos: SV_POSITION;
				float4 texcoord: TEXCOORD0;
			};

			vertexOutput vert(vertexInput v)
			{
				vertexOutput o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.texcoord.xy = (v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				return o;
			}

			half4 frag(vertexOutput i) : COLOR
			{
				return tex2D(_MainTex, i.texcoord) * _Color;
			}

			ENDCG
		}
	}
}
