// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Outline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Outline("Outline", Range(0,1)) = 0.0
		_OutlineColor("Outline Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 200
  
        // 뒷 배경 -> 아웃라인처럼 보이게 더 크게 작성
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            // include file that contains UnityObjectToWorldNormal helper function
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;   // vertex position
                float2 uv : TEXCOORD0;      // texture coordinate
                float4 tangent : TANGENT;     // normal vector
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;          // texture coordinate
                float4 vertex : SV_POSITION;    // clip space position
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed _Outline;
            fixed4 _OutlineColor;

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

            v2f vert(appdata v)
            {
                v2f output;
                output.vertex = UnityObjectToClipPos(CreateOutline(v.vertex, _Outline));
                output.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                return output;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                if (_Outline > 0) {
                    col.rgb = _OutlineColor;
                }
                
                return col;
            }

            ENDCG

        }


        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag

            // include file that contains UnityObjectToWorldNormal helper function
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;   // vertex position
                float2 uv : TEXCOORD0;      // texture coordinate
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;          // texture coordinate
                float4 vertex : SV_POSITION;    // clip space position
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f output;
                output.vertex = UnityObjectToClipPos(v.vertex);
                output.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return output;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                return col;
            }
            ENDCG
        }
        
    }
}
