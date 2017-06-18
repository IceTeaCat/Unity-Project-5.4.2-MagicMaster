using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

    public static int money=10000;
    public static int gem=1000;

    //UI
    public GameObject confirmPanel;

    public Text Money;
    public Text Gem;
    public GameObject[] skill_btn;
    public static bool[] skillopen_btn=new bool[12];

    public int BuyNumber;

    void Start () {
        Money.text = money.ToString();
        Gem.text = gem.ToString();


        for (int i = 0; i < 12; i++)
        {
            if (skillopen_btn[i])
                skill_btn[i].SetActive(true);
        }


    }
	

    public void ShowConfirmPanel(int number)
    {
        confirmPanel.SetActive(true);
        BuyNumber = number;
    }

    public void CloseConfirmPanel()
    {
        confirmPanel.SetActive(false);
    }

    public void ConfirmToBuy()
    {
        confirmPanel.SetActive(false);

        MoneyBuySkill(BuyNumber);
    }


    public void MoneyBuySkill(int number)
    {
        if (money >= 1000 && gem >= 100)
        {
            money -= 1000;
            gem -= 100;
            print("購買成功");

            skill_btn[number].SetActive(true);
            skillopen_btn[number] = true;
        }
        else
            print("購買失敗");



        Money.text = money.ToString();
        Gem.text = gem.ToString();
    }


    public void BuyGem(int g)
    {
        gem += g;
        print("購買成功");
        Gem.text = gem.ToString();
    }

    public void BuyMoneybyGem()
    {
        gem -= 10;
        money += 1000;

        Money.text = money.ToString();
        Gem.text = gem.ToString();
    }



}
