using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States : MonoBehaviour
{

    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm=GameManager.GetInstance();
        
    }

    // Update is called once per frame
    public void Inicio()
    {


        gm.ChangeState(GameManager.GameState.STORY);
        
    }

    public void Jogo()
    {
        

        gm.ChangeState(GameManager.GameState.GAME);
        
    }
    public void Sair()
    {
        

        gm.ChangeState(GameManager.GameState.MENU);
        
    }

    public void Voltar()
    {
        

        gm.ChangeState(GameManager.GameState.GAME);
        
    }



    


}
