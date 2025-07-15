using System;
using Unity.Collections;
using UnityEngine;

public class PlayerStatus : HalfSingleMono<PlayerStatus>
{
    public event Action<float> OnMaxExp;
    public event Action<float> OnExp;
    [SerializeField] private PlayerBasicStatusData playerBasicStatusData;
    
    private float _playerMaxHp;
    private float _playerHp;
    private float _playerDef;
    private float _playerAtk;
    private float _playerMoveSpeed;
    private float _playerAttackSpeed;
    private float _playerExp;
    private int _playerLevel;
    private float _playerMaxExp;

    public float PlayerMaxHp
    {
        get=>_playerMaxHp;
        private set
        {
            if (value != _playerMaxHp)
            {
                _playerHp = value;
            }

            if (value < PlayerHp)
            {
                PlayerHp = value;
            }
        }
    }
    
    public float PlayerHp
    {
        get => _playerHp;
        private set
        {
            if (value != _playerHp)
            {
                _playerHp = value;
            }

            if (value < 0)
            {
                _playerHp = 0;
            }

            if (value > PlayerMaxHp)
            {
                _playerHp = PlayerMaxHp;
            }
        }
    }

    public float PlayerDef
    {
        get => _playerDef;
        set
        {
            if (_playerDef != value)
            {
                _playerDef = value;
            }
        }
    }

    public float PlayerAtk
    {
        get => _playerAtk;
        private set
        {
            if (_playerAtk != value)
            {
                _playerAtk = value;
            }
        }
    }

    public float PlayerMoveSpeed
    {
        get => _playerMoveSpeed;
        private set
        {
            if (_playerMoveSpeed != value)
            {
                _playerMoveSpeed = value;
            }
        }
    }

    public float PlayerAttackSpeed
    {
        get => _playerAttackSpeed;
        private set
        {
            if (_playerAttackSpeed != value)
            {
                _playerAttackSpeed = value;
            }
        }
    }

    public float PlayerMaxExp
    {
        get => _playerMaxExp;
        private set
        {
            if (_playerMaxExp != value)
            {
                _playerMaxExp = value;
                OnMaxExp?.Invoke(_playerMaxExp);
            }
        }
    }

    public float PlayerExp
    {
        get => _playerExp;
        set
        {
            if (value >= 0)
            {
                float delta = value - _playerExp;
                AddExp(delta);
            }
        }
    }

    public void AddExp(float expGained)
    {
        if (expGained <= 0) return;

        _playerExp += expGained;

        if (_playerExp >= PlayerMaxExp)
        {
            HandleLevelUpLogic();
        }
        else
        {
            OnExp?.Invoke(_playerExp);
        }
    }

    private void HandleLevelUpLogic()
    {
        while (_playerExp >= PlayerMaxExp)
        {
            float currentMaxExp = PlayerMaxExp;
            _playerExp -= currentMaxExp;
            PlayerLevel++;
        
            PlayerMaxExp = CalculateExpRequirement(PlayerLevel);
        
            AugmentManager.Instance.AugmentSelection();
        }
    
        OnExp?.Invoke(_playerExp);
    }

    private float CalculateExpRequirement(int level)
    {
        float baseExp = 100f;
        float multiplier = 1.2f;
        return baseExp * Mathf.Pow(multiplier, level - 1);
    }

    private float CalculateExpRequirementLinear(int level)
    {
        float baseExp = 100f;
        float increment = 50f;
        return baseExp + (increment * (level - 1));
    }

    public int PlayerLevel
    {
        get => _playerLevel;
        private set
        {
            if (_playerLevel != value)
            {
                _playerLevel = value;
            }
        }
    }
    private void Start()
    {
        OnMaxExp += PlayerStatusUi.Instance.SetMaxExp;
        OnExp += PlayerStatusUi.Instance.SetExp;
        
        PlayerMaxHp = playerBasicStatusData.playerMaxHp;
        PlayerHp = PlayerMaxHp;
        PlayerMoveSpeed = playerBasicStatusData.playerMoveSpeed;
        PlayerAtk = playerBasicStatusData.playerAtk;
        PlayerDef = playerBasicStatusData.playerDef;
        PlayerMaxExp = playerBasicStatusData.playerMaxExp;
        PlayerAttackSpeed = playerBasicStatusData.playerAttackSpeed;
        PlayerExp = 0;
        PlayerLevel = 1;
    }

    public void SetExp(float exp)
    {
        PlayerExp = exp;
    }
}
