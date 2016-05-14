using UnityEngine;
using System.Collections;

public class ScriptJogador : MonoBehaviour 
{
    public Transform esferaAzulPrefab;
    public Transform esferaVerdePrefab;
    public Transform esferaVermelhoPrefab;
    public Transform spawnAzul;
    public Transform spawnVerde;
    public Transform spawnVermelho;

    private float tempoInicial;
    private float tempoFinal;
    private float angulo;
    private bool azulHab = true;
    private bool amareloHab = true;
    private bool vermelhoHab = true;
    private Vector2 inicioToque;
    private Vector2 fimToque;
    private Vector2 diferenca;    
    private Transform esferaAzul;
    private Transform esferaVerde;
    private Transform esferaVermelho;    
    private Transform esfera;

    // FUNCOES QUE DESABILITAM ALGUM BOTAO
    public void DesabilitaBotaoAzul(float tempo)
    {
        azulHab = false;
        Invoke("HabilitaBotaoAzul", tempo);
    }

    public void DesabilitaBotaoAmarelo(float tempo)
    {
        amareloHab = false;
        Invoke("HabilitaBotaoAmarelo", tempo);
    }

    public void DesabilitaBotaoVermelho(float tempo)
    {
        vermelhoHab = false;
        Invoke("HabilitaBotaoVermelho", tempo);
    }

    // FUNCOES QUE IRAO HABILITAR UM BOTAO NOVAMENTE
    private void HabilitaBotaoAzul()
    {
        azulHab = true;
    }

    private void HabilitaBotaoAmarelo()
    {
        amareloHab = true;
    }

    private void HabilitaBotaoVermelho()
    {
        vermelhoHab = true;
    }

	// Update is called once per frame
	void Update () 
    {
	    if (Input.GetMouseButtonDown(0))
	{
		inicioToque = Input.mousePosition;
		tempoInicial = Time.time;
		//print ("Inicio: "+inicioToque.x+", "+inicioToque.y);
		// VERIFICA SE CLICOU NO PAUSE
		if (inicioToque.y >= Screen.height - 0.13*Screen.height)
			GetComponent<ScriptPause>().Pause();
	}
	if (Input.GetMouseButtonUp(0))
	{
		if (inicioToque.y <= Screen.height/6)
		{
			fimToque = Input.mousePosition;
			tempoFinal = Time.time;
			//print ("Fim: "+fimToque.x+", "+fimToque.y);
			
			diferenca = fimToque - inicioToque;
			if (diferenca.y != 0 || diferenca.x != 0)
			{
				if (diferenca.x == 0)
					angulo = -90;
				else
				{
					angulo = Mathf.Atan(diferenca.y/diferenca.x);
					angulo = (180*angulo)/Mathf.PI;
					if (angulo < 20 && angulo >= 0) angulo = 20;
					else if (angulo > -20 && angulo <= 0) angulo = -20;
				}
				float distancia = Mathf.Sqrt(Mathf.Pow(diferenca.x,2) + Mathf.Pow(diferenca.y,2));
				float tempo = tempoFinal - tempoInicial;
				if (distancia < 40)
				{
					inicioToque = new Vector2(Screen.width, Screen.height);
					return;
				}
				if (distancia > 1500) distancia = 1500;
				if (tempo < 0.08) tempo = 0.08f;
				
				int forca = (int)(distancia / (tempo + 0.3f));
				if (forca < 600) forca = 600;
				else if (forca > 2100) forca = 2100;
				// Criacao da bola
				// Se angulo for positivo
				if (angulo >= 0) transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
				// Se angulo for negativo
				else if (angulo <= 0) transform.rotation = Quaternion.AngleAxis(180+angulo, Vector3.forward);
				if (inicioToque.x < Screen.width/3 && azulHab) // BOTAO AZUL
				{
					//print ("Azul");
					esferaAzul = Instantiate(esferaAzulPrefab, spawnAzul.position, Quaternion.identity) as Transform;
					esferaAzul.GetComponent<Rigidbody>().AddForce(transform.right * forca);
				}
				else if (inicioToque.x < 2*Screen.width/3 && amareloHab) // BOTAO AMARELO
				{
					//print ("Verde");
                    esferaVerde = Instantiate(esferaVerdePrefab, spawnVerde.position, Quaternion.identity) as Transform;
					esferaVerde.GetComponent<Rigidbody>().AddForce(transform.right * forca);
				}
				else if (vermelhoHab)// BOTAO VERMELHO
				{
					//print ("Vermelho");
                    esferaVermelho = Instantiate(esferaVermelhoPrefab, spawnVermelho.position, Quaternion.identity) as Transform;
					esferaVermelho.GetComponent<Rigidbody>().AddForce(transform.right * forca);
				}
				inicioToque = new Vector2(Screen.width, Screen.height);
			}
		}
	}
	}
}
