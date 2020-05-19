using UnityEngine;

public class Brick : MonoBehaviour {
    public int hp;
    public event System.Action<Brick> onHit;
    public event System.Action<Brick> onDestroyed;

   private void OnCollisionEnter(Collision collision) {

        var other = collision.GetContact(0).otherCollider;

        var ball = other.GetComponent<Ball>();

        if (ball!=null) {
            this.hp--;
            
            Debug.Log("-1 HP to " + this.gameObject.name);
            //if the hp falls beneat zero, destroy the brick
            if(this.hp <= 0) {
                // we raise the event . . .
                this.onDestroyed?.Invoke(this);
                Destroy(this.gameObject);
            } else {
                //else 
                this.onHit?.Invoke(this);
            }
        }

        // see what the brick is colliding with
        //Debug.Log(collision.GetContact(0).otherCollider.name);
    }
}
