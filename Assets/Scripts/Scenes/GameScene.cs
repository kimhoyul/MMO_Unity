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

		gameObject.GetOrAddComponent<CursorController>();

		GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
		Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

		Managers.Game.Spawn(Define.WorldObject.Monster, "Warrior");
	}

	public override void Clear()
	{

	}

}
