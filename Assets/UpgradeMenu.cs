using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{

	private Button _shipping;
	private Button _wine;
	private Button _wineCorn;
	private Button _wineSquash;
	private Button _wineBeets;
	
	private Button _kissUp;
	private Button _info;
	private WarehouseManager _whm;
	private Panels _panels;
	private bool _gainedLife = false;

	private int _kissUpPrice = 1000;
	private string _originalKissUpText;
	
	private bool _showWineMenu = false;
	private int _wineAndDinePrice = 500;
	private string _originalWineText;

	private bool _shippingUpgradeLimit = false;
	private int _upgradeShippingPrice = 1500;
	private string _originalShippingText;

	private bool _showInfoMenu = false;

	private ModifyOrder _modifyOrder;
	
	// Use this for initialization
	void Start ()
	{
		_shipping = transform.Find("Upgrade Shipping").gameObject.GetComponent<Button>();
		_wine = transform.Find("Wine & Dine").gameObject.GetComponent<Button>();
		_kissUp = transform.Find("Kiss up").gameObject.GetComponent<Button>();
		_info = transform.Find("Info").gameObject.GetComponent<Button>();
		_whm = GameObject.Find("Main Camera").GetComponent<WarehouseManager>();
		
		_wineCorn = _wine.transform.Find("Corn").gameObject.GetComponent<Button>();
		_wineSquash = _wine.transform.Find("Squash").gameObject.GetComponent<Button>();
		_wineBeets = _wine.transform.Find("Beets").gameObject.GetComponent<Button>();

        _wineCorn.gameObject.transform.localScale = new Vector3(0, 0, 0);
        _wineSquash.gameObject.transform.localScale = new Vector3(0, 0, 0);
        _wineBeets.gameObject.transform.localScale = new Vector3(0, 0, 0);
		
		_panels = FindObjectOfType<Panels>();
		
		_kissUp.onClick.AddListener(KissUp);
		_wine.onClick.AddListener(WineAndDine);
		_info.onClick.AddListener(Info);
		
		_wineCorn.onClick.AddListener(WineCorn);
		_wineBeets.onClick.AddListener(WineBeets);
		_wineSquash.onClick.AddListener(WineSquash);
		_shipping.onClick.AddListener(UpgradeShipping);

		_modifyOrder = FindObjectOfType<ModifyOrder>();
		

		_originalWineText = _wine.transform.Find("Text").GetComponent<Text>().text;
		_wine.transform.Find("Text").GetComponent<Text>().text = _originalWineText + " ($" + _wineAndDinePrice + ")";

		_originalShippingText = _shipping.transform.Find("Text").GetComponent<Text>().text;
		_shipping.transform.Find("Text").GetComponent<Text>().text = _originalShippingText + " ($" + _upgradeShippingPrice + ")";
		
		_originalKissUpText = _kissUp.transform.Find("Text").GetComponent<Text>().text;
		_kissUp.transform.Find("Text").GetComponent<Text>().text = _originalKissUpText + " ($" + _kissUpPrice + ")";
		
		
		HideUpgradeMenu();
		HideInfoDescriptions();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!_gainedLife && _whm.Money >= _kissUpPrice)
			_kissUp.interactable = true;
		else
			_kissUp.interactable = false;

		if (!_shippingUpgradeLimit && _whm.Money >= _upgradeShippingPrice)
			_shipping.interactable = true;
		else
			_shipping.interactable = false;
		
		if (_whm.Money >= _wineAndDinePrice)
			_wine.interactable = true;
		else
			_wine.interactable = false;
	}

	void UpgradeShipping()
	{
		// can basically extend 2 days
		_whm.NewOrderDurationOffset += 1;
		if (_whm.NewOrderDurationOffset == 1)
		{
			_shippingUpgradeLimit = true;
			_shipping.interactable = false;
		}

		_upgradeShippingPrice *= 2;
	}

	void WineAndDine()
	{
		if (!_showWineMenu)
		{
			if (_whm.Money < _wineAndDinePrice)
			{
				_wineCorn.interactable = false;
				_wineBeets.interactable = false;
				_wineSquash.interactable = false;
			}
			else
			{
				_wineCorn.interactable = true;
				_wineBeets.interactable = true;
				_wineSquash.interactable = true;
			}
			
			DisplayWineMenu();	
			HideInfoDescriptions();
		}
		else
		{
			HideWineMenu();
			DisplayInfoDescriptions();
		}

	}

	void DisplayWineMenu()
	{
        _wineCorn.gameObject.transform.localScale = new Vector3(1, 1, 1);
        _wineSquash.gameObject.transform.localScale = new Vector3(1, 1, 1);
        _wineBeets.gameObject.transform.localScale = new Vector3(1, 1, 1);
		_showWineMenu = true;
	}

	void HideWineMenu()
	{
        _wineCorn.gameObject.transform.localScale = new Vector3(0, 0, 0);
        _wineSquash.gameObject.transform.localScale = new Vector3(0, 0, 0);
        _wineBeets.gameObject.transform.localScale = new Vector3(0, 0, 0);
		_showWineMenu = false;
	}
	
	void WineCorn()
	{
		if (_whm.capPrices["Corn"] > 8)
		{
			_whm.buyPrices["Corn"] -= 2;
            _whm.capPrices["Corn"] -= 3;
            _whm.Buy(_wineAndDinePrice);
			_whm.ChangeVeggiePrice("Corn", 0);
            _modifyOrder.UpdatePrices();

			_wineAndDinePrice = Mathf.RoundToInt(_wineAndDinePrice * 1.5f);
			_wine.transform.Find("Text").GetComponent<Text>().text = _originalWineText + " ($" + _wineAndDinePrice + ")";

			if (_whm.Money < _wineAndDinePrice)
			{
				_wineCorn.interactable = false;
				_wineBeets.interactable = false;
				_wineSquash.interactable = false;
			}
		}
	}
	
	void WineBeets()
	{
		if (_whm.capPrices["Beets"] > 8)
		{
			_whm.buyPrices["Beets"] -= 2;
            _whm.capPrices["Beets"] -= 3;
            _whm.Buy(_wineAndDinePrice);
			_whm.ChangeVeggiePrice("Beets", 0);
            _modifyOrder.UpdatePrices();

			_wineAndDinePrice = Mathf.RoundToInt(_wineAndDinePrice * 1.5f);
			_wine.transform.Find("Text").GetComponent<Text>().text = _originalWineText + " ($" + _wineAndDinePrice + ")";

			if (_whm.Money < _wineAndDinePrice)
			{
				_wineCorn.interactable = false;
				_wineBeets.interactable = false;
				_wineSquash.interactable = false;
			}
		}
	}
	
	void WineSquash()
	{
		if (_whm.capPrices["Squash"] > 8)
		{
			_whm.buyPrices["Squash"] -= 2;
            _whm.capPrices["Squash"] -= 3;
            _whm.Buy(_wineAndDinePrice);
			_whm.ChangeVeggiePrice("Squash", 0);
            _modifyOrder.UpdatePrices();

			_wineAndDinePrice = Mathf.RoundToInt(_wineAndDinePrice * 1.5f);
			_wine.transform.Find("Text").GetComponent<Text>().text = _originalWineText + " ($" + _wineAndDinePrice + ")";

			if (_whm.Money < _wineAndDinePrice)
			{
				_wineCorn.interactable = false;
				_wineBeets.interactable = false;
				_wineSquash.interactable = false;
			}
		}
	}

	void KissUp()
	{
		_gainedLife = true;
		_kissUp.interactable = false;
		_whm.failed -= 1;
		_whm.hasKissedUp = true;
		_panels.UpdateLives();
		_whm.Buy(_kissUpPrice);

		var tutorial = FindObjectOfType<Tutorial>();
		tutorial.BossSpeakKissUp();
	}

	void Info()
	{

		if (!_showInfoMenu)
		// bringing the menu up
		{
			if (_showWineMenu)
			{
				HideWineMenu();
			}
			DisplayInfoDescriptions();
		}
		else
		// clicking the menu away
		{
			HideInfoDescriptions();
			if (_showWineMenu)
			{
				DisplayWineMenu();
			}
		}
		
		_showInfoMenu = !_showInfoMenu;
	}
	
	void DisplayInfoDescriptions()
	{
		var information = transform.Find("Info");
        information.transform.Find("Upgrade Shipping Descripton").gameObject.GetComponent<Text>().gameObject.transform.localScale = new Vector3(1, 1, 1);
        information.transform.Find("Wine Description").gameObject.GetComponent<Text>().gameObject.transform.localScale = new Vector3(1, 1, 1);
        information.transform.Find("Kiss up Description").gameObject.GetComponent<Text>().gameObject.transform.localScale = new Vector3(1, 1, 1);
        information.transform.Find("Upgrade Storage Description").gameObject.GetComponent<Text>().gameObject.transform.localScale = new Vector3(1, 1, 1);
	}

	void HideInfoDescriptions()
	{
		var information = transform.Find("Info");
        information.transform.Find("Upgrade Shipping Descripton").gameObject.GetComponent<Text>().gameObject.transform.localScale = new Vector3(0, 0, 0);
        information.transform.Find("Wine Description").gameObject.GetComponent<Text>().gameObject.transform.localScale = new Vector3(0, 0, 0);
        information.transform.Find("Kiss up Description").gameObject.GetComponent<Text>().gameObject.transform.localScale = new Vector3(0, 0, 0);
        information.transform.Find("Upgrade Storage Description").gameObject.GetComponent<Text>().gameObject.transform.localScale = new Vector3(0, 0, 0);
	}

	public void HideUpgradeMenu()
	{
		transform.localScale = new Vector3(0, 0, 0);
	}
	
	public void ShowUpgradeMenu()
	{
		transform.localScale = new Vector3(1, 1, 1);
	}
}
