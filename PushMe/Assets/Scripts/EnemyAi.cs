using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class EnemyAi : MonoBehaviour
{

 public float MoveSpeed;
 public static GameObject closest  ;
 public Rigidbody rb;
 public float force ;
    void Start()
    {
        closest = gameObject ; 
        force = Random.Range(5f, 15f) ;
        MoveSpeed = Random.Range(2f , 5f) ;
        // InvokeRepeating("FindNearest", 0f, 0.5f);
        FindNearest();
        rb = GetComponent<Rigidbody>();
     
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerMovement._ispaused == false )
        {
            var step = MoveSpeed * Time.deltaTime;
            transform.LookAt(closest.transform.position);
            transform.position = Vector3.MoveTowards(transform.position , closest.transform.position,step ) ; ///+= transform.forward*MoveSpeed*Time.deltaTime;

        }
       
        
    
    }
     void OnCollisionStay(Collision other) 
        
     {
        
        //var direction = (closest.transform.position - this.transform.position).normalized;
          //  rb.AddForce(direction * force, ForceMode.Impulse);
          // rb.AddForce((transform.forward * -1) * force);

           // If the object we hit is the enemy
           if(other.gameObject.tag =="Target" && PlayerMovement._ispaused == false)
           {
            // Calculate Angle Between the collision point and the player
         Vector3 dir = closest.transform.position - transform.position;
         // We then get the opposite (-Vector3) and normalize it
         dir = -dir.normalized;
         // And finally we add force in the direction of dir and multiply it by force. 
         // This will push back the player
         GetComponent<Rigidbody>().AddForce(dir*force);

           }
     
    
    }
   void OnCollisionEnter(Collision ot) 
   {
        if(ot.gameObject.tag == "zeropoint")
        {
            FindNearest();
            this.gameObject.SetActive(false);
        }
    }
  
    public void FindNearest()
    {  
        
      
       closest = GameObject.FindGameObjectsWithTag("Target").Where(d=> d.gameObject != gameObject).Aggregate((prev, next) =>
       Vector3.Distance(prev.transform.position, transform.position) > Vector3.Distance(next.transform.position, transform.position) ? prev: next);
            Debug.Log("benim adım  : "+this.gameObject + " hedef bu : "+ closest);
 

    }



}