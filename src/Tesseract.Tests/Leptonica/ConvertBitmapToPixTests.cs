using System;

namespace Tesseract.Tests.Leptonica
{
    public class ConvertBitmapToPixTests : TesseractTestBase
    {

        
        private unsafe PixColor GetPixel(Pix pix, int x, int y)
        {
            var pixDepth = pix.Depth;
            var pixData = pix.GetData();
            var pixLine = (uint*)pixData.Data + pixData.WordsPerLine * y;
            uint pixValue;
            if (pixDepth == 1) {
                pixValue = PixData.GetDataBit(pixLine, x);
            } else if (pixDepth == 4) {
                pixValue = PixData.GetDataQBit(pixLine, x);
            } else if (pixDepth == 8) {
                pixValue = PixData.GetDataByte(pixLine, x);
            } else if (pixDepth == 32) {
                pixValue = PixData.GetDataFourByte(pixLine, x);
            } else {
                throw new ArgumentException(String.Format("Bit depth of {0} is not supported.", pix.Depth), "pix");
            }

            if (pix.Colormap != null) {
                return pix.Colormap[(int)pixValue];
            } else {
                if (pixDepth == 32) {
                    return PixColor.FromRgba(pixValue);
                } else {
                    byte grayscale = (byte)(pixValue * 255 / ((1 << 16) - 1));
                    return new PixColor(grayscale, grayscale, grayscale);
                }
            }
        }
    }
}
