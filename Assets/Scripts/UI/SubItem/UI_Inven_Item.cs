using UnityEngine;
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
		GetGameObject((int)GameObjects.ItemNameText).GetComponent<Text>().text = _name;

		GetGameObject((int)GameObjects.ItemIcon).BindEvent((PointerEventData) => { Debug.Log($"������ Ŭ�� : {_name}"); });
	}

	public void SetInfo(string name)
	{
		_name = name;
	}
}
