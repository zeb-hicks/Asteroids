#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
Texture2D NormalTexture;
float3 LightPosition;
float3 LightColor;
float LightRadius;
float2 WorldPosition;

sampler2D SpriteTextureSampler = sampler_state {
	Texture = <SpriteTexture>;
};
sampler2D NormalTextureSampler = sampler_state {
	Texture = <NormalTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float factor = 1.0 / 32.0;
	float4 diff = tex2D(SpriteTextureSampler, input.TextureCoordinates);
	float4 norm = normalize(tex2D(NormalTextureSampler, input.TextureCoordinates) - 0.5) * float4(1.0, -1.0, 1.0, 1.0);
	float light = max(dot(norm.xyz, LightPosition * factor), 1.0 - norm.a);
	float dist = distance(input.TextureCoordinates.xy * 2.0 - 1.0, LightPosition.xy * factor);
	//light = min(light / dist * LightRadius, 1.0);
	light = light / dist * LightRadius;
	return diff * light * float4(LightColor, 1.0) * input.Color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};