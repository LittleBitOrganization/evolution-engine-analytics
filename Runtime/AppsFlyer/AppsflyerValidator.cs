using System;
using UnityEngine;

namespace AppsFlyerConnector
{
    public class AppsflyerValidator : MonoBehaviour, IAppsFlyerPurchaseValidation
    {
        public event Action<string> OnDidApplyAnimationProperties;
        public event Action<string> OnDidReceivePurchaseRevenueError;

        public void didReceivePurchaseRevenueValidationInfo(string validationInfo)
        {
            OnDidApplyAnimationProperties?.Invoke(validationInfo);
        }

        public void didReceivePurchaseRevenueError(string error)
        {
            OnDidReceivePurchaseRevenueError?.Invoke(error);
        }
    }
}