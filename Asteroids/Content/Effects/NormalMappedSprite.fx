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

struct VertexShaderOutput {
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

struct PixelShaderOutput {
	float4 Color0 : COLOR0;
	float4 Color1 : COLOR1;
};

float4 UnpackNormal(sampler2D normal, float2 uv) {
	return normalize(tex2D(normal, uv) - float4(0.5, 0.5, 0.5, 0.0)) * float4(1.0, -1.0, 1.0, 1.0);
}

float NormalMapLight(float3 normal, float3 light, float3 lightcolor) {

}

PixelShaderOutput MainPS(VertexShaderOutput input) {
	PixelShaderOutput o;
	o.Color0 = tex2D(SpriteTextureSampler, input.TextureCoordinates);;
	o.Color1 = tex2D(NormalTextureSampler, input.TextureCoordinates);;
	return o;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};