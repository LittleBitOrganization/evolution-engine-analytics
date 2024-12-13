using System;
using System.Collections;
using System.Threading.Tasks;
using LittleBitGames.Environment.Purchase;
using UnityEngine;

namespace AppsFlyerConnector
{
    public class AppsflyerValidator : MonoBehaviour, IAppsFlyerPurchaseValidation, IPurchaseValidator
    {
        public event Action<string> OnSuccessValidate;
        public event Action<string> OnFailureValidate;

        private TaskCompletionSource<bool> _taskCompletionSource = new TaskCompletionSource<bool>();

        private float _timeoutError = 20;
        public void Init(float timeoutError)
        {
            _timeoutError = timeoutError;
        }

        private Coroutine _coroutine;
        public async Task<bool> ValidateAsync()
        {
            _coroutine = StartCoroutine(StartTimeoutError());
            
            _taskCompletionSource.SetCanceled();
            try
            {
                var result = await _taskCompletionSource.Task;
                StopCoroutine(_coroutine);
                _coroutine = null;
                _taskCompletionSource = null;
                return result;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private IEnumerator StartTimeoutError()
        {
            yield return new WaitForSecondsRealtime(_timeoutError);
            _taskCompletionSource.SetResult(false);
        }

        public void Reset()
        {
            _taskCompletionSource?.SetCanceled();
            _taskCompletionSource = new TaskCompletionSource<bool>();
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
        
        public void didReceivePurchaseRevenueValidationInfo(string validationInfo)
        {
            _taskCompletionSource.SetResult(true);
            OnSuccessValidate?.Invoke(validationInfo);
        }

        public void didReceivePurchaseRevenueError(string error)
        {
            _taskCompletionSource.SetResult(false);
            OnFailureValidate?.Invoke(error);
        }

        private void OnDestroy()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
    }
}