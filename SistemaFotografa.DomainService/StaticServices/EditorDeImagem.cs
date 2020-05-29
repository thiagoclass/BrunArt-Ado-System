using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace SistemaFotografa.DomainService
{
    public static class EditorDeImagem
    {
        public static byte[] DrawSize(byte[] imageOriginal, int widhtapproximate)
        {
            try
            {


                var imagemBitmap = Image.FromStream(new MemoryStream(imageOriginal));
                double Perc = ((widhtapproximate * 100) / imagemBitmap.Size.Width) + 1;
                Size newSize = new Size((int)(imagemBitmap.Size.Width * (Perc / 100)), (int)(imagemBitmap.Size.Height * (Perc / 100)));

                //newSize.Width = widht;
                //var grafico = Graphics.FromImage(imagemBitmap);
                //grafico.DrawImage(imagemBitmap, widht, 0);
                Bitmap bitmap = new Bitmap(imagemBitmap, newSize);

                MemoryStream memoryStream = new MemoryStream();
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imageContent = new byte[memoryStream.Length];
                memoryStream.Position = 0;
                memoryStream.Read(imageContent, 0, (int)memoryStream.Length);

                bitmap.Dispose();
                memoryStream.Dispose();


                return imageContent;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
