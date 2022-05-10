using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   float _baseSpeed = 10.0f;

   CharacterController characterController;
   //Referência usada para a câmera filha do jogador
   GameObject playerCamera;

   //Utilizada para poder travar a rotação no angulo que quisermos.
   float cameraRotation;

   GameManager gm;

   private Animator animator;
   
   void Start()
   {
       characterController = GetComponent<CharacterController>();
       playerCamera = GameObject.Find("Main Camera");
       cameraRotation = 0.0f;

       gm = GameManager.GetInstance();

       animator = GetComponent<Animator>();
   }

   void Update()
   {

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


   private void HandleAnimation(){

       print(characterController.velocity.x);
       print(characterController.velocity.z);
   
        if((characterController.velocity.x != 0) || (characterController.velocity.z != 0)){
            animator.SetBool("walking", true);
        }else{
            animator.SetBool("walking", false);
        }
    }
   

}