using System.Collections.Generic;
using UnityEngine;
using Solana.Unity.SDK;
using Solana.Unity.SDK.Nft;

public class LogAvailableNFTs : MonoBehaviour
{
    private void OnEnable()
    {
        // Subscribe to the OnNFTsUpdate event
        Web3.OnNFTsUpdate += HandleNFTsUpdate;
        LoadNFTs();
    }

    private void OnDisable()
    {
        // Unsubscribe from the event when this script is disabled
        Web3.OnNFTsUpdate -= HandleNFTsUpdate;
    }

    private async void LoadNFTs()
    {
        // Initiates loading of NFTs
        await Web3.UpdateNFTs();
    }

    private void HandleNFTsUpdate(List<Nft> nfts, int total)
    {
        if (nfts == null || nfts.Count == 0)
        {
            Debug.LogWarning("No NFTs found in the wallet.");
            return;
        }

        Debug.Log($"Total NFTs found: {nfts.Count}");

        for (int i = 0; i < nfts.Count; i++)
        {
            Nft nft = nfts[i];
            var metadata = nft.metaplexData;


            // Log basic NFT info
            Debug.Log($"NFT {i + 1}:");
            Debug.Log($" - URI: {metadata.nftImage?.file}");

            

        }
    }
}