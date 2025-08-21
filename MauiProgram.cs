using CommunityToolkit.Maui;
using Shaunebu.Controls.Effects;
using Shaunebu.Controls.ViewModels;

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

        public static MauiAppBuilder UseTouchEffects(this MauiAppBuilder builder)
        {
            builder.ConfigureEffects(effects =>
            {
#if ANDROID
                effects.Add<TouchEffect, PlatformTouchEffect>();
#elif IOS
                effects.Add<TouchEffect, PlatformTouchEffect>();
#endif
            });

            return builder;
        }
    }
}
