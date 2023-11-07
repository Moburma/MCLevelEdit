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

                BitmapUtils.SetBackground(new Rect(0, 0, Globals.MAX_MAP_SIZE, Globals.MAX_MAP_SIZE), new Color(255, 0, 0, 0), bitmap);

                return DrawBitmapAsync(heightMap, bitmap);
            });
        }

        public Task<WriteableBitmap> DrawBitmapAsync(byte[] heightMap, WriteableBitmap bitmap)
        {
            return Task.Run(() =>
            {
                using (var fb = bitmap.Lock())
                {
                    for (int y = 0; y < Globals.MAX_MAP_SIZE; y++)
                    {
                        for (int x = 0; x < Globals.MAX_MAP_SIZE; x++)
                        {
                            int index = (y * Globals.MAX_MAP_SIZE) + x;
                            if (heightMap[index] > 20)
                            {
                                int j = 0;
                            }
                            fb.SetPixel(x, y, new Color(255, heightMap[index], heightMap[index], heightMap[index]));
                        }
                    }
                }
                //BitmapUtils.SaveBitmap(bitmap);

                return bitmap;
            });
        }

        public async Task<byte[]> CalculateTerrain(TerrainGenerationParameters genParams)
        {
            short[] mapEntityIndex_15B4E0 = new short[Globals.MAX_MAP_SIZE * Globals.MAX_MAP_SIZE];
            byte[] mapHeightmap_11B4E0 = new byte[Globals.MAX_MAP_SIZE * Globals.MAX_MAP_SIZE];
            byte[] mapAngle_13B4E0 = new byte[Globals.MAX_MAP_SIZE * Globals.MAX_MAP_SIZE];
            byte[] mapTerrainType_10B4E0 = new byte[Globals.MAX_MAP_SIZE * Globals.MAX_MAP_SIZE];

            ushort seed_17B4E0 = genParams.Seed;
            sub_B5E70_decompress_terrain_map_level(mapEntityIndex_15B4E0, (short)genParams.Seed, genParams.Offset, genParams.Raise, genParams.Gnarl);
            sub_44DB0_truncTerrainHeight(mapEntityIndex_15B4E0, mapHeightmap_11B4E0);//225db0 //trunc and create
            sub_44E40_generate_rivers(mapHeightmap_11B4E0, mapAngle_13B4E0, mapTerrainType_10B4E0, genParams.River, genParams.LRiver, ref seed_17B4E0);//225e40 //add any fields

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
                        //this.Log().Debug($"sub_B5EFA Seed:{seed} offset:{offset} raise:{raise} gnarl:{gnarl}");
                    }
                    sumEnt.Word += (ushort)((2 * (1 << i)) << 8);
                }
                for (int j = 1 << (7 - i); j > 0; j--)
                {
                    for (int k = 1 << (7 - i); k > 0; k--)
                    {
                        sub_B5F8F(mapEntityIndex_15B4E0, (short)(1 << i), ref sumEnt, gnarl, ref seed);
                        //this.Log().Debug($"sub_B5F8F Seed:{seed} offset:{offset} raise:{raise} gnarl:{gnarl}");
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
            indexx.X += (byte)(2 * a1);
            sumEnt += mapEntityIndex_15B4E0[indexx.Word];
            indexx.Y += (byte)(2 * a1);
            sumEnt += mapEntityIndex_15B4E0[indexx.Word];
            indexx.X -= (byte)(2 * a1);
            sumEnt += mapEntityIndex_15B4E0[indexx.Word];
            indexx.X += (byte)a1;
            indexx.Y -= (byte)a1;
            srandNumber = (ushort)(9377 * nextRand + 9439);
            nextRand = (short)srandNumber;
            //if (mapEntityIndex_15B4E0[indexx.Word] <= 0)
                mapEntityIndex_15B4E0[indexx.Word] = (short)(srandNumber % (ushort)(2 * gnarl + 1)
                + srandNumber % (ushort)((a1 << 6) + 1) + (sumEnt >> 2) - 32 * a1 - gnarl);
            indexx.X += (byte)a1;
            indexx.Y -= (byte)a1;
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
            indexx.X += (byte)a1;
            indexx.Y -= (byte)a1;
            sumEnt += mapEntityIndex_15B4E0[indexx.Word];
            indexx.X += (byte)a1;
            indexx.Y += (byte)a1;
            sumEnt += mapEntityIndex_15B4E0[indexx.Word];
            indexx.X -= (byte)a1;
            indexx.Y += (byte)a1;
            sumEnt += mapEntityIndex_15B4E0[indexx.Word];
            srandNumber = (ushort)(9377 * nextRand + 9439);
            sumEnt2 += mapEntityIndex_15B4E0[indexx.Word];
            indexx.Y -= (byte)a1;
            //if (mapEntityIndex_15B4E0[indexx.Word] <= 0)
                mapEntityIndex_15B4E0[indexx.Word] = (short)(srandNumber % (ushort)(2 * gnarl + 1)
                + srandNumber % (ushort)((a1 << 6) + 1) + (ushort)(sumEnt >> 2) - 32 * a1 - gnarl);

            //   X
            //  /|
            // X E-.
            //  \   \
            //   .-B R

            indexx.X -= (byte)(2 * a1);
            indexx.Y += (byte)a1;
            sumEnt2 += mapEntityIndex_15B4E0[indexx.Word];
            indexx.X += (byte)a1;
            indexx.Y += (byte)a1;
            sumEnt2 += mapEntityIndex_15B4E0[indexx.Word];
            indexx.Y -= (byte)a1;
            srandNumber = (ushort)(9377 * srandNumber + 9439);
            nextRand = (short)srandNumber;
            //if (mapEntityIndex_15B4E0[indexx.Word] <= 0)
                mapEntityIndex_15B4E0[indexx.Word] = (short)(srandNumber % (ushort)(2 * gnarl + 1)
                + srandNumber % (ushort)((a1 << 6) + 1) + (ushort)(sumEnt2 >> 2) - 32 * a1 - gnarl);
            indexx.X += (byte)(2 * a1);
            indexx.Y -= (byte)a1;
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

        private void sub_44E40_generate_rivers(byte[] mapHeightmap_11B4E0, byte[] mapAngle_13B4E0, byte[] mapTerrainType_10B4E0, int riverCount, ushort minSmooth, ref ushort seed_17B4E0)//225e40 rivers?
        {
            UAxis2d index = new UAxis2d();
            int locCount = riverCount;
            int i = 0;
            for (i = 0; i < Globals.MAX_MAP_SIZE * Globals.MAX_MAP_SIZE; i++)
            {
                if (mapHeightmap_11B4E0[i] > 0)
                    mapAngle_13B4E0[i] = 5;
                else
                    mapAngle_13B4E0[i] = 0;
            }
            i = 0;
            while ((locCount > 0) && (i < 1000))
            {
                for (i = 0; i < 1000; i++)
                {
                    seed_17B4E0 = (ushort)(9377 * seed_17B4E0 + 9439);
                    index.Word = (ushort)(seed_17B4E0 % 0xffffu);
                    if ((mapHeightmap_11B4E0[index.Word] > minSmooth) && mapAngle_13B4E0[index.Word] > 0)
                    {
                        sub_44EE0_smooth_tiles(mapHeightmap_11B4E0, mapAngle_13B4E0, mapTerrainType_10B4E0, index);
                        locCount--;
                        break;
                    }
                }
            }
            for (i = 0; i < Globals.MAX_MAP_SIZE * Globals.MAX_MAP_SIZE; i++)
            {
                mapTerrainType_10B4E0[i] = 255;
            }
        }

        private void sub_44EE0_smooth_tiles(byte[] mapHeightmap_11B4E0, byte[] mapAngle_13B4E0, byte[] mapTerrainType_10B4E0, UAxis2d axis)//225ee0
        {
            //  X-X-X
            //  |   |
            //  X B X
            //  | | |
            //  X X-X

            UAxis2d tempAxis2 = new UAxis2d();
            UAxis2d tempAxis1 = new UAxis2d();
            byte centralHeight;
            byte minHeight;

            tempAxis1.Word = axis.Word;
            for (int i = 0; i < Globals.MAX_MAP_SIZE * Globals.MAX_MAP_SIZE; i++)
            {
                mapTerrainType_10B4E0[i] = 3;
            }

            centralHeight = mapHeightmap_11B4E0[axis.Word];

            do
            {
                mapTerrainType_10B4E0[tempAxis1.Word] = 0;
                tempAxis1.Y--;
                minHeight = 255;
                if (mapTerrainType_10B4E0[tempAxis1.Word] > 0 && mapHeightmap_11B4E0[tempAxis1.Word] < 255)
                {
                    minHeight = mapHeightmap_11B4E0[tempAxis1.Word];
                    tempAxis2.Word = tempAxis1.Word;
                }
                tempAxis1.X++;
                if (mapTerrainType_10B4E0[tempAxis1.Word] > 0 && minHeight > mapHeightmap_11B4E0[tempAxis1.Word])
                {
                    minHeight = mapHeightmap_11B4E0[tempAxis1.Word];
                    tempAxis2.Word = tempAxis1.Word;
                }
                tempAxis1.Y++;
                if (mapTerrainType_10B4E0[tempAxis1.Word] > 0 && minHeight > mapHeightmap_11B4E0[tempAxis1.Word])
                {
                    minHeight = mapHeightmap_11B4E0[tempAxis1.Word];
                    tempAxis2.Word = tempAxis1.Word;
                }
                tempAxis1.Y++;
                if (mapTerrainType_10B4E0[tempAxis1.Word] > 0 && minHeight > mapHeightmap_11B4E0[tempAxis1.Word])
                {
                    minHeight = mapHeightmap_11B4E0[tempAxis1.Word];
                    tempAxis2.Word = tempAxis1.Word;
                }
                tempAxis1.X--;
                if (mapTerrainType_10B4E0[tempAxis1.Word] > 0 && minHeight > mapHeightmap_11B4E0[tempAxis1.Word])
                {
                    minHeight = mapHeightmap_11B4E0[tempAxis1.Word];
                    tempAxis2.Word = tempAxis1.Word;
                }
                tempAxis1.X--;
                if (mapTerrainType_10B4E0[tempAxis1.Word] > 0 && minHeight > mapHeightmap_11B4E0[tempAxis1.Word])
                {
                    minHeight = mapHeightmap_11B4E0[tempAxis1.Word];
                    tempAxis2.Word = tempAxis1.Word;
                }
                tempAxis1.Y--;
                if (mapTerrainType_10B4E0[tempAxis1.Word] > 0 && minHeight > mapHeightmap_11B4E0[tempAxis1.Word])
                {
                    minHeight = mapHeightmap_11B4E0[tempAxis1.Word];
                    tempAxis2.Word = tempAxis1.Word;
                }
                tempAxis1.Y--;
                if (mapTerrainType_10B4E0[tempAxis1.Word] > 0 && minHeight > mapHeightmap_11B4E0[tempAxis1.Word])
                {
                    minHeight = mapHeightmap_11B4E0[tempAxis1.Word];
                    tempAxis2.Word = tempAxis1.Word;
                }
                if (mapAngle_13B4E0[tempAxis2.Word] == 0 || minHeight == 255)
                    break;

                if (minHeight > centralHeight)//if near tile is higger then central tile set central as near tile
                    mapHeightmap_11B4E0[tempAxis2.Word] = centralHeight;

                centralHeight = mapHeightmap_11B4E0[tempAxis2.Word];
                tempAxis1.Word = tempAxis2.Word;
            } while (centralHeight > 0);

            for (int i = 0; i < Globals.MAX_MAP_SIZE * Globals.MAX_MAP_SIZE; i++)
            {
                if (mapTerrainType_10B4E0[i] == 0)
                {
                    mapAngle_13B4E0[i] = 0;
                }
            }
        }
    }
}
