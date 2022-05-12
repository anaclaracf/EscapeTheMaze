using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
   float _baseSpeed = 10.0f;

   CharacterController characterController;
   //Referência usada para a câmera filha do jogador
   GameObject playerCamera;

   //Utilizada para poder travar a rotação no angulo que quisermos.
   float cameraRotation;

   GameManager gm;
   private GameObject [] canvas;


   private Animator animator;
   Vector3 originalPos;
   public Text message;
   public AudioClip footstep;

   public AudioSource som_walking;
   
   void Start()
   {
       characterController = GetComponent<CharacterController>();
       playerCamera = GameObject.Find("Main Camera");
    //    plano = GameObject.FindGameObjectWithTag("end");
       cameraRotation = 0.0f;

       gm = GameManager.GetInstance();

       animator = GetComponent<Animator>();
       canvas = GameObject.FindGameObjectsWithTag("canvas");

       originalPos=this.transform.position;
   }

   void Update()
   {
       Perdeu();

       HandleAnimation();

       if(gm.gameState == GameManager.GameState.GAME){

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // print(x);
        // print(z);

            //Tratando movimentação do mouse
        float mouse_dX = Input.GetAxis("Mouse X");
        float mouse_dY = Input.GetAxis("Mouse Y");

        //Tratando a rotação da câmera
        if(Input.GetMouseButton(0)){
            cameraRotation += mouse_dY;
            transform.Rotate(Vector3.up, mouse_dX);
        }

        cameraRotation = Mathf.Clamp(cameraRotation, -75.0f, 75.0f);

        
        Vector3 direction = transform.right * x + transform.forward * z;

        characterController.Move(direction * _baseSpeed * Time.deltaTime);

        
        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0.0f, 0.0f);
       }

       if(Input.GetKeyDown(KeyCode.Escape) && gm.gameState==GameManager.GameState.GAME){
           gm.ChangeState(GameManager.GameState.PAUSE);
       }
       
   }

    private void Reset()
    {
       
        gm.minute = 4;
        gm.seconds = 0;
    }

    private void Perdeu(){

       if ((gm.gameState == GameManager.GameState.ENDGAME || gm.gameState == GameManager.GameState.MENU) && gm.nS == GameManager.GameState.GAME){
            this.transform.position= originalPos;
            Reset();
        }

        if ( gm.nS == GameManager.GameState.MENU){
            this.transform.position= originalPos;
            Reset();
        }
   }

   private void HandleAnimation(){

        if((characterController.velocity.x != 0) || (characterController.velocity.z != 0)){
            animator.SetBool("walking", true);
            if(!som_walking.isPlaying)
                som_walking.PlayOneShot(footstep, 0.2f);
        }else{
            animator.SetBool("walking", false);
            som_walking.Pause();

            

        }
    }


     void OnTriggerEnter(Collider collision)
    {

        print(collision.gameObject.tag=="end");
        if(collision.gameObject.tag=="end" && (gm.minute>=0 || gm.seconds>0)){
            message.text="You finally escaped the Asylum! Enjoy your freedom...";

        }else{
            message.text="Unfortunately the crew caught you, maybe next time...";

        }
        gm.ChangeState(GameManager.GameState.ENDGAME);
    }
   

}