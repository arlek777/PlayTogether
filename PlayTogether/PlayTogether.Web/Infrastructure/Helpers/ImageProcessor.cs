using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Kaliko.ImageLibrary;
using Kaliko.ImageLibrary.Scaling;

namespace PlayTogether.Web.Infrastructure.Helpers
{
    public static class ImageProcessor
    {
        /// <summary>
        /// Fixes EXIF orientation problem and returns reduced image to save more space in DB.
        /// </summary>
        public static string ProccessPhoto(byte[] photoBytes)
        {
            var memoryStream = new MemoryStream(photoBytes);
            var image = Image.FromStream(memoryStream);
            KalikoImage kalikoImage = new KalikoImage(image);
            foreach (var prop in image.PropertyItems)
            {
                if (prop.Id == 0x0112) //value of EXIF
                {
                    int orientationValue = image.GetPropertyItem(prop.Id).Value[0];
                    RotateFlipType rotateFlipType = GetOrientationToFlipType(orientationValue);
                    image.RotateFlip(rotateFlipType);

                    memoryStream.Close();
                    memoryStream = new MemoryStream();
                    image.Save(memoryStream, ImageFormat.Jpeg);
                    kalikoImage = new KalikoImage(memoryStream);
                    break;
                }
            }

            var thumbnail = kalikoImage.Scale(new FitScaling(image.Width / 4, image.Height / 4));
            memoryStream.Close();
            memoryStream = new MemoryStream();
            thumbnail.SaveJpg(memoryStream, 50);

            return Convert.ToBase64String(memoryStream.GetBuffer());
        }

        private static RotateFlipType GetOrientationToFlipType(int orientationValue)
        {
            RotateFlipType rotateFlipType = RotateFlipType.RotateNoneFlipNone;

            switch (orientationValue)
            {
                case 1:
                    rotateFlipType = RotateFlipType.RotateNoneFlipNone;
                    break;
                case 2:
                    rotateFlipType = RotateFlipType.RotateNoneFlipX;
                    break;
                case 3:
                    rotateFlipType = RotateFlipType.Rotate180FlipNone;
                    break;
                case 4:
                    rotateFlipType = RotateFlipType.Rotate180FlipX;
                    break;
                case 5:
                    rotateFlipType = RotateFlipType.Rotate90FlipX;
                    break;
                case 6:
                    rotateFlipType = RotateFlipType.Rotate90FlipNone;
                    break;
                case 7:
                    rotateFlipType = RotateFlipType.Rotate270FlipX;
                    break;
                case 8:
                    rotateFlipType = RotateFlipType.Rotate270FlipNone;
                    break;
                default:
                    rotateFlipType = RotateFlipType.RotateNoneFlipNone;
                    break;
            }

            return rotateFlipType;
        }
    }
}
