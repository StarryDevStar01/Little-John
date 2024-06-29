using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using UnityEngine.UI;
using TMPro;
using System.Numerics;
using Newtonsoft.Json.Linq;

public class BlockchainManager : MonoBehaviour
{
    public string Address { get; private set; }
    public Button playBtn;
    public Button NFTShopBtn;
    public Button NFTTokenGateBtn;
    public TextMeshProUGUI NFTTokenGateTxt;
    public GameObject NFTShopGUI;
    public GameObject GameGUI;
    public GameObject HomeGUI;
    public GameObject progressPanel;
    public Text stageTxt;
    public Text weaponTxt;
    public Text goldTxt;

    public Button buy100CoinBtn;
    public TextMeshProUGUI buy100CoinBtnTxt;
    public Button buy200CoinBtn;
    public TextMeshProUGUI buy200CoinBtnTxt;
    public Button buy300CoinBtn;
    public TextMeshProUGUI buy300CoinBtnTxt;

    public PlayerStats playerStats;
    public EnemyStats enemyStats;
    public WeaponStats weaponStats;

    public GameObject loginButton;

    public Button closeButton;

    private void Start()
    {
        HomeGUI.SetActive(true);
        NFTShopGUI.SetActive(false);
        GameGUI.SetActive(false);
    }

    public async void Login()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();
        //SaveProgress();
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        Contract contract = ThirdwebManager.Instance.SDK.GetContract("0xc8707F503e3610c6cF86b9e284109bD6d03805A6");
        List<NFT> nftList = await contract.ERC721.GetOwned(Address);
        if (nftList.Count == 0)
        {
            NFTTokenGateBtn.gameObject.SetActive(true);
        }
        else
        {
            NFTTokenGateBtn.gameObject.SetActive(false);
            Debug.Log("Login");
            LoadProgress();
        }
    }

    public void OpenNFTShop()
    {
        HomeGUI.SetActive(false);
        NFTShopGUI.SetActive(true);
        GameGUI.SetActive(false);
    }

    public void CloseNFTShop()
    {
        HomeGUI.SetActive(true);
        NFTShopGUI.SetActive(false);
        GameGUI.SetActive(false);
    }
    public async void ClaimNFTPass()
    {
        NFTTokenGateTxt.text = "Claiming...";
        NFTTokenGateBtn.interactable = false;
        var contract = ThirdwebManager.Instance.SDK.GetContract("0xc8707F503e3610c6cF86b9e284109bD6d03805A6");
        var result = await contract.ERC721.ClaimTo(Address, 1);
        NFTTokenGateTxt.text = "Claim PASS!";
        NFTTokenGateBtn.gameObject.SetActive(false);
        playBtn.gameObject.SetActive(true);
        NFTShopBtn.gameObject.SetActive(true);
        NFTShopBtn.gameObject.SetActive(true);
        progressPanel.SetActive(true);
    }

    public async void Buy100Coins()
    {
        buy100CoinBtnTxt.text = "Buying...";
        buy100CoinBtn.interactable = false;
        buy200CoinBtn.interactable = false;
        buy300CoinBtn.interactable = false;
        closeButton.interactable = false;
        var contract = ThirdwebManager.Instance.SDK.GetContract("0x080CDF8dCe4Bf4005FbcbC7f497BBDB673F5C263");
        var result = await contract.ERC20.Claim("1");
        buy100CoinBtnTxt.text = "Buy 100 Coins";
        Prefs.coins += 100;
        SaveProgress();
    }

    public async void Buy200Coins()
    {
        buy200CoinBtnTxt.text = "Buying...";
        buy100CoinBtn.interactable = false;
        buy200CoinBtn.interactable = false;
        buy300CoinBtn.interactable = false;
        closeButton.interactable = false;
        var contract = ThirdwebManager.Instance.SDK.GetContract("0xc06e906a99289A2Add385E8303Ca3F0619bFb977");
        var result = await contract.ERC20.Claim("1");
        buy200CoinBtnTxt.text = "Buy 200 Coins";
        Prefs.coins += 200;
        SaveProgress();
    }

    public async void Buy300Coins()
    {
        buy300CoinBtnTxt.text = "Buying...";
        buy100CoinBtn.interactable = false;
        buy200CoinBtn.interactable = false;
        buy300CoinBtn.interactable = false;
        closeButton.interactable = false;
        var contract = ThirdwebManager.Instance.SDK.GetContract("0x2cfF32c2D2702aDfb21F08EA89B6f3e8F05dd981");
        var result = await contract.ERC20.Claim("1");
        buy300CoinBtnTxt.text = "Buy 300 Coins";
        Prefs.coins += 300;
        SaveProgress();
    }

    public async void LoadProgress()
    {
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        //string addressProgress = "0xA24d7ECD79B25CE6C66f1Db9e06b66Bd11632E00";
        var contract = ThirdwebManager.Instance.SDK.GetContract(
             "0xc99C9e23EC59322d41376e05c7569d6f95Fc4Cd1",
            "[{\"type\":\"function\",\"name\":\"getPlayerGold\",\"inputs\":[{\"type\":\"address\",\"name\":\"_player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"getPlayerLevelJson\",\"inputs\":[{\"type\":\"address\",\"name\":\"_player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"string\",\"name\":\"\",\"internalType\":\"string\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"getPlayerMonsterLevelJson\",\"inputs\":[{\"type\":\"address\",\"name\":\"_player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"string\",\"name\":\"\",\"internalType\":\"string\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"getPlayerWeaponLevelJson\",\"inputs\":[{\"type\":\"address\",\"name\":\"_player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"string\",\"name\":\"\",\"internalType\":\"string\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"setPlayerStats\",\"inputs\":[{\"type\":\"address\",\"name\":\"_player\",\"internalType\":\"address\"},{\"type\":\"string\",\"name\":\"_levelJson\",\"internalType\":\"string\"},{\"type\":\"string\",\"name\":\"_weaponLevelJson\",\"internalType\":\"string\"},{\"type\":\"string\",\"name\":\"_monsterLevelJson\",\"internalType\":\"string\"},{\"type\":\"uint256\",\"name\":\"_gold\",\"internalType\":\"uint256\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"}]"
            );
        Debug.Log("LoadProgress");
        string playerLevel = await contract.Read<string>("getPlayerLevelJson", Address);
        string weaponLevel = await contract.Read<string>("getPlayerWeaponLevelJson", Address);
        string monsterLevel = await contract.Read<string>("getPlayerMonsterLevelJson", Address);
        BigInteger coinLevel = await contract.Read<BigInteger>("getPlayerGold", Address);

        if ((playerLevel == "") || weaponLevel == "" || monsterLevel == "") {
            playBtn.gameObject.SetActive(true);
            NFTShopBtn.gameObject.SetActive(true);
            NFTShopBtn.gameObject.SetActive(true);
            progressPanel.SetActive(true);
            return;
        }

        JObject jsonplayerLevel = JObject.Parse(playerLevel);
        int playerLevelTxt = jsonplayerLevel["level"].Value<int>();
        stageTxt.text = "Stage: " + playerLevelTxt.ToString();

        JObject jsonweaponLevel = JObject.Parse(weaponLevel);
        int levelWeapon = jsonweaponLevel["level"].Value<int>();
        weaponTxt.text = "Gun Level: " + levelWeapon.ToString();

        goldTxt.text = "Gold: " + coinLevel.ToString();

        Prefs.playerData = playerLevel;
        Prefs.playerWeaponData = weaponLevel;
        Prefs.enemyData = monsterLevel;

        playerStats.Load();
        enemyStats.Load();
        weaponStats.Load();
        Prefs.coins = (int)coinLevel;

        playBtn.gameObject.SetActive(true);
        NFTShopBtn.gameObject.SetActive(true);
        NFTShopBtn.gameObject.SetActive(true);
        progressPanel.SetActive(true);
    }

    public async void SaveProgress()
    {
        Debug.Log("Save Progress");
        loginButton.SetActive(false);
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        //string addressProgress = "0xA24d7ECD79B25CE6C66f1Db9e06b66Bd11632E00";
        var contract = ThirdwebManager.Instance.SDK.GetContract(
            "0xc99C9e23EC59322d41376e05c7569d6f95Fc4Cd1",
            "[{\"type\":\"function\",\"name\":\"getPlayerGold\",\"inputs\":[{\"type\":\"address\",\"name\":\"_player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"getPlayerLevelJson\",\"inputs\":[{\"type\":\"address\",\"name\":\"_player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"string\",\"name\":\"\",\"internalType\":\"string\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"getPlayerMonsterLevelJson\",\"inputs\":[{\"type\":\"address\",\"name\":\"_player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"string\",\"name\":\"\",\"internalType\":\"string\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"getPlayerWeaponLevelJson\",\"inputs\":[{\"type\":\"address\",\"name\":\"_player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"string\",\"name\":\"\",\"internalType\":\"string\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"setPlayerStats\",\"inputs\":[{\"type\":\"address\",\"name\":\"_player\",\"internalType\":\"address\"},{\"type\":\"string\",\"name\":\"_levelJson\",\"internalType\":\"string\"},{\"type\":\"string\",\"name\":\"_weaponLevelJson\",\"internalType\":\"string\"},{\"type\":\"string\",\"name\":\"_monsterLevelJson\",\"internalType\":\"string\"},{\"type\":\"uint256\",\"name\":\"_gold\",\"internalType\":\"uint256\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"}]"
            );
        await contract.Write("setPlayerStats", Address,
            JsonUtility.ToJson(playerStats),
            JsonUtility.ToJson(weaponStats),
            JsonUtility.ToJson(enemyStats),
            Prefs.coins);
        string playerLevel = await contract.Read<string>("getPlayerLevelJson", Address);
        string weaponLevel = await contract.Read<string>("getPlayerWeaponLevelJson", Address);
        string monsterLevel = await contract.Read<string>("getPlayerMonsterLevelJson", Address);
        string coinLevel = await contract.Read<string>("getPlayerGold", Address);
        Debug.Log(playerLevel);
        Debug.Log(weaponLevel);
        Debug.Log(monsterLevel);
        Debug.Log(coinLevel);
        loginButton.SetActive(true);
        buy100CoinBtn.interactable = true;
        buy200CoinBtn.interactable = true;
        buy300CoinBtn.interactable = true;
        closeButton.interactable = true;
    }
}
