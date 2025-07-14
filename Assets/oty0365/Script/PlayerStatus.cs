using System;
using Unity.Collections;
using UnityEngine;

public class PlayerStatus : HalfSingleMono<PlayerStatus>
{
    [SerializeField] private PlayerBasicStatusData playerBasicStatusData;
    
    private float _playerMaxHp;
    private float _playerHp;
    private float _playerDef;
    private float _playerAtk;
    private float _playerMoveSpeed;
    private float _playerAttackSpeed;
    private float _playerExp;
    private float _playerLevel;
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
            }
        }
    }

    public float PlayerExp
    {
        get => _playerExp;
        set
        {
            if (_playerExp != value)
            {
                _playerExp = value;
            }

            if (value > PlayerMaxExp)
            {
                _playerExp = 0;
                PlayerLevel++;
                PlayerMaxExp = PlayerMaxExp/(PlayerLevel-1)*PlayerLevel;
            }
        }
        
    }

    public float PlayerLevel
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
}
