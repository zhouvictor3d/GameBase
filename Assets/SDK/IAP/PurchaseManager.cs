using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class PurchaseManager : MonoBehaviour, IStoreListener
{
    public BuyManager buyManager;
    private IStoreController controller;

    public void Instance()
    {
        var module = StandardPurchasingModule.Instance();
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);
        builder.AddProduct("id0", ProductType.NonConsumable);
        builder.AddProduct("id1", ProductType.Subscription);
        builder.AddProduct("Id2", ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
    }

   

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
    }
    public void OnInitializeFailed(InitializationFailureReason error)
    {

    }
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {

    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        return PurchaseProcessingResult.Complete;
    }
    public void OnPurchaseClicked(string productId)
    {
        controller.InitiatePurchase(productId);
    }
}
