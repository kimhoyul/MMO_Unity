using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameScene))]
[RequireComponent(typeof(CursorController))]
public class GameScene : BaseScene
{
	protected override void Init()
	{
		base.Init();

		SceneType = Define.Scene.Game;

		Managers.UI.ShowSceneUI<UI_Inven>();

		Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
	}

	public override void Clear()
	{

	}

}
