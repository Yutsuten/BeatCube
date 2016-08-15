using UnityEngine;
using System.Collections;

public class ScriptMenu : MonoBehaviour 
{
    public static int FRAMERATE;
    public static float T_EXP_BOTAO;
    public static bool menuEnabled = true;

	public float tExplosao;
	public Transform botoesMenu;

	private int iteracoes;
	private int[] topScores = new int[5];
	private float tempo;
	private bool showAbout;
	private string textoAbout = "Highscores:\n";
	private string strPlay;
	private string strAbout;
	private string strExit;
	private Transform btMenu;
	private Collider alvo;
	private GUIStyle stylePlay = new GUIStyle();
	private GUIStyle styleAbout = new GUIStyle();
	private GUIStyle styleExit = new GUIStyle();
	private GUIStyle styleProducers = new GUIStyle();

    private void LoadScores()
    {
	    int i;
	    string str;
	    textoAbout = "Highscores:\n";
	    for (i = 0; i < 5; i++)
	    {
		    topScores[i] = 0;
		    str = "Score" + i;
		    //PlayerPrefs.SetInt(str, 0); // Reseta pontuacao
		    if (PlayerPrefs.HasKey(str))
			    topScores[i] = PlayerPrefs.GetInt(str);
		    textoAbout += "\n" + (i+1) + "º: " + topScores[i];
	    }
    }

    public void NewScore(int pontuacao)
    {
	    int i;
	    int j;
	    string str;
	    for (i = 0; i < 5; i++)
	    {
		    if (topScores[i] < pontuacao)
		    {
			    // EMPURRANDO PARA BAIXO AS PONTUACOES
			    for (j = 4; j > i; j--)
			    {
				    topScores[j] = topScores[j-1];
			    }
			    topScores[i] = pontuacao;
			    // ATUALIZANDO A TABELA DE PONTUACOES
			    textoAbout = "Highscores:\n";
			    for (j = 0; j < 5; j++)
			    {
				    str = "Score" + j;
				    PlayerPrefs.SetInt(str, topScores[j]);
				    textoAbout += "\n" + (j+1) + "º: " + topScores[j];
			    }
			    PlayerPrefs.Save();
			    break;
		    }
	    }
	    return;
    }

    public void EnableMenu()
    {
        GetComponent<ScriptSpawn>().CancelInvoke();
        GetComponent<ScriptEspecial>().CancelInvoke();
        GetComponent<ScriptPontuacao>().enabled = false;
        GetComponent<ScriptVidas>().enabled = false;
        GetComponent<ScriptNiveis>().enabled = false;
        GetComponent<ScriptSpawn>().enabled = false;
        GetComponent<ScriptEspecial>().enabled = false;
        GetComponent<ScriptPause>().ResetaLbAbout();
        menuEnabled = true;
        Instantiate(botoesMenu, new Vector3(-1.8f, 2.5f, -0.2f), Quaternion.identity);
        stylePlay.normal.textColor = Color.white;
        styleAbout.normal.textColor = Color.white;
        styleExit.normal.textColor = Color.white;
        strPlay = "Play";
        strAbout = "Scores";
        strExit = "Exit";
        showAbout = false;
    }

    public void DisableMenu()
    {
        // DESABILITANDO A VARIAVEL
        menuEnabled = false;
        GetComponent<ScriptPontuacao>().enabled = true;
        GetComponent<ScriptVidas>().enabled = true;
        GetComponent<ScriptNiveis>().enabled = true;
        GetComponent<ScriptNiveis>().AtualizaLevel();
        GetComponent<ScriptSpawn>().enabled = true;
        GetComponent<ScriptEspecial>().enabled = true;
        GetComponent<ScriptSpawn>().Spawn();
        GetComponent<ScriptPause>().BeginGame();
        // PEGANDO O PONTEIRO PARA TODOS OS BOTOES
        var pai = alvo.transform.parent.gameObject;
        alvo.transform.parent = null;
        Destroy(pai); // SOME COM OS BOTOES
        alvo.transform.GetComponent<Collider>().enabled = false;
        // TIRANDO OS ESCRITOS
        strPlay = "";
        strAbout = "";
        strExit = "";
        //ScriptTarget.DESTROY_ALL_TARGETS = false;
    }

    public void PlaySelected(Collider Col)
    {
	    // EXPLODINDO O BOTAO JOGAR
	    alvo = Col;
	    for (var i = 0; i < Col.gameObject.transform.childCount; i++)
	    {
		    // EXPLOSAO DO BOTAO
		    Col.gameObject.transform.GetChild(i).GetComponent<Rigidbody>().AddForce((Col.gameObject.transform.GetChild(i).position - Col.gameObject.transform.position) * Random.Range(150,200));
            Col.gameObject.transform.GetChild(i).GetComponent<ScriptExplosao>().Destroi(tExplosao, 0.5f);
		    Destroy(Col.gameObject.transform.GetChild(i).gameObject, tExplosao);
	    }
	    stylePlay.normal.textColor = Color.red;
	    showAbout = false;
	    Invoke("DisableMenu", tExplosao);
    }

    public void AboutSelected(Collider Col)
    {
	    for (int i = 0; i < Col.gameObject.transform.childCount; i++)
	    {
		    // EXPLOSAO DO BOTAO
		    Col.gameObject.transform.GetChild(i).GetComponent<Rigidbody>().AddForce((Col.gameObject.transform.GetChild(i).position - Col.gameObject.transform.position) * Random.Range(150,200));
            Col.gameObject.transform.GetChild(i).GetComponent<ScriptExplosao>().Destroi(tExplosao, 0.5f);
		    Destroy(Col.gameObject.transform.GetChild(i).gameObject, tExplosao);
	    }
	    Col.gameObject.GetComponent<Collider>().enabled = false;
	    styleAbout.normal.textColor = Color.red;
	    showAbout = true;
    }

    public void ExitSelected(Collider Col)
    {
	    for (int i = 0; i < Col.gameObject.transform.childCount; i += 1)
	    {
		    // EXPLOSAO DO BOTAO
		    Col.gameObject.transform.GetChild(i).GetComponent<Rigidbody>().AddForce((Col.gameObject.transform.GetChild(i).position - Col.gameObject.transform.position) * Random.Range(150,200));
            Col.gameObject.transform.GetChild(i).GetComponent<ScriptExplosao>().Destroi(tExplosao, 0.5f);
		    Destroy(Col.gameObject.transform.GetChild(i).gameObject, tExplosao);
	    }
	    styleExit.normal.textColor = Color.red;
	    Invoke("ExitGame", 1.0f);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(12 * Screen.width / 100, 24 * Screen.height / 100, Screen.width / 10, Screen.height / 30), strPlay, stylePlay);
        GUI.Label(new Rect(39 * Screen.width / 100, 24 * Screen.height / 100, Screen.width / 10, Screen.height / 30), strAbout, styleAbout);
        GUI.Label(new Rect(75 * Screen.width / 100, 24 * Screen.height / 100, Screen.width / 10, Screen.height / 30), strExit, styleExit);
        if (showAbout)
        {
            GUI.Label(new Rect(12 * Screen.width / 100, 35 * Screen.height / 100, Screen.width / 2, Screen.height / 2), textoAbout, styleProducers);
        }
    }

    private void ExitGame()
    {
        Application.Quit();
    }
	
	// Use this for initialization
	void Start () 
	{
		// FONTES DO MENU
		stylePlay.fontSize = Screen.width/13;
		styleAbout.fontSize = Screen.width/13;
		styleExit.fontSize = Screen.width/13;
		styleProducers.fontSize = Screen.width/15;
		styleProducers.normal.textColor = Color.white;
		// HABILITANDO O MENU
		EnableMenu();
		T_EXP_BOTAO = tExplosao;
		FRAMERATE = 0;
		tempo = 0;
		iteracoes = 0;
		// LENDO AS PONTUACOES
		LoadScores();
	}
	
	// Update is called once per frame
	void Update () 
	{
		tempo += Time.deltaTime;
		iteracoes += 1;
		if (tempo >= 1.0)
		{
			FRAMERATE = iteracoes - 1;
			iteracoes = 0;
			if (Time.deltaTime < 1.0)
			{
				tempo = Time.deltaTime;
				iteracoes += 1;
			}
			else
				tempo = 0;
		}
	}
}
