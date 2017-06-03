require "Common.Wrap"
luanet.load_assembly("UnityEngine")


object					= System.Object
Type					= System.Type
Object					= System.Object
GameObject				= UnityEngine.GameObject
Transform				= UnityEngine.Transform
MonoBehaviour			= UnityEngine.MonoBehaviour
Component				= UnityEngine.Component
Application				= UnityEngine.Application
SystemInfo				= UnityEngine.SystemInfo
Screen					= UnityEngine.Screen
Camera					= UnityEngine.Camera
Material 				= UnityEngine.Material
Renderer 				= UnityEngine.Renderer
AsyncOperation			= UnityEngine.AsyncOperation


CharacterController 	= UnityEngine.CharacterController
SkinnedMeshRenderer 	= UnityEngine.SkinnedMeshRenderer
Animation				= UnityEngine.Animation

AnimationClip			= UnityEngine.AnimationClip

AnimationEvent			= UnityEngine.AnimationEvent

AnimationState			= UnityEngine.AnimationState
Input					= UnityEngine.Input
KeyCode					= UnityEngine.KeyCode
AudioClip				= UnityEngine.AudioClip
AudioSource				= UnityEngine.AudioSource
Physics					= UnityEngine.Physics
Light					= UnityEngine.Light
LightType				= UnityEngine.LightType
ParticleEmitter			= UnityEngine.ParticleEmitter
Space					= UnityEngine.Space
CameraClearFlags		= UnityEngine.CameraClearFlags
RenderSettings  		= UnityEngine.RenderSettings
MeshRenderer			= UnityEngine.MeshRenderer
WrapMode				= UnityEngine.WrapMode
QueueMode				= UnityEngine.QueueMode
PlayMode				= UnityEngine.PlayMode
ParticleAnimator		= UnityEngine.ParticleAnimator
TouchPhase 				= UnityEngine.TouchPhase
AnimationBlendMode 		= UnityEngine.AnimationBlendMode


function print(...)

	local arg = {...};
	local t   = {};
	
	for i, k in ipairs( arg ) do
		table.insert( t, tostring(k))
	end
	
	local str  = table.concat(t);
	Debugger.Log( str );
end



require "Common.class"

require "Common.Math"

require "Common.Layer"

require "Common.List"

require "Common.Time"

require "Common.Event"

require "Common.Timer"
require "Common.Vector3"
require "Common.Vector2"
require "Common.Quaternion"
require "Common.Vector4"
require "Common.Raycast"
require "Common.Color"
require "Common.Touch"
require "Common.Ray"
require "Common.Plane"
require "Common.Bounds"
require "Common.Coroutine"



function traceback( msg )

	msg = debug.traceback( msg, 2 );
	return msg;
end
