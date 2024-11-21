using System;
using System.Text;
using System.Threading.Tasks;
using Solana.Unity.Rpc;
using Solana.Unity.Rpc.Builders;
using Solana.Unity.Rpc.Models;
using Solana.Unity.Rpc.Types;
using Solana.Unity.Wallet;
using Solana.Unity.SDK;
using UnityEngine;

public class SendLogMessageExample : MonoBehaviour
{
    private const string ProgramId = "2r9AvbDhiTq9dcAojVmJgnxZX3AH7nsycL6WXtPPqNW4"; // Replace with your actual program ID

    private async void Start()
    {
        // Ensure Web3.Wallet is connected
        if (Web3.Wallet == null || Web3.Wallet.Account == null)
        {
            Debug.LogError("Wallet not connected. Please connect your wallet.");
            return;
        }

        // Initialize the RPC client for Devnet
        var rpcClient = ClientFactory.GetClient(Cluster.DevNet);

        // Get the source account from the connected wallet
        var fromAccount = Web3.Wallet.Account;

        // Get a recent block hash to include in the transaction
        var blockHash = await rpcClient.GetLatestBlockHashAsync(Commitment.Confirmed);
        if (!blockHash.WasSuccessful || string.IsNullOrEmpty(blockHash.Result.Value.Blockhash))
        {
            Debug.LogError("Failed to retrieve recent block hash.");
            return;
        }

        // Create instruction data for `log_message`
        var instructionData = BuildLogMessageData("Hello from Drop Party!");

        // Create transaction instruction to call `log_message` in your program
        var logMessageInstruction = new TransactionInstruction
        {
            ProgramId = new PublicKey(ProgramId),
            Keys = new System.Collections.Generic.List<AccountMeta>
            {
                AccountMeta.Writable(fromAccount.PublicKey, isSigner: true)
            },
            Data = instructionData
        };

        // Build the transaction
        var tx = new TransactionBuilder()
            .SetRecentBlockHash(blockHash.Result.Value.Blockhash)
            .SetFeePayer(fromAccount)
            .AddInstruction(logMessageInstruction)
            .Build(fromAccount);

        // Send the transaction
        var txResult = await rpcClient.SendTransactionAsync(tx, true, Commitment.Confirmed);

        if (txResult.WasSuccessful)
        {
            Debug.Log($"log_message sent successfully with transaction signature: {txResult.Result}");
        }
        else
        {
            Debug.LogError($"Transaction failed with reason: {txResult.Reason}");
        }
    }

    private byte[] BuildLogMessageData(string message)
    {
        // Instruction data construction for `log_message`
        // Typically, the first byte is the function identifier (e.g., 0 for `log_message`)
        var data = new System.Collections.Generic.List<byte> { 0 };

        // Encode the message as UTF-8 and append it to the instruction data
        data.AddRange(Encoding.UTF8.GetBytes(message));

        return data.ToArray();
    }
}