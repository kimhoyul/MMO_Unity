using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven_Item : UI_Base
{	
	enum GameObjects
	{
		ItemIcon,
		ItemNameText,
	}

	string _name;

	public override void Init()
	{
		Bind<GameObject>(typeof(GameObjects));
	}

	public void SetInfo(string name)
	{
		_name = name;

		GetGameObject((int)GameObjects.ItemNameText).GetComponent<Text>().text = _name;
		GetGameObject((int)GameObjects.ItemIcon).BindEvent((PointerEventData) => { Debug.Log($"������ Ŭ�� : {_name}"); });
	}
}
