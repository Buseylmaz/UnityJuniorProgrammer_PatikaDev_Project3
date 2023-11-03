using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRb;
    Animator playerAnim;

    public float jumpForce = 10;
    public float gravityModifier ;

    //Efekler için
    public ParticleSystem explosionParticle;
    public ParticleSystem walkParticle;

    //spaces tuþuna bir den kez basýlsa bile sadece bir kez zýplamasý için sebep de oyuncunun uzaya dogru uçmasýný engellemek için bir baþka yolu da görünmez bir obje eklemek veya oyuncunun zýplayýþýný sýnýrlandýrmaktda olabilir.
    public bool isOnGround = true;
    public bool gameOver;

    //oyuncunun havadayken 2 kez zýplamasýný istiyoruz
    public bool doubleJump = false;
    int countJump ;

    //Ses için
    public AudioClip jumpClip;
    public AudioClip crashClip;
    AudioSource playSource;

    //skor
    public int score = 0;

    //Panel
    public GameObject panel;
    public Text scoreText;


    

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playSource = GetComponent<AudioSource>();

        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

       
    }

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)  && isOnGround && !gameOver  )//space basmýþ isen oyuncu yerde ise ve ölmemiþse gerçekleþtir
        {

            //Impulse istenilen hýza anýnda ulaþmasý ve daha hýzlý harekt etmesi için
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;

            
            
            //zýblarken jump_trig animasyonu çalýþmasý için//tetikleyici ayarlama
            playerAnim.SetTrigger("Jump_trig");
            walkParticle.Stop();
            playSource.PlayOneShot(jumpClip, 2f);
            
        }
        
        //Çift zýplamaß
        if(isOnGround==false && doubleJump==false && Input.GetKeyDown(KeyCode.Space))
        {
            if (countJump<=1)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                countJump += 1;

            }
            
            
        }
        
        
    }


    //Çarpýþtýrýcýlarý kullanarak oyunucnun ne zamna yerde ne zaman yerde olmadýgýný biliyoruz.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            doubleJump = false;
            countJump = 0;
            walkParticle.Play();

        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            //Debug.Log("Game over");

            
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            walkParticle.Stop();
            playSource.PlayOneShot(crashClip, 2f);
            //SetBool setýnteger gibi özellikleri seçmemiz animator ekranýnda istedgimiz degerleri bool int gibi degerler vermemizden kaynaklanýyor.

            Score();


            //Sahneyi yeniden yüklemesi için 
            Scene aktifSahne = SceneManager.GetActiveScene();
            SceneManager.LoadScene(aktifSahne.name);
        }

        else if (collision.gameObject.CompareTag("Win")) //Skor kazanma
        {
            score += 1;
            
        }
        


    }

    void Score()
    {
        if (score <= 0)
        {
            score = 0;
            Debug.Log(score);
            scoreText.text = score.ToString();

        }
        else if (score > 0)
        {
            Debug.Log(score);
            score -= 1;
            scoreText.text = score.ToString();
        }


        panel.active = true;

    }

   


}
