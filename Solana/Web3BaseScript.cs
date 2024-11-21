using UnityEngine;
using Solana.Unity.SDK;
using Solana.Unity.Rpc;
using Solana.Unity.Wallet;
using System.Threading.Tasks;

namespace Solana.Unity.Scripts
{
    public class Web3BaseScript : MonoBehaviour
    {
        // Protected members to be accessed by derived classes
        protected IRpcClient rpc;
        protected WalletBase wallet;

        protected virtual void Awake()
        {
            // Initialize rpc and wallet from Web3 instance
            rpc = Web3.Rpc;
            wallet = Web3.Wallet;

            if (wallet == null)
            {
                Debug.LogError("Wallet is not connected. Please connect a wallet.");
            }
        }

        protected async Task CheckBalance()
        {
            if (wallet?.Account == null)
            {
                Debug.LogError("Wallet is not connected or has no account.");
                return;
            }

            // Fetch and display wallet balance
            var balanceResult = await rpc.GetBalanceAsync(wallet.Account.PublicKey);
            if (balanceResult.WasSuccessful)
            {
                Debug.Log($"Wallet balance: {balanceResult.Result.Value} lamports");
            }
            else
            {
                Debug.LogError("Failed to retrieve balance: " + balanceResult.Reason);
            }
        }

        // Additional shared functionality can go here, such as methods for sending transactions, etc.
    }
}