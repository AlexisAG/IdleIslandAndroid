Shader "Custom/Water"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		[NoScaleOffset] _DeriveHeightMap("Deriv (AG) Height (B)", 2D) = "black" {}
		[NoScaleOffset] _FlowMap("Flow (RG, A noise)", 2D) = "black" {}

		_Glossiness("Smoothness", Range(0,1)) = 0.5

		_Tiling("Tiling Texture", Float) = 1
		_AnimationSpeed("Animation Speed", Range(0, 1)) = 1
		[Toggle] _UseFlowMapDirection("Use flow map direction", float) = 1
		_FlowStrength("Flow Strength", float) = 1
		_HeightScale("Height Scale", float) = 1
		_UOffset("U Offset for distortion", Range(-.25, .25)) = 0.25
		_VOffset("V Offset for distortion", Range(-.25, .25)) = 0.25

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows addshadow

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _DeriveHeightMap;
		sampler2D _FlowMap;
        fixed4 _Color;
		half _Glossiness;
		float _Tiling;
		float _UseFlowMapDirection;
		float _AnimationSpeed;
		float _FlowStrength;
		float _HeightScale;
		float _UOffset;
		float _VOffset;


        struct Input
        {
            float2 uv_MainTex;
        };

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

		float2 FixNormal(float2 value) 
		{
			return value * 2 - 1;
		}

		float3 UnpackDerivativeHeight(float4 textureData) {
			float3 deriveHeight = textureData.agb;
			deriveHeight.xy = FixNormal(deriveHeight.xy);
			return deriveHeight;
		}

		float3 FlowUVW(float2 uv, float2 dir, float2 uvOffset, float tiling, float time, bool isSecondFlow)
		{
			float3 uvw;
			float phaseOffset = isSecondFlow ? 0.5 : 0;
			float progress = frac(time + phaseOffset);
			uvw.xy = uv - dir * progress;
			uvw.xy *= tiling;
			uvw.xy += phaseOffset + (time - progress) * uvOffset;
			uvw.z = 1 - abs(1 - 2 * progress); //loop animation

			return uvw;
		}

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
			float4 flowMapSamples = tex2D(_FlowMap, IN.uv_MainTex);
			
			float2 dir = float2(0,0);
			float noise = flowMapSamples.a;
			
			if (_UseFlowMapDirection)
				dir = FixNormal(flowMapSamples.rg) * _FlowStrength;

			float2 offsetUV = float2(_UOffset, _VOffset);

			float3 flowA = FlowUVW(IN.uv_MainTex, dir, offsetUV, _Tiling, (_Time.y * _AnimationSpeed) + noise, false);
			float3 flowB = FlowUVW(IN.uv_MainTex, dir, offsetUV, _Tiling, (_Time.y * _AnimationSpeed) + noise, true);

			float3 deriveHeightA = UnpackDerivativeHeight(tex2D(_DeriveHeightMap, flowA.xy)) * (flowA.z * _HeightScale);
			float3 deriveHeightB = UnpackDerivativeHeight(tex2D(_DeriveHeightMap, flowB.xy)) * (flowB.z * _HeightScale);

			fixed4 c = ((tex2D (_MainTex, flowA.xy) * flowA.z) + (tex2D(_MainTex, flowB.xy) * flowB.z)) * _Color; 
            o.Albedo = c.rgb;
			o.Normal = normalize(float3(-(deriveHeightA.xy + deriveHeightB.xy), 1));
			o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
