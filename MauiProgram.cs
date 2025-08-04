using CommunityToolkit.Maui;

namespace Shaunebu.Controls
{
    public static class MauiExtensions
    {
        public static MauiAppBuilder UseFloatingChatButton(this MauiAppBuilder builder)
        {
            builder.UseMauiCommunityToolkit();
            return builder;
        }
    }
}
