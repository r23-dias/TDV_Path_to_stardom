using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Path_to_stardom_TDV
{
    public enum GameState
    {
        MainMenu,
        Playing,
        Credits
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameState _currentState;

        // Menu System
        private MainMenu _mainMenu;
        private CreditsScreen _creditsScreen;

        // Game Objects
        private Fighter player1;
        private Fighter player2;
        private StaticBackground _background;
        private Camera _camera;

        // Input
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;

        private float _backgroundWidth;
        private float _leftLimit;
        private float _rightLimit;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Mantém sua resolução de 1280x720 mas em tela cheia
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            _currentState = GameState.MainMenu;
        }

        protected override void Initialize()
        {
            // Inicializar sistema de menus
            _mainMenu = new MainMenu();
            _creditsScreen = new CreditsScreen();

            // Inicializar objetos do jogo
            Vector2 player1StartPos = new Vector2(-320, 500);
            Vector2 player2StartPos = new Vector2(320, 588);

            // Calcula o ponto médio entre os dois jogadores
            Vector2 cameraStartPos = new Vector2(0,341);

            _camera = new Camera(GraphicsDevice.Viewport, cameraStartPos);

            player1 = new Fighter("Player1", "MKing", player1StartPos, Color.White);
            player2 = new Fighter("Player2", "FWarrior", player2StartPos, Color.White);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Carregar conteúdo dos menus
            _mainMenu.LoadContent(Content, GraphicsDevice);
            _creditsScreen.LoadContent(Content, GraphicsDevice);

            _background = new StaticBackground(
                screenWidth: _graphics.PreferredBackBufferWidth,
                verticalOffset: -150 // Valores negativos = sobe, positivos = desce
            );
            _background.LoadContent(Content);

            // Calcular limites dos jogadores com base no fundo
            _backgroundWidth = _background.GetLayerWidth();
            _leftLimit = -_backgroundWidth;
            _rightLimit = _backgroundWidth * 2;

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player1.LoadContent(Content);
            player2.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            // Sair do jogo
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                currentKeyboardState.IsKeyDown(Keys.Escape))
            {
                if (_currentState == GameState.MainMenu)
                    Exit();
                else
                    _currentState = GameState.MainMenu;
            }

            switch (_currentState)
            {
                case GameState.MainMenu:
                    UpdateMainMenu();
                    break;

                case GameState.Playing:
                    UpdateGame(gameTime);
                    break;

                case GameState.Credits:
                    UpdateCredits();
                    break;
            }

            player1.Update(gameTime, currentKeyboardState, previousKeyboardState, player2);
            player2.Update(gameTime, currentKeyboardState, previousKeyboardState, player1);
            ClampPlayerPosition(player1);
            ClampPlayerPosition(player2);
            _camera.Update(player1.Position, player2.Position);

            base.Update(gameTime);
        }

        private void UpdateMainMenu()
        {
            int selectedOption = _mainMenu.Update(currentKeyboardState, previousKeyboardState);

            switch (selectedOption)
            {
                case 0: // NOVO JOGO
                    _currentState = GameState.Playing;
                    break;

                case 1: // CREDITOS
                    _currentState = GameState.Credits;
                    break;

                case 2: // SAIR
                    Exit();
                    break;
            }
        }

        private void UpdateGame(GameTime gameTime)
        {
            player1.Update(gameTime, currentKeyboardState, previousKeyboardState, player2);
            player2.Update(gameTime, currentKeyboardState, previousKeyboardState, player1);
            

            // Verificar colisões de ataque
            CheckAttackCollisions();
        }

        private void UpdateCredits()
        {
            if (_creditsScreen.Update(currentKeyboardState, previousKeyboardState))
            {
                _currentState = GameState.MainMenu;
            }
        }

        private void CheckAttackCollisions()
        {
            // Player 1 atacando Player 2
            if (player1.IsAttacking && !player1.HasHit)
            {
                foreach (var attackHitbox in player1.AttackHitboxes)
                {
                    if (attackHitbox.Intersects(player2.Hitbox))
                    {
                        player2.TakeDamage(player1.AttackDamage);
                        player1.HasHit = true;
                        break;
                    }
                }
            }

            // Player 2 atacando Player 1
            if (player2.IsAttacking && !player2.HasHit)
            {
                foreach (var attackHitbox in player2.AttackHitboxes)
                {
                    if (attackHitbox.Intersects(player1.Hitbox))
                    {
                        player1.TakeDamage(player2.AttackDamage);
                        player2.HasHit = true;
                        break;
                    }
                }
            }
        }

        private void ClampPlayerPosition(Fighter player)
        {
            Vector2 position = player.Position;
            position.X = MathHelper.Clamp(position.X, _leftLimit, _rightLimit);
            player.Position = position;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(
                sortMode: SpriteSortMode.BackToFront,
                blendState: BlendState.AlphaBlend,
                samplerState: SamplerState.PointClamp,
                depthStencilState: DepthStencilState.None,
                rasterizerState: RasterizerState.CullNone,
                effect: null,
                transformMatrix: _camera.TransformMatrix);

            switch (_currentState)
            {
                case GameState.MainMenu:
                    _mainMenu.Draw(_spriteBatch);
                    break;

                case GameState.Playing:
                    DrawGame();
                    break;

                case GameState.Credits:
                    _creditsScreen.Draw(_spriteBatch);
                    break;
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawGame()
        {
            // Desenha o background (já com depth configurado no método anterior)
            _background.Draw(_spriteBatch, _camera.Position);

            // Desenha os players com depth fixo (entre as layers do background)
            player1.Draw(_spriteBatch, GraphicsDevice);
            player2.Draw(_spriteBatch, GraphicsDevice);

            DrawHealthBars();
        }

        private void DrawHealthBars()
        {
            // Calcular posição das barras baseada na câmera
            Vector2 cameraOffset = _camera.Position;

            // Barra de vida do Player 1 - sempre no canto superior esquerdo da tela
            Rectangle healthBarP1 = new Rectangle(
                (int)(cameraOffset.X - 590), // Ajusta para ficar no canto esquerdo da tela
                (int)(cameraOffset.Y - 310), // Ajusta para ficar no topo da tela
                500,
                30
            );
            Rectangle currentHealthP1 = new Rectangle(
                (int)(cameraOffset.X - 590),
                (int)(cameraOffset.Y - 310),
                (int)(500 * (player1.Health / 100f)),
                30
            );

            _spriteBatch.Draw(CreateColorTexture(Color.Gray), healthBarP1, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01f);
            _spriteBatch.Draw(CreateColorTexture(Color.Green), currentHealthP1, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.009f);

            // Barra de vida do Player 2 - sempre no canto superior direito da tela
            Rectangle healthBarP2 = new Rectangle(
                (int)(cameraOffset.X + 90), // Ajusta para ficar no canto direito da tela
                (int)(cameraOffset.Y - 310), // Ajusta para ficar no topo da tela
                500,
                30
            );
            Rectangle currentHealthP2 = new Rectangle(
                (int)(cameraOffset.X + 90),
                (int)(cameraOffset.Y - 310),
                (int)(500 * (player2.Health / 100f)),
                30
            );

            _spriteBatch.Draw(CreateColorTexture(Color.Gray), healthBarP2, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01f);
            _spriteBatch.Draw(CreateColorTexture(Color.Green), currentHealthP2, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.009f);
        }


        private Texture2D CreateColorTexture(Color color)
        {
            Texture2D texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new[] { color });
            return texture;
        }
    }
}
