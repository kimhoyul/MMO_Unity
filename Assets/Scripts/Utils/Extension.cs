using System;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{
	#region UI
	public static void AddUIEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.CLICK)
	{
		UI_Base.AddUIEvent(go, action, type);
	}
	#endregion UI
}
