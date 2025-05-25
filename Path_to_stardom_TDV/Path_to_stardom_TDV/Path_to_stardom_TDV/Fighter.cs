using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Path_to_stardom_TDV
{
    public class Fighter
    {
        public string Name { get; private set; }
        public string CharacterType { get; private set; }
        public Vector2 Position { get; set; }
        public float Health { get; private set; } = 100f;
        public bool IsAttacking { get; private set; }
        public bool IsJumping { get; private set; }
        public bool HasHit { get; set; }
        public int AttackDamage { get; private set; }
        public Rectangle Hitbox { get; private set; }
        public List<Rectangle> AttackHitboxes { get; private set; } = new List<Rectangle>();
        public bool ShowHitboxes = true;

        private Dictionary<string, AttackInfo> _attacks = new Dictionary<string, AttackInfo>();
        private AttackInfo _currentAttack;
        private Dictionary<AnimationState, Animation> _animations;
        private Animation _currentAnimationData;
        private float _animationFrameTimer;
        private int _currentFrame;
        private Texture2D _hitboxTexture;
        private Color _drawColor;
        private Vector2 _velocity;
        private float _jumpForce = -15f;
        private float _gravity = 0.5f;
        private float _moveSpeed = 5f;
        private float _groundY = 200f;
        private AnimationState _currentFighterState;
        private bool _isFacingRight;

        public Fighter(string name, string characterType, Vector2 position, Color drawColor)
        {
            Name = name;
            CharacterType = characterType;
            Position = position;
            _drawColor = drawColor;
            _animations = new Dictionary<AnimationState, Animation>();
            _currentFighterState = AnimationState.Idle;
            _isFacingRight = (position.X < 640);
            InitializeAttacks();
            _groundY = position.Y;
        }

        public void LoadContent(ContentManager content)
        {
            if (CharacterType == "MKing")
            {
                _animations.Add(AnimationState.Idle, new Animation(content.Load<Texture2D>("MKing/MKingIdle"), 160, 111, 8, 0.12f));
                _animations.Add(AnimationState.Walk, new Animation(content.Load<Texture2D>("MKing/MKingWalk"), 160, 111, 8, 0.08f));
                _animations.Add(AnimationState.Attack1, new Animation(content.Load<Texture2D>("MKing/MKingAttack1"), 160, 111, 4, 0.1f));
                _animations.Add(AnimationState.Attack2, new Animation(content.Load<Texture2D>("MKing/MKingAttack2"), 160, 111, 4, 0.12f));
                _animations.Add(AnimationState.Attack3, new Animation(content.Load<Texture2D>("MKing/MKingAttack3"), 160, 111, 4, 0.14f));
            }
            else if (CharacterType == "FWarrior")
            {
                _animations.Add(AnimationState.Idle, new Animation(content.Load<Texture2D>("FWarrior/FWarriorIdle"), 162, 162, 10, 0.12f));
                _animations.Add(AnimationState.Walk, new Animation(content.Load<Texture2D>("FWarrior/FWarriorWalk"), 162, 162, 8, 0.08f));
                _animations.Add(AnimationState.Attack1, new Animation(content.Load<Texture2D>("FWarrior/FWarriorAttack1"), 162, 162, 7, 0.09f));
                _animations.Add(AnimationState.Attack2, new Animation(content.Load<Texture2D>("FWarrior/FWarriorAttack2"), 162, 162, 7, 0.11f));
                _animations.Add(AnimationState.Attack3, new Animation(content.Load<Texture2D>("FWarrior/FWarriorAttack3"), 162, 162, 8, 0.13f));
            }

            _currentAnimationData = _animations[_currentFighterState];
            _hitboxTexture = new Texture2D(_currentAnimationData.SpriteSheet.GraphicsDevice, 1, 1);
            _hitboxTexture.SetData(new[] { Color.White });
        }

        public void Update(GameTime gameTime, KeyboardState currentKeyboardState, KeyboardState previousKeyboardState, Fighter opponent)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (IsJumping)
            {
                _velocity.Y += _gravity;
                Position += new Vector2(0, _velocity.Y);
                if (Position.Y >= _groundY)
                {
                    Position = new Vector2(Position.X, _groundY);
                    IsJumping = false;
                    _velocity.Y = 0;
                    if (_currentFighterState == AnimationState.Jump)
                        SetFighterState(AnimationState.Idle);
                }
            }
            bool movingHorizontally = false;
            if (Name == "Player1")
            {
                if (currentKeyboardState.IsKeyDown(Keys.A))
                {
                    Position = new Vector2(Position.X - _moveSpeed, Position.Y);
                    movingHorizontally = true;
                    _isFacingRight = false;
                }
                if (currentKeyboardState.IsKeyDown(Keys.D))
                {
                    Position = new Vector2(Position.X + _moveSpeed, Position.Y);
                    movingHorizontally = true;
                    _isFacingRight = true;
                }
                if (currentKeyboardState.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W))
                {
                    Jump();
                }
                if (currentKeyboardState.IsKeyDown(Keys.F) && !previousKeyboardState.IsKeyDown(Keys.F))
                {
                    MKAttack1();
                }
                if (currentKeyboardState.IsKeyDown(Keys.G) && !previousKeyboardState.IsKeyDown(Keys.G))
                {
                    MKAttack2();
                }
                if (currentKeyboardState.IsKeyDown(Keys.H) && !previousKeyboardState.IsKeyDown(Keys.H))
                {
                    MKAttack3();
                }
            }
            else if (Name == "Player2")
            {
                if (currentKeyboardState.IsKeyDown(Keys.Left))
                {
                    Position = new Vector2(Position.X - _moveSpeed, Position.Y);
                    movingHorizontally = true;
                    _isFacingRight = false;
                }
                if (currentKeyboardState.IsKeyDown(Keys.Right))
                {
                    Position = new Vector2(Position.X + _moveSpeed, Position.Y);
                    movingHorizontally = true;
                    _isFacingRight = true;
                }
                if (currentKeyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up))
                {
                    Jump();
                }
                if (currentKeyboardState.IsKeyDown(Keys.NumPad1) && !previousKeyboardState.IsKeyDown(Keys.NumPad1))
                {
                    FWAttack1();
                }
                if (currentKeyboardState.IsKeyDown(Keys.NumPad2) && !previousKeyboardState.IsKeyDown(Keys.NumPad2))
                {
                    FWAttack2();
                }
                if (currentKeyboardState.IsKeyDown(Keys.NumPad3) && !previousKeyboardState.IsKeyDown(Keys.NumPad3))
                {
                    FWAttack3();
                }
            }

            if (!IsAttacking && !IsJumping)
            {
                SetFighterState(movingHorizontally ? AnimationState.Walk : AnimationState.Idle);
            }

            _animationFrameTimer += deltaTime;
            if (_animationFrameTimer >= _currentAnimationData.FrameDuration)
            {
                _animationFrameTimer -= _currentAnimationData.FrameDuration;
                _currentFrame = (_currentFrame + 1) % _currentAnimationData.NumberOfFrames;

                if (_currentFighterState.ToString().StartsWith("Attack") && _currentFrame == 0 && IsAttacking)
                {
                    IsAttacking = false;
                    HasHit = false;
                    SetFighterState(AnimationState.Idle);
                }
            }

            UpdateHitboxes();
        }

        private void InitializeAttacks()
        {
            if (CharacterType == "MKing")
            {
                _attacks.Add("MKAttack1", new AttackInfo(
                    "MKAttack1", AnimationState.Attack1, 10,
                    new List<Rectangle> {
                        new Rectangle(50, 50, 160, 50),
                        new Rectangle(-20, 20, 160, 30),
                        new Rectangle(-20, 100, 160, 20)
                    },
                    1, 3));

                _attacks.Add("MKAttack2", new AttackInfo(
                    "MKAttack2", AnimationState.Attack2, 10,
                    new List<Rectangle> {
                        new Rectangle(-180, -10, 100, 120),
                        new Rectangle(-80, 100, 220, 40),
                        new Rectangle(140, 85, 60, 30),
                        new Rectangle(200, 35, 35, 50)
                    },
                    1, 3));

                _attacks.Add("MKAttack3", new AttackInfo(
                    "MKAttack3", AnimationState.Attack3, 10,
                    new List<Rectangle> {
                        new Rectangle(130, -80, 80, 200),
                        new Rectangle(85, -120, 100, 40),
                        new Rectangle(0, -160, 100, 40)
                    },
                    1, 3));
            }
            else if (CharacterType == "FWarrior")
            {
                _attacks.Add("FWAttack1", new AttackInfo(
                    "FWAttack1", AnimationState.Attack1, 10,
                    new List<Rectangle> {
                        new Rectangle(100, -40, 40, 60),
                        new Rectangle(50, -60, 80, 20),
                        new Rectangle(20, 20, 80, 20)
                    },
                    1, 3));

                _attacks.Add("FWAttack2", new AttackInfo(
                    "FWAttack2", AnimationState.Attack2, 10,
                    new List<Rectangle> {
                        new Rectangle(-160, -80, 60, 90),
                        new Rectangle(-100, 20, 180, 30),
                        new Rectangle(80, -20, 40, 50),
                        new Rectangle(120, -50, 15, 40)
                    },
                    1, 3));

                _attacks.Add("FWAttack3", new AttackInfo(
                    "FWAttack3", AnimationState.Attack3, 10,
                    new List<Rectangle> {
                        new Rectangle(55, -130, 80, 200),
                        new Rectangle(0, -170, 100, 40),
                        new Rectangle(-80, -180, 80, 40)
                    },
                    1, 3));
            }
        }

        private void UpdateHitboxes()
        {
            const float scale = 3f;
            int spriteWidth = _currentAnimationData.FrameWidth;
            int spriteHeight = _currentAnimationData.FrameHeight;

            int hitboxWidth = spriteWidth;
            int hitboxHeight = spriteHeight;
            int offsetX = 0;
            int offsetY = 0;

            if (CharacterType == "MKing")
            {
                hitboxWidth = (int)(spriteWidth * 0.16f);
                hitboxHeight = (int)(spriteHeight * 0.5f);
                offsetX = (int)(spriteWidth * -0.15f);
                offsetY = (int)(spriteHeight * -0.12f);
            }
            else if (CharacterType == "FWarrior")
            {
                hitboxWidth = (int)(spriteWidth * 0.15f);
                hitboxHeight = (int)(spriteHeight * 0.3f);
                offsetX = (int)(spriteWidth * -0.22f);
                offsetY = (int)(spriteHeight * -0.50f);
            }

            // Ajuste para quando o personagem estiver virado para a esquerda
            if (!_isFacingRight)
            {
                if (CharacterType == "MKing")
                {
                    offsetX = (int)(spriteWidth * -0.3f);
                }
                else if (CharacterType == "FWarrior")
                {
                    offsetX = (int)(spriteWidth * -0.25f);
                }
            }

            Hitbox = new Rectangle(
                (int)Position.X + offsetX,
                (int)Position.Y + offsetY,
                (int)(hitboxWidth * scale),
                (int)(hitboxHeight * scale)
            );

            AttackHitboxes.Clear();

            if (IsAttacking && _currentAttack != null &&
                _currentFrame >= _currentAttack.ActiveStartFrame &&
                _currentFrame <= _currentAttack.ActiveEndFrame)
            {
                foreach (var hitbox in _currentAttack.Hitboxes)
                {
                    Rectangle actualHitbox = new Rectangle(
                        (int)Position.X + (_isFacingRight ? hitbox.X : -hitbox.X - hitbox.Width),
                        (int)Position.Y + hitbox.Y,
                        hitbox.Width,
                        hitbox.Height
                    );
                    AttackHitboxes.Add(actualHitbox);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            SpriteEffects effect = _isFacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Rectangle sourceRect = new Rectangle(
                _currentFrame * _currentAnimationData.FrameWidth,
                0,
                _currentAnimationData.FrameWidth,
                _currentAnimationData.FrameHeight
            );

            // Depth fixo para os personagens (entre as layers de background)
            float depth = 0.05f; // Ajuste este valor conforme necessário

            Vector2 origin = new Vector2(
                _currentAnimationData.FrameWidth / 2f,
                _currentAnimationData.FrameHeight / 2f
            );

            spriteBatch.Draw(
                _currentAnimationData.SpriteSheet,
                Position,
                sourceRect,
                _drawColor,
                0f,
                origin,
                3f,
                effect,
                depth
            );

            if (ShowHitboxes)
            {
                if (_hitboxTexture == null)
                {
                    _hitboxTexture = new Texture2D(graphicsDevice, 1, 1);
                    _hitboxTexture.SetData(new[] { Color.White });
                }

                spriteBatch.Draw(_hitboxTexture, Hitbox, Color.Red * 0.5f);

                foreach (var hitbox in AttackHitboxes)
                {
                    spriteBatch.Draw(_hitboxTexture, hitbox, Color.Blue * 0.5f);
                }
            }
        }

        private void SetFighterState(AnimationState newState)
        {
            if (_currentFighterState == newState) return;

            _currentFighterState = newState;
            _currentAnimationData = _animations[newState];
            _currentFrame = 0;
            _animationFrameTimer = 0;

            if (newState.ToString().StartsWith("Attack"))
            {
                IsAttacking = true;
                HasHit = false;
            }
        }

        public void Jump()
        {
            if (!IsJumping)
            {
                IsJumping = true;
                _velocity.Y = _jumpForce;
                if (_animations.ContainsKey(AnimationState.Jump))
                    SetFighterState(AnimationState.Jump);
            }
        }

        public void MKAttack1()
        {
            if (!IsAttacking && _attacks.TryGetValue("MKAttack1", out var attack))
            {
                _currentAttack = attack;
                AttackDamage = attack.Damage;
                SetFighterState(attack.AnimationState);
            }
        }

        public void MKAttack2()
        {
            if (!IsAttacking && _attacks.TryGetValue("MKAttack2", out var attack))
            {
                _currentAttack = attack;
                AttackDamage = attack.Damage;
                SetFighterState(attack.AnimationState);
            }
        }

        public void MKAttack3()
        {
            if (!IsAttacking && _attacks.TryGetValue("MKAttack3", out var attack))
            {
                _currentAttack = attack;
                AttackDamage = attack.Damage;
                SetFighterState(attack.AnimationState);
            }
        }

        public void FWAttack1()
        {
            if (!IsAttacking && _attacks.TryGetValue("FWAttack1", out var attack))
            {
                _currentAttack = attack;
                AttackDamage = attack.Damage;
                SetFighterState(attack.AnimationState);
            }
        }

        public void FWAttack2()
        {
            if (!IsAttacking && _attacks.TryGetValue("FWAttack2", out var attack))
            {
                _currentAttack = attack;
                AttackDamage = attack.Damage;
                SetFighterState(attack.AnimationState);
            }
        }

        public void FWAttack3()
        {
            if (!IsAttacking && _attacks.TryGetValue("FWAttack3", out var attack))
            {
                _currentAttack = attack;
                AttackDamage = attack.Damage;
                SetFighterState(attack.AnimationState);
            }
        }
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0)
                Health = 0;
        }


    }       
}
