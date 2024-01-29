using System;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{
	#region UI
	public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
	{
		return Util.GetOrAddComponent<T>(go);
	}

	public static void BindEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.CLICK)
	{
		UI_Base.BindEvent(go, action, type);
	}
	#endregion UI
}
