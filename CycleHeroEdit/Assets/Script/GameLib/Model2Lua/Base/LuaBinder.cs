﻿using System;
using System.Collections.Generic;

public static class LuaBinder
{
	public static List<string> wrapList = new List<string>();
	public static void Bind(IntPtr L, string type = null)
	{
		if (type == null || wrapList.Contains(type)) return;
		wrapList.Add(type); type += "Wrap";
		switch (type) {
			case "AnimationBlendModeWrap":                      AnimationBlendModeWrap.Register(L); break;
			case "AnimationClipWrap":                           AnimationClipWrap.Register(L); break;
			case "AnimationStateWrap":                          AnimationStateWrap.Register(L); break;
			case "AnimationWrap":                               AnimationWrap.Register(L); break;
			case "ApplicationWrap":                             ApplicationWrap.Register(L); break;
			case "AsyncOperationWrap":                          AsyncOperationWrap.Register(L); break;
			case "AudioClipWrap":                               AudioClipWrap.Register(L); break;
			case "AudioSourceWrap":                             AudioSourceWrap.Register(L); break;
			case "BehaviourWrap":                               BehaviourWrap.Register(L); break;
			case "BlendWeightsWrap":                            BlendWeightsWrap.Register(L); break;
			case "BoxColliderWrap":                             BoxColliderWrap.Register(L); break;
			case "CameraClearFlagsWrap":                        CameraClearFlagsWrap.Register(L); break;
			case "CameraWrap":                                  CameraWrap.Register(L); break;
			case "CharacterControllerWrap":                     CharacterControllerWrap.Register(L); break;
			case "ColliderWrap":                                ColliderWrap.Register(L); break;
			case "ComponentWrap":                               ComponentWrap.Register(L); break;
			case "DebuggerWrap":                                DebuggerWrap.Register(L); break;
			case "DelegateFactoryWrap":                         DelegateFactoryWrap.Register(L); break;
			case "DelegateWrap":                                DelegateWrap.Register(L); break;
			case "EnumWrap":                                    EnumWrap.Register(L); break;
			case "EventDelegateWrap":                           EventDelegateWrap.Register(L); break;
			case "GameObjectWrap":                              GameObjectWrap.Register(L); break;
			case "IEnumeratorWrap":                             IEnumeratorWrap.Register(L); break;
			case "InputWrap":                                   InputWrap.Register(L); break;
			case "KeyCodeWrap":                                 KeyCodeWrap.Register(L); break;
			case "LuaEnumTypeWrap":                             LuaEnumTypeWrap.Register(L); break;
			case "MaterialWrap":                                MaterialWrap.Register(L); break;
			case "MeshColliderWrap":                            MeshColliderWrap.Register(L); break;
			case "MeshRendererWrap":                            MeshRendererWrap.Register(L); break;
			case "MonoBehaviourWrap":                           MonoBehaviourWrap.Register(L); break;
			case "ObjectWrap":                                  ObjectWrap.Register(L); break;
			case "PhysicsWrap":                                 PhysicsWrap.Register(L); break;
			case "PlayModeWrap":                                PlayModeWrap.Register(L); break;
			case "QueueModeWrap":                               QueueModeWrap.Register(L); break;
			case "RendererWrap":                                RendererWrap.Register(L); break;
			case "RenderTextureWrap":                           RenderTextureWrap.Register(L); break;
			case "ScreenWrap":                                  ScreenWrap.Register(L); break;
			case "Framework_AppConstWrap":                      SimpleFramework_AppConstWrap.Register(L); break;
			case "Framework_ByteBufferWrap":                    Framework_ByteBufferWrap.Register(L); break;
            case "Framework_LuaBehaviourWrap":                  Framework_LuaBehaviourWrap.Register(L); break;
			case "Framework_LuaHelperWrap":                     Framework_LuaHelperWrap.Register(L); break;
			case "Framework_MusicManagerWrap":                  Framework_MusicManagerWrap.Register(L); break;
			case "Framework_NetworkManagerWrap":                Framework_NetworkManagerWrap.Register(L); break;
            case "Framework_GameUIManagerWrap":                 Framework_GameUIManagerWrap.Register(L); break;
            case "Framework_BundleManagerWrap":                 Framework_BundleManagerWrap.Register(L); break;
			case "Framework_TimerManagerWrap":                  Framework_TimerManagerWrap.Register(L); break;
			case "Framework_UtilWrap":                          Framework_UtilWrap.Register(L); break;
            case "Framework_WrapGridWrap":                      Framework_WrapGridWrap.Register(L); break;
            case "Framework_LevelManagerWrap":                  Framework_LevelManagerWrap.Register(L); break;
            case "SkinnedMeshRendererWrap":                     SkinnedMeshRendererWrap.Register(L); break;
			case "SleepTimeoutWrap":                            SleepTimeoutWrap.Register(L); break;
			case "SpaceWrap":                                   SpaceWrap.Register(L); break;
            case "iTweenWrap":                                  iTweenWrap.Register(L); break;
            case "SphereColliderWrap":                          SphereColliderWrap.Register(L); break;
			case "stringWrap":                                  stringWrap.Register(L); break;
			case "System_ObjectWrap":                           System_ObjectWrap.Register(L); break;
			case "TextureWrap":                                 TextureWrap.Register(L); break;
			case "TimeWrap":                                    TimeWrap.Register(L); break;
			case "TouchPhaseWrap":                              TouchPhaseWrap.Register(L); break;
			case "TrackedReferenceWrap":                        TrackedReferenceWrap.Register(L); break;
			case "TransformWrap":                               TransformWrap.Register(L); break;
			case "TypeWrap":                                    TypeWrap.Register(L); break;
			case "UIButtonWrap":                                UIButtonWrap.Register(L); break;
			case "UIEventListenerWrap":                         UIEventListenerWrap.Register(L); break;
			case "UIGridWrap":                                  UIGridWrap.Register(L); break;
			case "UILabelWrap":                                 UILabelWrap.Register(L); break;
			case "UIPanelWrap":                                 UIPanelWrap.Register(L); break;
			case "UIRectWrap":                                  UIRectWrap.Register(L); break;
			case "UIWidgetWrap":                                UIWidgetWrap.Register(L); break;
			case "WWWWrap":                                     WWWWrap.Register(L); break;
            case "UITextureWrap":                               UITextureWrap.Register(L); break;
		}
	}
}
