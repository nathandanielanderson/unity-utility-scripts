using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Solana.Unity.SDK;
using Solana.Unity.SDK.Nft;

public class DisplayNftImage : MonoBehaviour
{
    public RawImage uiImage; // Assign this if displaying on UI
    public GameObject plane; // Assign this if displaying on a 3D Plane

void Start()
{
    Debug.Log("DisplayNftImage is attached to: " + gameObject.name);
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
        if (nfts == null || nfts.Count == 0)
        {
            Debug.LogWarning("No NFTs found in the wallet.");
            return;
        }

        // Get the first NFT in the list
        Nft firstNft = nfts[1];

        // Check if the NFT has an image loaded
        if (firstNft.metaplexData?.nftImage?.file != null)
        {
            ApplyNftTexture(firstNft.metaplexData.nftImage.file);
        }
        else
        {
            Debug.LogWarning("NFT image not found.");
        }
    }

    private void ApplyNftTexture(Texture2D texture)
    {
        // Apply to UI RawImage if assigned
        if (uiImage != null)
        {
            uiImage.texture = texture;
            uiImage.color = Color.white; // Set to white to ensure the image displays correctly
            Debug.Log("NFT image applied to UI RawImage.");
        }

        // Apply to 3D plane if assigned
        if (plane != null)
        {
            Renderer renderer = plane.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.mainTexture = texture;
                Debug.Log("NFT image applied to 3D plane.");
            }
        }
    }
}