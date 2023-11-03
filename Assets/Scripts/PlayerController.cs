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

    //Efekler i�in
    public ParticleSystem explosionParticle;
    public ParticleSystem walkParticle;

    //spaces tu�una bir den kez bas�lsa bile sadece bir kez z�plamas� i�in sebep de oyuncunun uzaya dogru u�mas�n� engellemek i�in bir ba�ka yolu da g�r�nmez bir obje eklemek veya oyuncunun z�play���n� s�n�rland�rmaktda olabilir.
    public bool isOnGround = true;
    public bool gameOver;

    //oyuncunun havadayken 2 kez z�plamas�n� istiyoruz
    public bool doubleJump = false;
    int countJump ;

    //Ses i�in
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

        if (Input.GetKeyDown(KeyCode.Space)  && isOnGround && !gameOver  )//space basm�� isen oyuncu yerde ise ve �lmemi�se ger�ekle�tir
        {

            //Impulse istenilen h�za an�nda ula�mas� ve daha h�zl� harekt etmesi i�in
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;

            
            
            //z�blarken jump_trig animasyonu �al��mas� i�in//tetikleyici ayarlama
            playerAnim.SetTrigger("Jump_trig");
            walkParticle.Stop();
            playSource.PlayOneShot(jumpClip, 2f);
            
        }
        
        //�ift z�plama�
        if(isOnGround==false && doubleJump==false && Input.GetKeyDown(KeyCode.Space))
        {
            if (countJump<=1)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                countJump += 1;

            }
            
            
        }
        
        
    }


    //�arp��t�r�c�lar� kullanarak oyunucnun ne zamna yerde ne zaman yerde olmad�g�n� biliyoruz.
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
            //SetBool set�nteger gibi �zellikleri se�memiz animator ekran�nda istedgimiz degerleri bool int gibi degerler vermemizden kaynaklan�yor.

            Score();


            //Sahneyi yeniden y�klemesi i�in 
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
