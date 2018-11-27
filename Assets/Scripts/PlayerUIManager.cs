using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject AlivePanel;

    [SerializeField]
    private GameObject DeadPanel;

    [SerializeField]
    private TextMeshProUGUI NameText;
    [SerializeField]
    private Image HealthBarImage;
    [SerializeField]
    private TextMeshProUGUI HPText;
    [SerializeField]
    private GameObject CrownObject;
    [SerializeField]
    private Image WeaponImage;

    [SerializeField]
    private int MaxHP;
    [SerializeField]
    private int CurrentHP;

    private bool IsCrownActive = false;
    private float TimeWithCrown = 0;

    [SerializeField]
    private float TimeToWin = 60;
    [SerializeField]
    private TextMeshProUGUI TimerWithCrownText;

    private bool Alive = true;

    private float DeathTime = 5;
    private float CurrentDeathTime = 5;
    [SerializeField]
    private TextMeshProUGUI DeathTimer;

    private int IdPlayer;

    void Update()
    {
        if (Alive)
            UpdateAlive();
        else
            UpdateDead();
    }

    private void UpdateDead()
    {
        CurrentDeathTime -= Time.deltaTime;
        DeathTimer.text = CurrentDeathTime.ToString("0.00");
        if (CurrentDeathTime <= 0)
            PlayerRespawn();
    }

    private void UpdateAlive()
    {
        if (IsCrownActive)
        {
            TimeWithCrown += Time.deltaTime;
            TimerWithCrownText.text = TimeWithCrown.ToString("0.00");
            if (TimeWithCrown >= TimeToWin)
            {
                GameManager.instance.GameUIManager.WinGame(IdPlayer);
            }
        }
    }

    public void Init(int id, int maxHP = 100, float deathTime = 5)
    {
        IdPlayer = id;
        NameText.text = "Player " + (id + 1);
        MaxHP = maxHP;
        CurrentHP = maxHP;
        DeathTime = deathTime;
        CurrentDeathTime = deathTime;
    }

    public void DropCrown()
    {
        IsCrownActive = false;
        CrownObject.SetActive(false);
    }
    public void PickCrown()
    {
        IsCrownActive = true;
        CrownObject.SetActive(true);
    }

    public void SetHP(int hp)
    {
        CurrentHP = hp;
        HPText.text = hp + "/100 HP";
        HealthBarImage.fillAmount = (float)CurrentHP / (float)MaxHP;
        if (CurrentHP <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        Alive = false;
        AlivePanel.SetActive(false);
        DeadPanel.SetActive(true);
        DeathTimer.text = DeathTime.ToString("0.00");
    }

    private void PlayerRespawn()
    {
        Alive = true;
        AlivePanel.SetActive(true);
        DeadPanel.SetActive(false);
        CurrentDeathTime = DeathTime;
        CurrentHP = MaxHP;
        SetHP(MaxHP);
        GameManager.instance.RespawnPlayer(IdPlayer);
    }

    public void SetWeapon(Sprite weapon)
    {
        WeaponImage.sprite = weapon;
    }
}
