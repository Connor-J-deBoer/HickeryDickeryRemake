// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;

namespace HickeryDickery.Characters
{
    public class MaterialSpriteSheetAnimator : MonoBehaviour
    {
        [SerializeField] private Texture2D _spriteSheet;
        [SerializeField] private int _textureWidth = 32;
        [SerializeField] private int _defaultIndex = 0;
        [HideInInspector] [SerializeField] private Renderer _renderer;
        private Texture[] _textures;
        void OnValidate()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void Awake()
        {
            int width = _spriteSheet.width;
            int cellCount = Mathf.RoundToInt(width / Mathf.Max(_textureWidth, Mathf.Epsilon));
            _textures = new Texture[cellCount];
            
            for (int i = 0; i < cellCount; ++i)
            {
                Color[] pixels = _spriteSheet.GetPixels(i * _textureWidth, 0, _textureWidth, _spriteSheet.height);
                Texture2D cell = new (_textureWidth, _spriteSheet.height, _spriteSheet.format, false);
                cell.SetPixels(pixels);
                cell.Apply();
                _textures[i] = cell;
            }
            _renderer.sharedMaterial.SetTexture("_BaseMap", _textures[_defaultIndex]);
        }
        public void SetFace(int index)
        {
            _renderer.material.SetTexture("_BaseMap", _textures[index]);
        }
    }
}