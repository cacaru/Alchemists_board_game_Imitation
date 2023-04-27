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
  
        // �� ��� -> �ƿ�����ó�� ���̰� �� ũ�� �ۼ�
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
                // ��� �߿� ũ�⸦ �����ϴ� �κи� ���� �ִ´�.
                /* x 0 0 0
                 * 0 y 0 0
                 * 0 0 z 0
                 * 0 0 0 1   <- 4���� ����̱⋚���� ������ ���� �����ϰ� ��������
                 *  float4�� ����ϴ� ���� ::float : ���� ���е� ����
                 *  4���� : x y z�� ��ġ�� w ������ ��Ÿ��
                       > 3�������� �̷���� ����Ƽ���� �����Ϳ� ���� �������� ���� w�� ������ ���������ν� ��ȯ���� ���� �ľ� �� ��ȯ�� �̷��� �ϱ� ����.
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
