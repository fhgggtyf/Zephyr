using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class UIStatTab : MonoBehaviour
{
	public UnityAction<StatTabSO> TabClicked;

	[SerializeField] private TMP_Text _tabText = default;
	[SerializeField] private Button _actionButton = default;
	[SerializeField] private Color _selectedIconColor = default;
	[SerializeField] private Color _deselectedIconColor = default;

	[ReadOnly] public StatTabSO _currentTabType = default;

	public void SetTab(StatTabSO tabType, bool isSelected)
	{
		_currentTabType = tabType;
		_tabText.text = tabType.TabName;

		UpdateState(isSelected);
	}

	public void UpdateState(bool isSelected)
	{
		_actionButton.interactable = !isSelected;

		if (isSelected)
		{
			_tabText.color = _selectedIconColor;
		}
		else
		{
			_tabText.color = _deselectedIconColor;
		}
	}

	public void ClickButton()
	{
		TabClicked.Invoke(_currentTabType);
	}
}
