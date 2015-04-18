using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KrazeForms
{
    class StandardTextureFactory:IImageFactory
    {
        private static int HEIGHTCONSTANT = 10;
        private static int WIDTHCONSTANT = 10;

        private static IList<Tuple<SpaceType, Image, int>> images = initImages();
        private static Image selectedImage = initSelectedImage();
        private static Image playerImage = initPlayerImage();
        
        private static IList<Tuple<SpaceType, Image, int>> initImages()
        {
            IList<Tuple<SpaceType, Image, int>> retImages = new List<Tuple<SpaceType, Image, int>>();
            retImages.Add(Tuple.Create<SpaceType, Image, int>(SpaceType.Key, new Bitmap(Image.FromFile("Kraze Key Tile.png"),WIDTHCONSTANT,HEIGHTCONSTANT), 0));
            retImages.Add(Tuple.Create<SpaceType, Image, int>(SpaceType.Wall, new Bitmap(Image.FromFile("Brick Tile Kraze.png"),WIDTHCONSTANT,HEIGHTCONSTANT), 0));
            retImages.Add(Tuple.Create<SpaceType, Image, int>(SpaceType.Empty, new Bitmap(Image.FromFile("Free Space Kraze.png"),WIDTHCONSTANT,HEIGHTCONSTANT), 0));
            retImages.Add(Tuple.Create<SpaceType, Image, int>(SpaceType.Win, new Bitmap(Image.FromFile("Free Space Kraze.png"), WIDTHCONSTANT, HEIGHTCONSTANT), 0));
            for (int i = 0; i < 9; i++)
            {
                int ctr = i + 1;
                retImages.Add(Tuple.Create<SpaceType, Image, int>(SpaceType.Door, new Bitmap(Image.FromFile(ctr + " tile.png"),WIDTHCONSTANT,HEIGHTCONSTANT), ctr));
            }
            return retImages;
        }

        private static Image initSelectedImage()
        {
            return new Bitmap(Image.FromFile("Kraze Selected Tile Highlight.png"), WIDTHCONSTANT, HEIGHTCONSTANT);
        }

        private static Image initPlayerImage()
        {
            return new Bitmap(Image.FromFile("Main Character Tile.png"),WIDTHCONSTANT,HEIGHTCONSTANT);
        }

        public Image CreateImage(SpaceType type, int num)
        {
            IEnumerable<Image> retImageEn = from tup in images
                                            where tup.Item1 == type
                                            && tup.Item3 == num
                                            select tup.Item2;
            return retImageEn.First();
        }

        public Image CreateImage(SpaceType type)
        {
            return CreateImage(type, 0);
        }

        public Image CreateSelectedImage()
        {
            return selectedImage;
        }

        public int GetSpaceHeightConstant()
        {
            return StandardTextureFactory.HEIGHTCONSTANT;
        }

        public int GetSpaceWidthConstant()
        {
            return StandardTextureFactory.WIDTHCONSTANT;
        }

        public Image CreatePlayerImage(IDirection dir)
        {
            return playerImage;
        }
    }
}
