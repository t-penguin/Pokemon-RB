using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Battle : MonoBehaviour
{
    #region UI Events

    public static event Action<int> BattleStartAnimation;


    public static void StartBattle(int animationType) => BattleStartAnimation?.Invoke(animationType);

    #endregion

    #region UI Fields

    [Header("Player Side Info")]
    [SerializeField] GameObject _playerTeamInfo;
    [SerializeField] List<Image> _playerTeamIcons;
    [SerializeField] GameObject _playerPokemonInfo;
    [SerializeField] Text _playerNicknameText;
    [SerializeField] Text _playerLevelText;
    [SerializeField] Text _playerHealthText;
    [SerializeField] Image _playerHealthBar;
    [Space(10)]

    [Header("Enemy Side Info")]
    [SerializeField] GameObject _enemyPokemonInfo;
    [SerializeField] Text _enemyNicknameText;
    [SerializeField] Text _enemyLevelText;
    [SerializeField] Image _enemyHealthBar;

    [Header("Selection Menu")]
    [SerializeField] GameObject _selectionMenu;
    [SerializeField] RectTransform _selectionArrow;
    private int _selection;
    [Space(10)]

    [Header("Move Selection Menu")]
    [SerializeField] GameObject _movesMenu;
    [SerializeField] List<Text> _movesText;
    [SerializeField] RectTransform _movesArrow;
    private int _moveSelection;
    [SerializeField] GameObject _infoMenu;
    [SerializeField] Text _typeText;
    [SerializeField] Text _ppText;

    #endregion

    [SerializeField] Tilemap _battleTilemap;
    [SerializeField] TileBase _tile;

    private void Start()
    {
        TilemapEditor.SetTilemapToView(_battleTilemap, new Vector2Int(0, 0));
    }
}

public enum BattleMenu
{

}