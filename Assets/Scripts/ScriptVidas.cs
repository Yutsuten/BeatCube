using UnityEngine;
using System.Collections;

public class ScriptVidas : MonoBehaviour 
{
	public int numLives = 10;

	private int life;
	private bool onGame = true;
	private GUIStyle styleLife = new GUIStyle();
	private GUIStyle styleGameOver = new GUIStyle();

	public void ResetLives()
	{
		life = numLives * 100;
		onGame = true;
	}

	public void IncLife()
	{
		if (onGame) 
		{
			life += 1;
			if (life > numLives * 100)
					life = numLives * 100;
		}
	}

	public void DecLife (int qt)
	{
		life -= qt * 100;
		if (life < 10)
		{
			life = 0;
			onGame = false;
			//GetComponent<Jogador>().enabled = false;
			ReduceTimeScale();
            GetComponent<ScriptMenu>().NewScore(GetComponent<ScriptPontuacao>().RetornaPontuacao());
		}
	}

	private void ReduceTimeScale()
	{
		if (!onGame)
		{
			if (Time.timeScale > 0.1)
			{
				Time.timeScale -= 0.1f;
				Invoke("ReduceTimeScale", 0.05f);
			}
			else
				Time.timeScale = 0;
		}
	}

	private void EnableTargets()
	{  // EXISTE COPIA DISSO EM SCRIPT PAUSE
		//ScriptTarget.DESTROY_ALL_TARGETS = false;
	}

	void OnGUI()
	{
		GUI.Label(new Rect(2*Screen.width/100, 2*Screen.height/100, Screen.width/10, Screen.height/30), "Lives: " + (life/100) + "." + ((life/10)%10) + "\nFPS:" + ScriptMenu.FRAMERATE, styleLife);
		if (!onGame)
		{
            GUI.Box(new Rect((float)20 * Screen.width / 100, (float)33 * Screen.height / (float)100, Screen.width / (float)1.75, Screen.height / 4), "");
			GUI.Label(new Rect(27*Screen.width/100, 35*Screen.height/100, Screen.width/10, Screen.height/30), "Game Over!", styleGameOver);
            GUI.Label(new Rect(24 * Screen.width / 100, 42 * Screen.height / 100, Screen.width / 10, Screen.height / 30), "Score: " + (GetComponent<ScriptPontuacao>().RetornaPontuacao()), styleGameOver);
			if (GUI.Button(new Rect(22*Screen.width/100, 50*Screen.height/100, Screen.width/4, Screen.height/15), "Menu"))
			{  // EXISTE COPIA DISSO EM SCRIPT PAUSE
				//print("Botao Menu pressionado.");
				GetComponent<ScriptPontuacao>().ResetaContadores();
				GetComponent<ScriptMenu>().EnableMenu();
				GetComponent<ScriptEspecial>().ResetaEspecial();
				ResetLives();
				//ScriptTarget.DESTROY_ALL_TARGETS = true;
				//GetComponent<Jogador>().enabled = true;
				Invoke("EnableTargets", 0.05f);
				//DestroyCubes();
				Time.timeScale = 1.0f;
			}
			if (GUI.Button(new Rect(50*Screen.width/100, 50*Screen.height/100, Screen.width/4, Screen.height/15), "Restart"))
			{  // EXISTE COPIA DISSO EM SCRIPT PAUSE
				//print("Botao Restart pressionado.");
				GetComponent<ScriptPontuacao>().ResetaContadores();
				GetComponent<ScriptNiveis>().AtualizaLevel();
				GetComponent<ScriptEspecial>().ResetaEspecial();
				ResetLives();
				//GetComponent<Jogador>().enabled = true;
				//ScriptTarget.DESTROY_ALL_TARGETS = true;
				Invoke("EnableTargets", 0.05f);
				Time.timeScale = 1.0f;
			}
		}
	}

	// Use this for initialization
	void Start () 
	{
		styleLife.fontSize = Screen.width/20;
		styleLife.normal.textColor = Color.white;
		styleGameOver.fontSize = Screen.width/15;
		styleGameOver.normal.textColor = Color.white;
		life = numLives * 100;
		this.enabled = !ScriptMenu.menuEnabled;	
	}
}
