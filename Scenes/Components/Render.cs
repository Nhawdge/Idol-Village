using Raylib_cs;
using System.Numerics;
using VillageIdle.Utilities;

namespace VillageIdle.Scenes.Components
{
    internal class Render
    {
        public Texture2D Texture;
        public bool IsFlipped = false;
        public OriginAlignment OriginPos = OriginAlignment.Center;
        public int Column = 0;
        public int Row = 0;
        public int SourceX = 0;
        public int SourceY = 0;
        public int SpriteWidth = 0;
        public int SpriteHeight = 0;
        public float Scale = 1f;
        public float Rotation = 0f;
        public float RotationOffset = 0f;

        public bool CanRotate = true;
        public float ZIndex = 0f;
        public float RotationAsRadians => RenderRotation * (float)(Math.PI / 180);
        public Vector2 RotationAsVector2 => Vector2.Normalize(new Vector2((float)Math.Cos(RenderRotation), (float)Math.Sin(RenderRotation)));
        public Vector2 Position;
        public Color Color = Color.White;

        public TextureKey Key;

        public CollisionType Collision;

        public float RenderRotation
        {
            get => CanRotate ? Rotation + RotationOffset : 0f;
        }

        public virtual Vector2 Origin
        {
            get
            {
                switch (OriginPos)
                {
                    case OriginAlignment.Center:
                        return new Vector2(SpriteWidth / 2 * Scale, SpriteHeight / 2 * Scale);
                    case OriginAlignment.LeftCenter:
                        return new Vector2(0, SpriteHeight / 2 * Scale);
                    case OriginAlignment.LeftBottom:
                        return new Vector2(0, SpriteHeight * Scale);
                    case OriginAlignment.LeftTop:
                    default:
                        return Vector2.Zero;
                }
            }
        }

        public enum OriginAlignment
        {
            Center,
            LeftTop,
            LeftCenter,
            LeftBottom,
        }

        public void SetSource(Rectangle rect)
        {
            this.SourceX = (int)rect.X;
            this.SourceY = (int)rect.Y;
            this.SpriteWidth = (int)rect.Width;
            this.SpriteHeight = (int)rect.Height;
        }

        public virtual Rectangle Source
        {
            get
            {
                if (Column != 0 && Row != 0)
                    return new Rectangle(
                        Column * SpriteWidth + 2 * Column + 1,
                        Row * SpriteHeight + 2 * Row + 1,
                        SpriteWidth * (IsFlipped ? -1 : 1),
                        SpriteHeight
                        );
                return new Rectangle(SourceX, SourceY, SpriteWidth * (IsFlipped ? -1 : 1), SpriteHeight);
            }
        }

        public virtual Rectangle Destination
        {
            get => new Rectangle(
                    Position.X,
                    Position.Y,
                    SpriteWidth * Scale,
                    SpriteHeight * Scale);
        }

        public Render(TextureKey key, float scale = 1, bool isCentered = true)
        {
            Texture = TextureManager.Instance.GetTexture(key);
            Key = key;
            Position = Vector2.Zero;
            Scale = scale;
            Color = Color.White;
            OriginPos = isCentered ? OriginAlignment.Center : OriginAlignment.LeftTop;
            SpriteWidth = Texture.Width;
            SpriteHeight = Texture.Height;
        }

        public Render(Texture2D tilesprite, float scale = 1, bool isCentered = true)
        {
            Texture = tilesprite;
            Key = TextureKey.Empty;
            Position = Vector2.Zero;
            Scale = scale;
            Color = Color.White;
            OriginPos = isCentered ? OriginAlignment.Center : OriginAlignment.LeftTop;
            SpriteWidth = Texture.Width;
            SpriteHeight = Texture.Height;
        }

        public Render()
        {
        }
    }

    public enum CollisionType
    {
        None,
        Slow,
        Solid,
        Padding,
    }
}