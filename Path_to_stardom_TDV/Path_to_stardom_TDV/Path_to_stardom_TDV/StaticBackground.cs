using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Path_to_stardom_TDV
{
    public class StaticBackground
    {
        private List<Texture2D> _layers = new List<Texture2D>();
        private float _scale = 1.1f; // Ajuste conforme necessário
        private int _screenWidth;
        private int _verticalOffset;

        public StaticBackground(int screenWidth, int verticalOffset = 0)
        {
            _screenWidth = screenWidth;
            _verticalOffset = verticalOffset;
        }

        public void LoadContent(ContentManager content)
        {
            // Carrega as 9 layers em ordem (da mais distante para a mais próxima)
            _layers.Add(content.Load<Texture2D>("Background1/Layer_0011_0"));
            _layers.Add(content.Load<Texture2D>("Background1/Layer_0010_1"));
            _layers.Add(content.Load<Texture2D>("Background1/Layer_0009_2"));
            _layers.Add(content.Load<Texture2D>("Background1/Layer_0008_3"));
            _layers.Add(content.Load<Texture2D>("Background1/Layer_0007_Lights"));
            _layers.Add(content.Load<Texture2D>("Background1/Layer_0006_4"));
            _layers.Add(content.Load<Texture2D>("Background1/Layer_0005_5"));
            _layers.Add(content.Load<Texture2D>("Background1/Layer_0004_Lights"));
            _layers.Add(content.Load<Texture2D>("Background1/Layer_0003_6"));
            _layers.Add(content.Load<Texture2D>("Background1/Layer_0002_7"));
            _layers.Add(content.Load<Texture2D>("Background1/Layer_0001_8"));
            _layers.Add(content.Load<Texture2D>("Background1/Layer_0000_9"));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 cameraPosition)
        {
            // Desenha todas as layers em ordem (a primeira da lista é a mais distante)
            for (int i = 0; i < _layers.Count; i++)
            {
                var layer = _layers[i];
                float depth = 0.06f + (0.8f - (i * (0.8f / 11)));

                int textureWidth = (int)(layer.Width * _scale);
                int startTile = (int)(cameraPosition.X / textureWidth);

                // Desenha 3 cópias do background (esquerda, centro, direita)
                for (int j = startTile - 2; j <= startTile + 2; j++)
                {
                    Vector2 position = new Vector2(j * textureWidth, _verticalOffset);
                    spriteBatch.Draw(
                        layer,
                        position,
                        null,
                        Color.White,
                        0f,
                        Vector2.Zero,
                        _scale,
                        SpriteEffects.None,
                        depth // Importante para ordem de renderização
                    );
                }
            }
        }

        public float GetLayerWidth()
        {
            // Assume que todas as layers têm a mesma largura
            if (_layers.Count > 0)
            {
                return _layers[0].Width * _scale;
            }
            return 1280f * _scale; // fallback
        }


        // Calcula a profundidade baseada na ordem da layer (0 a 1)
        private float GetLayerDepth(int layerIndex)
        {
            return 0.1f + (layerIndex * 0.07f); // Ajuste os valores conforme necessário
        }
    }
}
