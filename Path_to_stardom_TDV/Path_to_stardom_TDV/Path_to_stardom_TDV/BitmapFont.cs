using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Path_to_stardom_TDV
{
    public class BitmapFont
    {
        private Texture2D _pixelTexture;
        private Dictionary<char, bool[,]> _fontData;

        public BitmapFont(GraphicsDevice graphicsDevice)
        {
            _pixelTexture = new Texture2D(graphicsDevice, 1, 1);
            _pixelTexture.SetData(new[] { Color.White });
            InitializeFontData();
        }

        private void InitializeFontData()
        {
            _fontData = new Dictionary<char, bool[,]>();

            // Letra A
            _fontData['A'] = new bool[,] {
                {false, true, true, true, false},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, true, true, true, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true}
            };

            // Letra B
            _fontData['B'] = new bool[,] {
                {true, true, true, true, false},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, true, true, true, false},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, true, true, true, false}
            };

            // Letra C
            _fontData['C'] = new bool[,] {
                {false, true, true, true, false},
                {true, false, false, false, true},
                {true, false, false, false, false},
                {true, false, false, false, false},
                {true, false, false, false, false},
                {true, false, false, false, true},
                {false, true, true, true, false}
            };

            // Letra D
            _fontData['D'] = new bool[,] {
                {true, true, true, true, false},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, true, true, true, false}
            };

            // Letra E
            _fontData['E'] = new bool[,] {
                {true, true, true, true, true},
                {true, false, false, false, false},
                {true, false, false, false, false},
                {true, true, true, true, false},
                {true, false, false, false, false},
                {true, false, false, false, false},
                {true, true, true, true, true}
            };

            // Letra F
            _fontData['F'] = new bool[,] {
                {true, true, true, true, true},
                {true, false, false, false, false},
                {true, false, false, false, false},
                {true, true, true, true, false},
                {true, false, false, false, false},
                {true, false, false, false, false},
                {true, false, false, false, false}
            };

            // Letra G
            _fontData['G'] = new bool[,] {
                {false, true, true, true, false},
                {true, false, false, false, true},
                {true, false, false, false, false},
                {true, false, true, true, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {false, true, true, true, false}
            };

            // Letra H
            _fontData['H'] = new bool[,] {
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, true, true, true, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true}
            };

            // Letra I
            _fontData['I'] = new bool[,] {
                {true, true, true, true, true},
                {false, false, true, false, false},
                {false, false, true, false, false},
                {false, false, true, false, false},
                {false, false, true, false, false},
                {false, false, true, false, false},
                {true, true, true, true, true}
            };

            // Letra J
            _fontData['J'] = new bool[,] {
                {true, true, true, true, true},
                {false, false, false, true, false},
                {false, false, false, true, false},
                {false, false, false, true, false},
                {false, false, false, true, false},
                {true, false, false, true, false},
                {false, true, true, false, false}
            };

            // Letra K
            _fontData['K'] = new bool[,] {
                {true, false, false, false, true},
                {true, false, false, true, false},
                {true, false, true, false, false},
                {true, true, false, false, false},
                {true, false, true, false, false},
                {true, false, false, true, false},
                {true, false, false, false, true}
            };

            // Letra L
            _fontData['L'] = new bool[,] {
                {true, false, false, false, false},
                {true, false, false, false, false},
                {true, false, false, false, false},
                {true, false, false, false, false},
                {true, false, false, false, false},
                {true, false, false, false, false},
                {true, true, true, true, true}
            };

            // Letra M
            _fontData['M'] = new bool[,] {
                {true, false, false, false, true},
                {true, true, false, true, true},
                {true, false, true, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true}
            };

            // Letra N
            _fontData['N'] = new bool[,] {
                {true, false, false, false, true},
                {true, true, false, false, true},
                {true, false, true, false, true},
                {true, false, false, true, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true}
            };

            // Letra O
            _fontData['O'] = new bool[,] {
                {false, true, true, true, false},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {false, true, true, true, false}
            };

            // Letra P
            _fontData['P'] = new bool[,] {
                {true, true, true, true, false},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, true, true, true, false},
                {true, false, false, false, false},
                {true, false, false, false, false},
                {true, false, false, false, false}
            };

            // Letra Q
            _fontData['Q'] = new bool[,] {
                {false, true, true, true, false},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, true, false, true},
                {true, false, false, true, false},
                {false, true, true, false, true}
            };

            // Letra R
            _fontData['R'] = new bool[,] {
                {true, true, true, true, false},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, true, true, true, false},
                {true, false, true, false, false},
                {true, false, false, true, false},
                {true, false, false, false, true}
            };

            // Letra S
            _fontData['S'] = new bool[,] {
                {false, true, true, true, false},
                {true, false, false, false, true},
                {true, false, false, false, false},
                {false, true, true, true, false},
                {false, false, false, false, true},
                {true, false, false, false, true},
                {false, true, true, true, false}
            };

            // Letra T
            _fontData['T'] = new bool[,] {
                {true, true, true, true, true},
                {false, false, true, false, false},
                {false, false, true, false, false},
                {false, false, true, false, false},
                {false, false, true, false, false},
                {false, false, true, false, false},
                {false, false, true, false, false}
            };

            // Letra U
            _fontData['U'] = new bool[,] {
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {false, true, true, true, false}
            };

            // Letra V
            _fontData['V'] = new bool[,] {
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {false, true, false, true, false},
                {false, true, false, true, false},
                {false, false, true, false, false}
            };

            // Letra W
            _fontData['W'] = new bool[,] {
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, true, false, true},
                {true, false, true, false, true},
                {true, true, false, true, true},
                {true, false, false, false, true}
            };

            // Letra X
            _fontData['X'] = new bool[,] {
                {true, false, false, false, true},
                {false, true, false, true, false},
                {false, false, true, false, false},
                {false, false, true, false, false},
                {false, false, true, false, false},
                {false, true, false, true, false},
                {true, false, false, false, true}
            };

            // Letra Y
            _fontData['Y'] = new bool[,] {
                {true, false, false, false, true},
                {true, false, false, false, true},
                {false, true, false, true, false},
                {false, false, true, false, false},
                {false, false, true, false, false},
                {false, false, true, false, false},
                {false, false, true, false, false}
            };

            // Letra Z
            _fontData['Z'] = new bool[,] {
                {true, true, true, true, true},
                {false, false, false, true, false},
                {false, false, true, false, false},
                {false, true, false, false, false},
                {true, false, false, false, false},
                {true, false, false, false, false},
                {true, true, true, true, true}
            };

            // Números
            _fontData['0'] = new bool[,] {
                {false, true, true, true, false},
                {true, false, false, false, true},
                {true, false, false, true, true},
                {true, false, true, false, true},
                {true, true, false, false, true},
                {true, false, false, false, true},
                {false, true, true, true, false}
            };

            _fontData['1'] = new bool[,] {
                {false, false, true, false, false},
                {false, true, true, false, false},
                {false, false, true, false, false},
                {false, false, true, false, false},
                {false, false, true, false, false},
                {false, false, true, false, false},
                {false, true, true, true, false}
            };

            _fontData['2'] = new bool[,] {
                {false, true, true, true, false},
                {true, false, false, false, true},
                {false, false, false, false, true},
                {false, false, false, true, false},
                {false, false, true, false, false},
                {false, true, false, false, false},
                {true, true, true, true, true}
            };

            _fontData['3'] = new bool[,] {
                {true, true, true, true, false},
                {false, false, false, false, true},
                {false, false, false, false, true},
                {false, true, true, true, false},
                {false, false, false, false, true},
                {false, false, false, false, true},
                {true, true, true, true, false}
            };

            // Símbolos especiais
            _fontData[' '] = new bool[,] {
                {false, false, false, false, false},
                {false, false, false, false, false},
                {false, false, false, false, false},
                {false, false, false, false, false},
                {false, false, false, false, false},
                {false, false, false, false, false},
                {false, false, false, false, false}
            };

            _fontData['>'] = new bool[,] {
                {true, false, false, false, false},
                {false, true, false, false, false},
                {false, false, true, false, false},
                {false, false, false, true, false},
                {false, false, true, false, false},
                {false, true, false, false, false},
                {true, false, false, false, false}
            };

            _fontData[':'] = new bool[,] {
                {false, false, false, false, false},
                {false, false, true, false, false},
                {false, false, false, false, false},
                {false, false, false, false, false},
                {false, false, false, false, false},
                {false, false, true, false, false},
                {false, false, false, false, false}
            };
        }

        public void DrawText(SpriteBatch spriteBatch, string text, Vector2 position, Color color, float scale = 1f)
        {
            int charWidth = (int)(6 * scale);
            int charHeight = (int)(8 * scale);
            int pixelSize = (int)(1 * scale);

            for (int i = 0; i < text.Length; i++)
            {
                char c = char.ToUpper(text[i]);
                if (_fontData.ContainsKey(c))
                {
                    var charData = _fontData[c];
                    Vector2 charPos = new Vector2(position.X + i * charWidth, position.Y);

                    for (int y = 0; y < charData.GetLength(0); y++)
                    {
                        for (int x = 0; x < charData.GetLength(1); x++)
                        {
                            if (charData[y, x])
                            {
                                Rectangle pixelRect = new Rectangle(
                                    (int)(charPos.X + x * pixelSize),
                                    (int)(charPos.Y + y * pixelSize),
                                    pixelSize,
                                    pixelSize
                                );
                                spriteBatch.Draw(_pixelTexture, pixelRect, color);
                            }
                        }
                    }
                }
            }
        }
    }
}

