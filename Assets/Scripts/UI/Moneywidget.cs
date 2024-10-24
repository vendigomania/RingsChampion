using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Moneywidget : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyValueLable;

    IEnumerator Start()
    {
        yield return null;

        PlayerData.OnChangeMoneyValue += UpdateInfo;

        UpdateInfo();
    }

    private void UpdateInfo()
    {
        moneyValueLable.text = PlayerData.Instance.Money.ToString();
    }
}
