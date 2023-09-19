using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {
    
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // no kitchen object
            if (player.HasKitchenObject()) {
                // player grabbed object
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else {
                // player has nothing to carry
            }
        }
        else {
            // there is a kitchen object
            if (player.HasKitchenObject()) {
                // player carrying something
            }
            else {
                // player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    
}