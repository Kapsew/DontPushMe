using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro ;
public class PlayerMovement : MonoBehaviour
{
   public float speed;
   public float force ;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;
    public TextMeshProUGUI _Countdown;
     public TextMeshProUGUI _playerskor;
      public TextMeshProUGUI _enemiesSkor;
    public static bool _ispaused ;
    public int playerscore ;
    public int enemiesScore ;

    float timeLeft ; 
public  void Start() 
{
    speed = 8f ;
    force = 12 ;
    _ispaused = false ;
    timeLeft = 60;
}
    public void FixedUpdate()
    {
        if(_ispaused == false)
        {
            Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
             rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
             /////////
            Counter();
            
        }
 
    }

void OnCollisionEnter(Collision ot) 
   {
        if(ot.gameObject.tag == "Target")
        {
            
         Vector3 dir = EnemyAi.closest.transform.position - transform.position;
         // We then get the opposite (-Vector3) and normalize it
         dir = -dir.normalized;
         // And finally we add force in the direction of dir and multiply it by force. 
         // This will push back the player
         GetComponent<Rigidbody>().AddForce(dir*force);

        }
         if(ot.gameObject.tag == "zeropoint")
        {
            enemiesScore++;
            _enemiesSkor.text = enemiesScore.ToString();
            Application.LoadLevel(Application.loadedLevel);
            this.gameObject.SetActive(false);
        }
    }

   public void Counter()
   {
            timeLeft = timeLeft -  Time.deltaTime;
            _Countdown.text = timeLeft.ToString("0");

            if(timeLeft <= 0)
           {
            _ispaused = true ;
            playerscore ++ ;
            _playerskor.text = _playerskor.ToString();
            //Application.LoadLevel(0);
             Application.LoadLevel(Application.loadedLevel);
           }
   }
   public void SetPause(bool val)
   {
        _ispaused = val ;
   }
     

    
       
    

}
