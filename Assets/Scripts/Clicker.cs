using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Clicker : MonoBehaviour
{
    //Canvas Group
    public Canvas gameCanvas;
    public Canvas upgradeCanvas;
    public Canvas shopCanvas;
    public Canvas rebirthCanvas;

    //Main Scoring
    private int score;
    private int increment;
    public TMP_Text scoreTxt;

    //Timing Increment
    private int incrementPerSec;
    private float nextTime;
    public TMP_Text perSecTxt;

    //Rebirth
    private int multiplier = 1;

    //Purchase Pricing, Increases, perSec
    private int[] upgradeCost = { 5, 100, 500 };
    private int[] upgradeInc = { 1, 50, 100 };
    private int[] shopCost = { 10, 500, 1000 };
    private int[] shopInc = { 1, 50, 100 };


    void Start()
    {
        gameCanvas.enabled = true;
        upgradeCanvas.enabled = false;
        shopCanvas.enabled = false;
        rebirthCanvas.enabled = false;

        score = 0;
        increment = 1;
        incrementPerSec = 0;
        scoreTxt.text = " $" + score;

        nextTime = Time.time + 1.0f;
    }

    void Update()
    {
        if(nextTime <= Time.time)
        {
            nextTime = Time.time + 1.0f;
            score += incrementPerSec * multiplier; 
            scoreTxt.text = " $" + score;
        }
    }

    public void MainBtnClick()
    {
        score += increment * multiplier;
        scoreTxt.text = " $" + score;
    }

    public void UpgradesBtn()
    {
        upgradeCanvas.enabled = true;
        gameCanvas.enabled = false;
    }

    public void ShopBtn()
    {
        shopCanvas.enabled = true;
        gameCanvas.enabled = false;
    }

    public void RebirthBtn()
    {
        rebirthCanvas.enabled = true;
        gameCanvas.enabled = false;
    }

    public void ReturnBtn()
    {
        gameCanvas.enabled = true;
        upgradeCanvas.enabled = false;
        shopCanvas.enabled = false;
        rebirthCanvas.enabled = false;
    }

    public void UpgradeBtn(int btnNumber)
    {
        if (upgradeCost[btnNumber] <= score)
        {
            score -= upgradeCost[btnNumber];
            upgradeCost[btnNumber] *= 5;
            increment += upgradeInc[btnNumber];
            upgradeInc[btnNumber] *= 3;
            scoreTxt.text = " $" + score;
        }
    }

    public void ShopBtn(int btnNumber)
    {
        if (shopCost[btnNumber] <= score)
        {
            score -= shopCost[btnNumber];
            shopCost[btnNumber] *= 5;
            incrementPerSec += shopInc[btnNumber];
            shopInc[btnNumber] *= 3;
            scoreTxt.text = " $" + score;
             perSecTxt.text = "$" + incrementPerSec * multiplier + " / sec";
        }
    }

    public void UpgradeTextBtn(TMP_Text text)
    {
        text.text = "Cost: $" + upgradeCost[Int32.Parse(text.name)] + "  Increase: $" + upgradeInc[Int32.Parse(text.name)];
    }

    public void ShopTextBtn(TMP_Text text)
    {
        text.text = "Cost: $" + shopCost[Int32.Parse(text.name)] + "  Increase: $" + shopInc[Int32.Parse(text.name)];
    }

    public void BuyRebirthBtn()
    {
        if(score >= 1000000)
        {
            score = 0;
            increment = 1;
            incrementPerSec = 0;
            upgradeCost = new[] { 5, 100, 500 };
            upgradeInc = new[] { 1, 50, 100 };
            shopCost = new[] { 10, 500, 1000 };
            shopInc = new[] { 1, 50, 100 };
            multiplier *= 2;
        }
    }
}
