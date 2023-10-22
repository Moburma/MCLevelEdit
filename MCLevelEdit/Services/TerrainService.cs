using MCLevelEdit.DataModel;
using MCLevelEdit.Interfaces;
using System.Threading.Tasks;

namespace MCLevelEdit.Services
{
    public class TerrainService : ITerrainService
    {
        public async Task<bool> CalculateTerrain(TerrainGenerationParameters genParams)
        {
            short[] mapEntityIndex_15B4E0 = new short[65536];
            sub_B5E70_decompress_terrain_map_level(mapEntityIndex_15B4E0, genParams.Seed, genParams.Offset, genParams.Raise, genParams.Gnarl);
            return true;
        }

        public void sub_B5E70_decompress_terrain_map_level(short[] mapEntityIndex_15B4E0, ushort seed, ushort offset, ushort raise, ushort gnarl)
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
                        sub_B5EFA(mapEntityIndex_15B4E0, (short)(1 << i), sumEnt, gnarl, ref seed);//355220
                    }
                    sumEnt.Word += (ushort)((2 * (1 << i)) << 8);
                }
                for (int j = 1 << (7 - i); j > 0; j--)
                {
                    for (int k = 1 << (7 - i); k > 0; k--)
                    {
                        sub_B5F8F(mapEntityIndex_15B4E0, (short)(1 << i), sumEnt, gnarl, ref seed);
                    }
                    sumEnt.Word += (ushort)((2 * (1 << i)) << 8);
                }
            }
        }

        void sub_B5EFA(short[] mapEntityIndex_15B4E0, short a1, UAxis2d indexx, ushort gnarl, ref ushort nextRand)//296EFA
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
            nextRand = srandNumber;
            //if (!mapEntityIndex_15B4E0[indexx.Word])
                mapEntityIndex_15B4E0[indexx.Word] = (short)(srandNumber % (ushort)(2 * gnarl + 1)
                + srandNumber % (ushort)((a1 << 6) + 1) + (sumEnt >> 2) - 32 * a1 - gnarl);
            indexx.Axis2D.X += (byte)a1;
            indexx.Axis2D.Y -= (byte)a1;
        }

        //----- (000B5F8F) --------------------------------------------------------
        void sub_B5F8F(short[] mapEntityIndex_15B4E0, short a1, UAxis2d indexx, ushort gnarl, ref ushort nextRand)//296f8f
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
            //if (!mapEntityIndex_15B4E0[indexx.Word])
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
            nextRand = srandNumber;
            //if (!mapEntityIndex_15B4E0[indexx.Word])
                mapEntityIndex_15B4E0[indexx.Word] = (short)(srandNumber % (ushort)(2 * gnarl + 1)
                + srandNumber % (ushort)((a1 << 6) + 1) + (ushort)(sumEnt2 >> 2) - 32 * a1 - gnarl);
            indexx.Axis2D.X += (byte)(2 * a1);
            indexx.Axis2D.Y -= (byte)a1;
        }
    }
}
