using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using MCLevelEdit.Avalonia;
using MCLevelEdit.DataModel;
using MCLevelEdit.Interfaces;
using MCLevelEdit.Utils;
using Splat;
using System.Threading.Tasks;

namespace MCLevelEdit.Services
{
    public class TerrainService : ITerrainService, IEnableLogger
    {
        public Task<WriteableBitmap> GenerateBitmapAsync(byte[] heightMap)
        {
            return Task.Run(() =>
            {
                WriteableBitmap bitmap = new WriteableBitmap(
                    new PixelSize(Globals.MAX_MAP_SIZE, Globals.MAX_MAP_SIZE),
                    new Vector(96, 96), // DPI (dots per inch)
                    PixelFormat.Rgba8888);

                return DrawBitmapAsync(heightMap, bitmap);
            });
        }

        public Task<WriteableBitmap> DrawBitmapAsync(byte[] heightMap, WriteableBitmap bitmap)
        {
            return Task.Run(() =>
            {
                BitmapUtils.SetBackground(new Rect(0, 0, Globals.MAX_MAP_SIZE, Globals.MAX_MAP_SIZE), new Color(255, 0, 0, 0), bitmap);

                using (var fb = bitmap.Lock())
                {
                    for (int y = 0; y < Globals.MAX_MAP_SIZE; y++)
                    {
                        for (int x = 0; x < Globals.MAX_MAP_SIZE; x++)
                        {
                            int index = (y * Globals.MAX_MAP_SIZE) + x;
                            fb.SetPixel(x, y, new Color(255, heightMap[index], heightMap[index], heightMap[index]));
                        }
                    }
                }
                BitmapUtils.SaveBitmap(bitmap);

                return bitmap;
            });
        }

        public async Task<byte[]> CalculateTerrain(TerrainGenerationParameters genParams)
        {
            short[] mapEntityIndex_15B4E0 = new short[Globals.MAX_MAP_SIZE * Globals.MAX_MAP_SIZE];
            byte[] mapHeightmap_11B4E0 = new byte[Globals.MAX_MAP_SIZE * Globals.MAX_MAP_SIZE];

            sub_B5E70_decompress_terrain_map_level(mapEntityIndex_15B4E0, (short)genParams.Seed, genParams.Offset, genParams.Raise, genParams.Gnarl);
            sub_44DB0_truncTerrainHeight(mapEntityIndex_15B4E0, mapHeightmap_11B4E0);//225db0 //trunc and create

            return mapHeightmap_11B4E0;
        }

        private void sub_B5E70_decompress_terrain_map_level(short[] mapEntityIndex_15B4E0, short seed, ushort offset, ushort raise, ushort gnarl)
        {
            UAxis2d sumEnt = new UAxis2d();

            mapEntityIndex_15B4E0[offset] = (short)raise;//32c4e0 //first seed
            for (short i = 7; i >= 0; i--)
            {
                sumEnt.Word = offset;
                for (int j = 1 << (7 - i); j > 0; j--)
                {
                    for (int k = 1 << (7 - i); k > 0; k--)
                    {
                        sub_B5EFA(mapEntityIndex_15B4E0, (short)(1 << i), ref sumEnt, gnarl, ref seed);//355220
                        this.Log().Debug($"sub_B5EFA Seed:{seed} offset:{offset} raise:{raise} gnarl:{gnarl}");
                    }
                    sumEnt.Word += (ushort)((2 * (1 << i)) << 8);
                }
                for (int j = 1 << (7 - i); j > 0; j--)
                {
                    for (int k = 1 << (7 - i); k > 0; k--)
                    {
                        sub_B5F8F(mapEntityIndex_15B4E0, (short)(1 << i), ref sumEnt, gnarl, ref seed);
                        this.Log().Debug($"sub_B5F8F Seed:{seed} offset:{offset} raise:{raise} gnarl:{gnarl}");
                    }
                    sumEnt.Word += (ushort)((2 * (1 << i)) << 8);
                }
            }
        }

        private void sub_B5EFA(short[] mapEntityIndex_15B4E0, short a1, ref UAxis2d indexx, ushort gnarl, ref short nextRand)//296EFA
        {
            //  X-.-X
            //   \  |
            //    E .
            //      |
            //  B---X

            short sumEnt;
            ushort srandNumber;

            sumEnt = mapEntityIndex_15B4E0[indexx.Word];
            indexx.Axis2D.X += (byte)(2 * a1);
            sumEnt += mapEntityIndex_15B4E0[indexx.Word];
            indexx.Axis2D.Y += (byte)(2 * a1);
            sumEnt += mapEntityIndex_15B4E0[indexx.Word];
            indexx.Axis2D.X -= (byte)(2 * a1);
            sumEnt += mapEntityIndex_15B4E0[indexx.Word];
            indexx.Axis2D.X += (byte)a1;
            indexx.Axis2D.Y -= (byte)a1;
            srandNumber = (ushort)(9377 * nextRand + 9439);
            nextRand = (short)srandNumber;
            //if (mapEntityIndex_15B4E0[indexx.Word] <= 0)
                mapEntityIndex_15B4E0[indexx.Word] = (short)(srandNumber % (ushort)(2 * gnarl + 1)
                + srandNumber % (ushort)((a1 << 6) + 1) + (sumEnt >> 2) - 32 * a1 - gnarl);
            indexx.Axis2D.X += (byte)a1;
            indexx.Axis2D.Y -= (byte)a1;
        }

        //----- (000B5F8F) --------------------------------------------------------
        private void sub_B5F8F(short[] mapEntityIndex_15B4E0, short a1, ref UAxis2d indexx, ushort gnarl, ref short nextRand)//296f8f
        {

            //   X
            //   |\
            // B E X
            //  \ /
            //   X

            short sumEnt;
            short sumEnt2;
            ushort srandNumber;

            sumEnt = mapEntityIndex_15B4E0[indexx.Word];
            sumEnt2 = sumEnt;
            indexx.Axis2D.X += (byte)a1;
            indexx.Axis2D.Y -= (byte)a1;
            sumEnt += mapEntityIndex_15B4E0[indexx.Word];
            indexx.Axis2D.X += (byte)a1;
            indexx.Axis2D.Y += (byte)a1;
            sumEnt += mapEntityIndex_15B4E0[indexx.Word];
            indexx.Axis2D.X -= (byte)a1;
            indexx.Axis2D.Y += (byte)a1;
            sumEnt += mapEntityIndex_15B4E0[indexx.Word];
            srandNumber = (ushort)(9377 * nextRand + 9439);
            sumEnt2 += mapEntityIndex_15B4E0[indexx.Word];
            indexx.Axis2D.Y -= (byte)a1;
            //if (mapEntityIndex_15B4E0[indexx.Word] <= 0)
                mapEntityIndex_15B4E0[indexx.Word] = (short)(srandNumber % (ushort)(2 * gnarl + 1)
                + srandNumber % (ushort)((a1 << 6) + 1) + (ushort)(sumEnt >> 2) - 32 * a1 - gnarl);

            //   X
            //  /|
            // X E-.
            //  \   \
            //   .-B R

            indexx.Axis2D.X -= (byte)(2 * a1);
            indexx.Axis2D.Y += (byte)a1;
            sumEnt2 += mapEntityIndex_15B4E0[indexx.Word];
            indexx.Axis2D.X += (byte)a1;
            indexx.Axis2D.Y += (byte)a1;
            sumEnt2 += mapEntityIndex_15B4E0[indexx.Word];
            indexx.Axis2D.Y -= (byte)a1;
            srandNumber = (ushort)(9377 * srandNumber + 9439);
            nextRand = (short)srandNumber;
            //if (mapEntityIndex_15B4E0[indexx.Word] <= 0)
                mapEntityIndex_15B4E0[indexx.Word] = (short)(srandNumber % (ushort)(2 * gnarl + 1)
                + srandNumber % (ushort)((a1 << 6) + 1) + (ushort)(sumEnt2 >> 2) - 32 * a1 - gnarl);
            indexx.Axis2D.X += (byte)(2 * a1);
            indexx.Axis2D.Y -= (byte)a1;
        }

        private void sub_44DB0_truncTerrainHeight(short[] mapEntityIndex_15B4E0, byte[] mapHeightmap_11B4E0)//225db0 // map to heightmap
        {
            int revMaxEnt = 0;
            uint weightedVar;
            int maxEnt = -32000;
            int minEnt = 32000;
            for (int i = 0; i < Globals.MAX_MAP_SIZE * Globals.MAX_MAP_SIZE; i++)
            {
                if (mapEntityIndex_15B4E0[i] > maxEnt)
                    maxEnt = mapEntityIndex_15B4E0[i];
                if (mapEntityIndex_15B4E0[i] < minEnt)
                    minEnt = mapEntityIndex_15B4E0[i];
            }
            if (maxEnt > 0)
                revMaxEnt = 0xC40000 / maxEnt;
            for (int i = 0; i < Globals.MAX_MAP_SIZE * Globals.MAX_MAP_SIZE; i++)
            {
                weightedVar = (uint)(revMaxEnt * mapEntityIndex_15B4E0[i] >> 16);
                mapEntityIndex_15B4E0[i] = 0;
                if ((weightedVar & 0x8000u) != 0)//water level trunc
                    weightedVar = 0;
                if (weightedVar > 196)//trunc max height
                    weightedVar = 196;
                mapHeightmap_11B4E0[i] = (byte)weightedVar;
            }
        }
    }
}
