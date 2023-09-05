using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameReferance 
{
    internal static Transform CanvasGame => _canvasGame != null ? _canvasGame : _canvasGame = GameObject.FindObjectOfType<CanvasGame>().transform;
    private static Transform _canvasGame;
    
    internal static Transform EnamyCardContainer => _enamyCardContainer != null ? _enamyCardContainer : _enamyCardContainer = GameObject.FindObjectOfType<EnamyCardContainer>().transform;
    private static Transform _enamyCardContainer;
    
    internal static Transform PlayerCardContainer => _playerCardContainer != null ? _playerCardContainer : _playerCardContainer = GameObject.FindObjectOfType<PlayerCardContainer>().transform;
    private static Transform _playerCardContainer;
    
    internal static Transform DragonDang => _dragonDang != null ? _dragonDang : _dragonDang = GameObject.FindObjectOfType<DragonDang>().transform;
    private static Transform _dragonDang;
    
    internal static Transform Cemetery => _сemetery != null ? _сemetery : _сemetery = GameObject.FindObjectOfType<Cemetery>().transform;
    private static Transform _сemetery;

}
