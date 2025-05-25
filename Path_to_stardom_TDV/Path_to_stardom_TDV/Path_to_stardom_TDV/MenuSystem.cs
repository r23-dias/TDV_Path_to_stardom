using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Path_to_stardom_TDV
{
    public class MainMenu
    {
        private Texture2D _backgroundTexture;
        private BitmapFont _font;
        private int _selectedOption = 0;
        private string[] _menuOptions = { "NOVO JOGO", "CREDITOS", "SAIR" };
        private Color _selectedColor = Color.Yellow;
        private Color _normalColor = Color.White;
        private Vector2 _titlePosition;
        private Vector2[] _optionPositions;

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _font = new BitmapFont(graphicsDevice);
            _backgroundTexture = CreateMenuBackground(graphicsDevice);

            _titlePosition = new Vector2(-120, 150);
            _optionPositions = new Vector2[_menuOptions.Length];

            for (int i = 0; i < _menuOptions.Length; i++)
            {
                _optionPositions[i] = new Vector2(-100, 300 + i * 80);
            }
        }

        private Texture2D CreateMenuBackground(GraphicsDevice graphicsDevice)
        {
            Texture2D texture = new Texture2D(graphicsDevice, 1280, 720);
            Color[] colorData = new Color[1280 * 720];

            for (int y = 0; y < 720; y++)
            {
                for (int x = 0; x < 1280; x++)
                {
                    float gradient = (float)y / 720f;
                    Color topColor = new Color(10, 10, 30);
                    Color bottomColor = new Color(50, 50, 100);
                    colorData[y * 1280 + x] = Color.Lerp(topColor, bottomColor, gradient);
                }
            }

            texture.SetData(colorData);
            return texture;
        }

        public int Update(KeyboardState currentKeyboard, KeyboardState previousKeyboard)
        {
            if (currentKeyboard.IsKeyDown(Keys.Down) && !previousKeyboard.IsKeyDown(Keys.Down))
            {
                _selectedOption = (_selectedOption + 1) % _menuOptions.Length;
            }
            else if (currentKeyboard.IsKeyDown(Keys.Up) && !previousKeyboard.IsKeyDown(Keys.Up))
            {
                _selectedOption = (_selectedOption - 1 + _menuOptions.Length) % _menuOptions.Length;
            }

            if (currentKeyboard.IsKeyDown(Keys.Enter) && !previousKeyboard.IsKeyDown(Keys.Enter))
            {
                return _selectedOption;
            }

            return -1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 bgPosition = new Vector2(-640, 0);
            spriteBatch.Draw(_backgroundTexture, bgPosition, Color.White);

            // Desenhar título
            _font.DrawText(spriteBatch, "PATH TO STARDOM", _titlePosition, Color.Gold, 3f);

            // Desenhar opções do menu
            for (int i = 0; i < _menuOptions.Length; i++)
            {
                Color color = (i == _selectedOption) ? _selectedColor : _normalColor;
                _font.DrawText(spriteBatch, _menuOptions[i], _optionPositions[i], color, 2f);

                // Desenhar indicador de seleção
                if (i == _selectedOption)
                {
                    Vector2 indicatorPos = new Vector2(_optionPositions[i].X - 30, _optionPositions[i].Y);
                    _font.DrawText(spriteBatch, ">", indicatorPos, _selectedColor, 2f);
                }
            }
        }
    }

    public class CreditsScreen
    {
        private Texture2D _backgroundTexture;
        private BitmapFont _font;
        private string[] _creditsText = {
            "PATH TO STARDOM",
            "",
            "DESENVOLVIDO POR:",
            "SEU NOME AQUI",
            "",
            "FEITO COM MONOGAME",
            "",
            "AGRADECIMENTOS ESPECIAIS:",
            "COMUNIDADE MONOGAME",
            "",
            "",
            "PRESSIONE ESC PARA VOLTAR"
        };

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _font = new BitmapFont(graphicsDevice);
            _backgroundTexture = CreateCreditsBackground(graphicsDevice);
        }

        private Texture2D CreateCreditsBackground(GraphicsDevice graphicsDevice)
        {
            Texture2D texture = new Texture2D(graphicsDevice, 1280, 720);
            Color[] colorData = new Color[1280 * 720];

            for (int y = 0; y < 720; y++)
            {
                for (int x = 0; x < 1280; x++)
                {
                    colorData[y * 1280 + x] = new Color(20, 20, 40);
                }
            }

            texture.SetData(colorData);
            return texture;
        }

        public bool Update(KeyboardState currentKeyboard, KeyboardState previousKeyboard)
        {
            return currentKeyboard.IsKeyDown(Keys.Escape) && !previousKeyboard.IsKeyDown(Keys.Escape);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, 1280, 720), Color.White);

            for (int i = 0; i < _creditsText.Length; i++)
            {
                if (!string.IsNullOrEmpty(_creditsText[i]))
                {
                    Vector2 position = new Vector2(400, 100 + i * 40);
                    Color color = (i == 0) ? Color.Gold : Color.White;
                    float scale = (i == 0) ? 2.5f : 1.5f;
                    _font.DrawText(spriteBatch, _creditsText[i], position, color, scale);
                }
            }
        }
    }
}

