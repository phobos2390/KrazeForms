using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KrazeForms
{
    class SpaceFactory
    {
        //private static IList<Tuple<SpaceType,Image,int>> images = initImages();
        //private static Image selectedImage = initSelectedImage();
        //private static const int spaceConstant = 10;

        //private static IList<Tuple<SpaceType, Image, int>> initImages()
        //{
        //    IList<Tuple<SpaceType, Image, int>> retImages = new List<Tuple<SpaceType, Image, int>>();
        //    retImages.Add(Tuple.Create<SpaceType, Image, int>(SpaceType.Key, (System.Drawing.Bitmap)System.Drawing.Image.FromFile("Kraze Key Tile.png"), 0));
        //    retImages.Add(Tuple.Create<SpaceType, Image, int>(SpaceType.Wall, (System.Drawing.Bitmap)System.Drawing.Image.FromFile("Brick Tile Kraze.png"), 0));
        //    retImages.Add(Tuple.Create<SpaceType, Image, int>(SpaceType.Empty, (System.Drawing.Bitmap)System.Drawing.Image.FromFile("Free Space Kraze.png"), 0));
        //    retImages.Add(Tuple.Create<SpaceType, Image, int>(SpaceType.Win, (System.Drawing.Bitmap)System.Drawing.Image.FromFile("Free Space Kraze.png"), 0));
        //    for (int i = 0; i < 9; i++)
        //    {
        //        int ctr = i + 1;
        //        retImages.Add(Tuple.Create<SpaceType, Image, int>(SpaceType.Door, (System.Drawing.Bitmap)System.Drawing.Image.FromFile(ctr + " tile.png"), ctr));
        //    }
        //    return retImages;
        //}

        //private static Image initSelectedImage()
        //{
        //    return (System.Drawing.Bitmap)System.Drawing.Image.FromFile("Kraze Selected Tile Highlight.png");
        //}

        private static IImageFactory imageFactory = initImageFactory();
        private static ISpaceFactory spaceFactory = initSpaceFactory();

        private static IImageFactory initImageFactory()
        {
            return new StandardTextureFactory();
        }

        private static ISpaceFactory initSpaceFactory()
        {
            return new StandardSpaceFactory();
        }

        private SpaceFactory() { }

        public static void SetImageFactory(IImageFactory imageFactory)
        {
            SpaceFactory.imageFactory = imageFactory;
        }

        public static void SetSpaceFactory(ISpaceFactory spaceFactory)
        {
            SpaceFactory.spaceFactory = spaceFactory;
        }

        public static Image CreateSelectedImage()
        {
            return imageFactory.CreateSelectedImage();
        }

        public static Image CreatePlayerImage(IDirection dir)
        {
            return imageFactory.CreatePlayerImage(dir);
        }

        public static Image CreateImage(SpaceType type, int num)
        {
            return imageFactory.CreateImage(type, num);
        }

        public static Image CreateImage(SpaceType type)
        {
            return imageFactory.CreateImage(type);
        }

        public static int GetSpaceHeightConstant()
        {
            return imageFactory.GetSpaceHeightConstant();
        }

        public static int GetSpaceWidthConstant()
        {
            return imageFactory.GetSpaceWidthConstant();
        }

        public static ISpace CreateSpaceFromChar(char type)
        {
            return spaceFactory.CreateSpaceFromChar(type);
        }

        public static ISpace CreateWall()
        {
            return spaceFactory.CreateWall();
        }

        public static ISpace CreateKeySpace()
        {
            return spaceFactory.CreateKeySpace();
        }

        public static ISpace CreateEmptySpace()
        {
            return spaceFactory.CreateEmptySpace();
        }

        public static ISpace CreateWinSpace()
        {
            return spaceFactory.CreateWinSpace();
        }

        public static ISpace CreateDoor(int keys)
        {
            return spaceFactory.CreateDoor(keys);
        }
    }
}
