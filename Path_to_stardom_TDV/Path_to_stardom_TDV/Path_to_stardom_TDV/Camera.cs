using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Path_to_stardom_TDV
{
    public class Camera
    {
        private Viewport _viewport;
        private Vector2 _position;
        private float _zoom = 1.0f;
        private float _minZoom = 0.2f;
        private float _maxZoom = 1.0f;
        private float _smoothness = 0.1f;

        public Vector2 Position => _position;

        public Camera(Viewport viewport, Vector2 initialPosition)
        {
            _viewport = viewport;
            _position = initialPosition;
        }

        public Matrix TransformMatrix =>
            Matrix.CreateTranslation(new Vector3(-_position, 0)) *
            Matrix.CreateScale(_zoom) *
            Matrix.CreateTranslation(new Vector3(_viewport.Width / 2, _viewport.Height / 2, 0));

        public void Update(Vector2 player1Pos, Vector2 player2Pos)
        {
            // Ponto médio entre os dois players (X e Y)
            float midpointX = (player1Pos.X + player2Pos.X) / 2f;
            float midpointY = (player1Pos.Y + player2Pos.Y) / 2f; // Usar o ponto médio em Y também

            // Aplicar limites verticais se necessário
            float minY = 0f; // Limite superior (valores negativos = mais alto)
            float maxY = 350f;  // Limite inferior (valores positivos = mais baixo)

            midpointY = MathHelper.Clamp(midpointY, minY, maxY);

            // Aplicar suavização para ambos os eixos
            _position = Vector2.Lerp(_position, new Vector2(midpointX, midpointY), _smoothness);

            // Distância horizontal e vertical
            float horizontalDistance = Math.Abs(player1Pos.X - player2Pos.X);
            float verticalDistance = Math.Abs(player1Pos.Y - player2Pos.Y);

            float margin = 400f; // margem para não ficar colado na borda

            // Espaço necessário horizontal para mostrar ambos os players + margem
            float requiredWidth = horizontalDistance + margin * 2;

            // Espaço necessário vertical para mostrar ambos os players + margem
            float requiredHeight = verticalDistance + margin * 2;

            // Calcula o zoom necessário para que esse espaço caiba no viewport
            float desiredZoomX = _viewport.Width / requiredWidth;
            float desiredZoomY = _viewport.Height / requiredHeight;

            // Usa o menor zoom para garantir que ambos os players fiquem visíveis
            float desiredZoom = Math.Min(desiredZoomX, desiredZoomY);

            _zoom = MathHelper.Clamp(desiredZoom, _minZoom, _maxZoom);
        }



    }
}
