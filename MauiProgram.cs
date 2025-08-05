using CommunityToolkit.Maui;
using MauiApp7.ViewModels;

namespace Shaunebu.Controls
{
    public static class MauiExtensions
    {
        public static MauiAppBuilder UseFloatingChatButton(this MauiAppBuilder builder)
        {
            builder.UseMauiCommunityToolkit();
            return builder;
        }

        public static MauiAppBuilder UseKanbanBoard(this MauiAppBuilder builder)
        {
            builder.UseMauiCommunityToolkit();
            builder.Services.AddTransient<KanbanViewModel>();
            return builder;
        }
    }
}
