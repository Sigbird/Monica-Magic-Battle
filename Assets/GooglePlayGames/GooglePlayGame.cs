using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;


public class GooglePlayGame : MonoBehaviour {



	// Use this for initialization
	void Start () {
		Init ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>

	/// Inicializa a pltaforma

	/// </summary>

	public static void Init()

	{

		PlayGamesPlatform.DebugLogEnabled = true;

		// Ativando

		PlayGamesPlatform.Activate();

	}

	// login

	/// <summary>

	/// Loga na pltaforma se já não estiver logado e a plataforma estivar ativa

	/// </summary>

	/// <param name=”onLogin”></param>

//	public static void Login(bool onLogin)
//
//	{
//
//		if(Social.Active == null)
//
//		{
//
//			Debug.LogError("plataforma inativa");
//
//			return;
//
//		}
//
//		if (IsAuthenticated())
//
//		{
//
//			return; // verificando se já está logado
//
//		}
//
//		Social.localUser.Authenticate((bool success) => {
//
//			Debug.Log(success);
//
//			if(onLogin != null)
//
//				onLogin(success);
//
//		});
//
//	}


	/// <summary>

	/// Status da autenticacao

	/// </summary>

	/// <returns>true se está logado e false se não</returns>

	public static bool IsAuthenticated()

	{

		return Social.localUser.authenticated;

	}



}
