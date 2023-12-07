using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EarthDenfender
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] private Material starBig;
        [SerializeField] private Material starMed;
        [SerializeField] private Material nebula;
        [SerializeField] private float starBigScrollSpeed;
        [SerializeField] private float starSmallScrollSpeed;
        [SerializeField] private float nebulaScrollSpeed;

        private int mainTextId;

        private void Start()
        {
            mainTextId = Shader.PropertyToID("_MainTex");
        }

        private void Update()
        {
            Vector2 offSet = starBig.GetTextureOffset(mainTextId);
            offSet += new Vector2(0, starBigScrollSpeed * Time.deltaTime);
            starBig.SetTextureOffset(mainTextId, offSet);

            offSet = starMed.GetTextureOffset(mainTextId);
            offSet += new Vector2(0, starSmallScrollSpeed * Time.deltaTime);
            starMed.SetTextureOffset(mainTextId, offSet);


            offSet = nebula.GetTextureOffset(mainTextId);
            offSet += new Vector2(0, nebulaScrollSpeed * Time.deltaTime);
            nebula.SetTextureOffset(mainTextId, offSet);
        }
    }
}