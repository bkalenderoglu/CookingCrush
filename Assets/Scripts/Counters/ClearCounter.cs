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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else {
                    // player is not carrying plate but something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        // counter has a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else {
                // player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}