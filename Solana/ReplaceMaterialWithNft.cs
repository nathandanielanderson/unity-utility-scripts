using System.Collections.Generic;
using UnityEngine;
using Solana.Unity.SDK;
using Solana.Unity.SDK.Nft;

public class ReplaceMaterialWithNft : MonoBehaviour
{
    public int nftIndex = 1; // Specify the index of the NFT to use
    public int materialIndex = 0; // Specify the material index on the Body object

    void Start()
    {
        Debug.Log("ReplaceMaterialWithNft script is attached to: " + gameObject.name);
    }

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
        Debug.Log($"Received {nfts.Count} NFTs from the wallet.");
        if (nfts == null || nfts.Count == 0)
        {
            Debug.LogWarning("No NFTs found in the wallet.");
            return;
        }

        // Ensure the specified NFT index is within bounds
        if (nftIndex < 0 || nftIndex >= nfts.Count)
        {
            Debug.LogWarning($"NFT index {nftIndex} is out of range. Wallet contains {nfts.Count} NFTs.");
            return;
        }

        // Get the specified NFT from the list
        Nft selectedNft = nfts[nftIndex];

        // Check if the NFT has an image loaded
        if (selectedNft.metaplexData?.nftImage?.file != null)
        {
            ApplyNftTextureToMaterial(selectedNft.metaplexData.nftImage.file);
        }
        else
        {
            Debug.LogWarning("NFT image not found for the selected NFT.");
        }
    }

    private void ApplyNftTextureToMaterial(Texture2D texture)
    {
        // Apply the texture to the specified material slot on the Body object
        Renderer bodyRenderer = GetComponent<Renderer>();
        if (bodyRenderer != null)
        {
            if (materialIndex >= 0 && materialIndex < bodyRenderer.materials.Length)
            {
                bodyRenderer.materials[materialIndex].mainTexture = texture;
                Debug.Log($"NFT image applied to material slot {materialIndex} on Body.");
            }
            else
            {
                Debug.LogWarning($"Material index {materialIndex} is out of range. This object has {bodyRenderer.materials.Length} materials.");
            }
        }
        else
        {
            Debug.LogWarning("Renderer component not found on this GameObject.");
        }
    }
}