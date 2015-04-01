using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KrazeForms
{
    class BiggerTextureFactory:IImageFactory
    {
        private static int HEIGHTCONSTANT = 30;
        private static int WIDTHCONSTANT = 30;
        private static string basePath = "BiggerTextures\\";

        private static IList<Tuple<SpaceType, Image, int>> images = initImages();
        private static Image selectedImage = initSelectedImage();
        private static IDictionary<IDirection,Image> playerImages = initPlayerImages();

        private static IList<Tuple<SpaceType, Image, int>> initImages()
        {
            IList<Tuple<SpaceType, Image, int>> retImages = new List<Tuple<SpaceType, Image, int>>();
            retImages.Add(Tuple.Create<SpaceType, Image, int>(SpaceType.Key, new Bitmap(Image.FromFile(basePath + "KeySpace.png"), WIDTHCONSTANT, HEIGHTCONSTANT), 0));
            retImages.Add(Tuple.Create<SpaceType, Image, int>(SpaceType.Wall, new Bitmap(Image.FromFile(basePath + "WallTextureV2.png"), WIDTHCONSTANT, HEIGHTCONSTANT), 0));
            retImages.Add(Tuple.Create<SpaceType, Image, int>(SpaceType.Empty, new Bitmap(Image.FromFile(basePath + "FreeSpace.png"), WIDTHCONSTANT, HEIGHTCONSTANT), 0));
            retImages.Add(Tuple.Create<SpaceType, Image, int>(SpaceType.Win, new Bitmap(Image.FromFile(basePath + "FreeSpace.png"), WIDTHCONSTANT, HEIGHTCONSTANT), 0));
            for (int i = 0; i < 9; i++)
            {
                int ctr = i + 1;
                retImages.Add(Tuple.Create<SpaceType, Image, int>(SpaceType.Door, new Bitmap(Image.FromFile(basePath + ctr + " Door.png"), WIDTHCONSTANT, HEIGHTCONSTANT), ctr));
            }
            return retImages;
        }

        private static Image initSelectedImage()
        {
            return new Bitmap(Image.FromFile(basePath + "SpaceSelected.png"),WIDTHCONSTANT,HEIGHTCONSTANT);
        }

        private static IDictionary<IDirection, Image> initPlayerImages()
        {
            IDictionary<IDirection, Image> playerImages = new Dictionary<IDirection, Image>();
            playerImages.Add(new NoDirection(), new Bitmap(Image.FromFile(basePath + "PlayerForwardImage.png"),WIDTHCONSTANT,HEIGHTCONSTANT));
            playerImages.Add((AbstractDirection)DirectionFactory.CreateDirection(Direction.Up), new Bitmap(Image.FromFile(basePath + "PlayerBackwardImage.png"),WIDTHCONSTANT,HEIGHTCONSTANT));
            playerImages.Add((AbstractDirection)DirectionFactory.CreateDirection(Direction.Down), new Bitmap(Image.FromFile(basePath + "PlayerForwardImage.png"),WIDTHCONSTANT,HEIGHTCONSTANT));
            playerImages.Add((AbstractDirection)DirectionFactory.CreateDirection(Direction.Left), new Bitmap(Image.FromFile(basePath + "PlayerLeftImage.png"),WIDTHCONSTANT,HEIGHTCONSTANT));
            playerImages.Add((AbstractDirection)DirectionFactory.CreateDirection(Direction.Right), new Bitmap(Image.FromFile(basePath + "PlayerRightImage.png"), WIDTHCONSTANT, HEIGHTCONSTANT));
            //playerImages.Add((AbstractDirection)DirectionFactory.CreateDirection(Direction.Up), (Bitmap)Image.FromFile(basePath + "PlayerBackwardImage.png"));
            //playerImages.Add((AbstractDirection)DirectionFactory.CreateDirection(Direction.Down), (Bitmap)Image.FromFile(basePath + "PlayerForwardImage.png"));
            //playerImages.Add((AbstractDirection)DirectionFactory.CreateDirection(Direction.Left), (Bitmap)Image.FromFile(basePath + "PlayerLeftImage.png"));
            //playerImages.Add((AbstractDirection)DirectionFactory.CreateDirection(Direction.Right), (Bitmap)Image.FromFile(basePath + "PlayerRightImage.png"));
            return playerImages;
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

        public Image CreatePlayerImage(IDirection dir)
        {
            return playerImages[(AbstractDirection)dir];
        }

        public int GetSpaceHeightConstant()
        {
            return HEIGHTCONSTANT;
        }

        public int GetSpaceWidthConstant()
        {
            return WIDTHCONSTANT;
        }
    }
}
