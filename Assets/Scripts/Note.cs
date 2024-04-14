using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] INGREDIENT_TYPE type;
    [SerializeField] TextMeshProUGUI titleTxt;
    [SerializeField] TextMeshProUGUI descTxt;
    [SerializeField] string title;
    [SerializeField] string desc;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;

        titleTxt.text = title == null || title.Equals("") ? type.ToString() : title ;
        descTxt.text = desc == null || desc.Equals("") ? " Sed dictum interdum tincidunt. Sed dui felis, malesuada eleifend pellentesque et, viverra sed libero. In ornare, diam eu pulvinar pretium, arcu orci ornare lorem, non eleifend ante enim a diam. Praesent gravida condimentum mi et euismod. Nulla venenatis eget elit id vestibulum. Vivamus molestie quam ac risus tincidunt suscipit. Duis dictum fringilla urna at egestas. Ut efficitur lorem id cursus pharetra. Vivamus euismod in metus vel pellentesque. Interdum et malesuada fames ac ante ipsum primis in faucibus. Praesent eu bibendum sapien. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse potenti. Vivamus ultrices vel dui non laoreet. Maecenas rutrum erat vel urna dignissim, sed malesuada ipsum vehicula. Maecenas sed erat semper, mollis felis sit amet, commodo dolor.\r\n\r\nMorbi iaculis ipsum tincidunt lorem ullamcorper elementum. Donec justo mauris, finibus sit amet mollis vel, tincidunt ac justo. Vivamus dignissim mauris nisi, et vehicula est dignissim sed. In at laoreet quam. Aenean rutrum justo id arcu aliquet, eget accumsan nunc sollicitudin. Quisque id nunc finibus, malesuada quam sit amet, semper nisi. Phasellus sollicitudin odio eu tellus porttitor suscipit. Sed laoreet interdum ipsum, et iaculis erat molestie a. Vivamus pretium turpis eu augue sagittis, ac auctor justo sollicitudin. Morbi mollis tempor accumsan. Vivamus volutpat nisl non dui euismod volutpat. Vivamus lorem quam, dictum vitae pellentesque in, ultricies nec magna. Quisque sit amet eros semper, vestibulum orci id, sollicitudin quam. Aenean cursus magna eu augue elementum, ac ullamcorper libero laoreet. Proin non tellus libero. Etiam at nibh tincidunt, tempus libero eu, mollis odio. " : desc;
    }

    public INGREDIENT_TYPE GetIngredientType()
    {
        return type;
    }
}
