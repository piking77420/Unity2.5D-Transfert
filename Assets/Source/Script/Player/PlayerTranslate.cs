using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTranslate : TranSlate
{
    // Start is called before the first frame update


    public new void BecommingGhosted() 
    {
        gameObject.TryGetComponent<Renderer>(out Renderer renderer);
        gameObject.TryGetComponent<Collider>(out Collider collider);
        gameObject.TryGetComponent<PlayerJump>(out PlayerJump playerJump);
        gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement movementPlayer);

        movementPlayer.enabled = !movementPlayer.enabled;
        playerJump.enabled = !playerJump.enabled;
        collider.enabled = !collider.enabled;
        renderer.enabled = !collider.enabled;

            


    }



    private new void Awake()
    {   
        base.Awake();


    }
 



    new void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

      

    }
}
