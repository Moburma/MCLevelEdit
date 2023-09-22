using Avalonia.Media;
using Avalonia.Platform;
using System;

namespace MCLevelEdit.Avalonia
{

    public static class LockedFramebufferExtensions
    {
        public static Span<byte> GetPixels(this ILockedFramebuffer framebuffer)
        {
            unsafe
            {
                return new Span<byte>((byte*)framebuffer.Address, framebuffer.RowBytes * framebuffer.Size.Height);
            }
        }

        public static Span<byte> GetPixel(this ILockedFramebuffer framebuffer, int x, int y)
        {
            unsafe
            {
                var bytesPerPixel = framebuffer.Format.BitsPerPixel / 8;
                var zero = (byte*)framebuffer.Address;
                var offset = framebuffer.RowBytes * y + bytesPerPixel * x;
                return new Span<byte>(zero + offset, bytesPerPixel);
            }
        }

        public static void SetPixel(this ILockedFramebuffer framebuffer, int x, int y, Color color)
        {
            var pixel = framebuffer.GetPixel(x, y);

            var alpha = color.A / 255.0;

            pixel[0] = (byte)(color.R * alpha);
            pixel[1] = (byte)(color.G * alpha);
            pixel[2] = (byte)(color.B * alpha);
            pixel[3] = color.A;
        }
    }
}
