using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour {
    
    public static DeliveryManager Instance { get; private set; }
    
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    private void Awake() {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipesMax) {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
            }
            
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        for (int i = 0; i < waitingRecipeSOList.Count; i++) {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                // has the same amount of ingredients
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
                    // loop through all ingredients in the recipe
                    bool ingredientMatched = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                        // loop through all ingredients in the plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO) {
                            // ingredient matches
                            ingredientMatched = true;
                            break;
                        }
                    }

                    if (!ingredientMatched) {
                        // this recipe ingredient was not found on the plate
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe) {
                    // player delivered the correct recipe
                    Debug.Log("Correct");
                    waitingRecipeSOList.RemoveAt(i);
                    // stop recipe exec.
                    return;
                }
            }
        }
        // no matches found
        Debug.Log("false");
    }
    
}