﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DwarfCorp.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace DwarfCorp
{
    [JsonObject(IsReference = true)]
    public class Lamp : Body
    {
        public Lamp()
        {

        }

        public Lamp(Vector3 position) :
            base("Lamp", PlayState.ComponentManager.RootComponent, Matrix.CreateTranslation(position), new Vector3(1.0f, 1.0f, 1.0f), Vector3.Zero)
        {
            Texture2D spriteSheet = TextureManager.GetTexture(ContentPaths.Entities.Furniture.interior_furniture);

            List<Point> frames = new List<Point>
            {
                new Point(0, 1),
                new Point(2, 1),
                new Point(1, 1),
                new Point(2, 1)
            };
            Animation lampAnimation = new Animation(GameState.Game.GraphicsDevice, spriteSheet, "Lamp", 32, 32, frames, true, Color.White, 3.0f, 1f, 1.0f, false);

            Sprite sprite = new Sprite(PlayState.ComponentManager, "sprite", this, Matrix.Identity, spriteSheet, false)
            {
                LightsWithVoxels = false,
                OrientationType = Sprite.OrientMode.YAxis
            };
            sprite.AddAnimation(lampAnimation);


            lampAnimation.Play();
            Tags.Add("Lamp");



            Voxel voxelUnder = new Voxel();

            if (PlayState.ChunkManager.ChunkData.GetFirstVoxelUnder(position, ref voxelUnder))
            {
                new VoxelListener(PlayState.ComponentManager, this, PlayState.ChunkManager, voxelUnder);
            }


            new LightEmitter("light", this, Matrix.Identity, new Vector3(0.1f, 0.1f, 0.1f), Vector3.Zero, 255, 8)
            {
                HasMoved = true
            };
            CollisionType = CollisionManager.CollisionType.Static;
        }
    }
}
