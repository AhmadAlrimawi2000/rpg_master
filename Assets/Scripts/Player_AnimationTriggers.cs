using UnityEngine;

public class Player_AnimationTriggers : MonoBehaviour
{

    private Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void CurrentStateTrigger()
    {
        //@ Get access to the player and let the current player's state know that we want to exit the state 
        player.CallAnimationTrigger();

    }


}
