Shader "Hidden/CamBW"
{
    Properties
    {
        _MainTex ("CurScreen (RGB)", 2D) = "white" {}
    _bwBlend("Black & White blend", Range(0,1)) = 0
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            
            #include "UnityCG.cginc"
            sampler2D _MainTex;
            float _bwBlend;

            float4 frag(v2f_img i) : COLOR 
            {
              float4 c = tex2D(_MainTex, i.uv);
              float lum = c.r * .3 + c.g * .59 + c.b * .11;
              float3 bw = float3( lum, lum, lum);
              
              float4 result = c;
              result.rgb =  lerp(c.rgb, bw, _bwBlend);
              return result;
            }
            
            ENDCG
        }
    }
}
